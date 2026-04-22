using System.Collections.Concurrent;
using System.Threading.RateLimiting;
using Investigation.Business.Constants.Services;
using Investigation.DataAccess.Concrete.EntityFramework.Context.MSSQL;
using Investigation.Shared.Audits;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Investigation.Business.Extensions
{
    public static class SecurityExtensions
    {
        private static readonly ConcurrentDictionary<string, DateTime> BannedIps = new();

        private static DateTime _lastCleanup = DateTime.UtcNow;
        private const int CLEANUP_INTERVAL_MINUTES = 30;

        private static HashSet<string> _staticExtensions = new(StringComparer.OrdinalIgnoreCase);
        private static HashSet<string> _blockedAgents = new(StringComparer.OrdinalIgnoreCase);

        private static DateTime _lastSettingsUpdate = DateTime.MinValue;
        private static int _settingsUpdateInProgress = 0;
        private const int SETTINGS_CACHE_MINUTES = 5;
        public static IServiceCollection AddCustomSecurity(this IServiceCollection services, IConfiguration config)
        {
            services.AddRateLimiter(options =>
            {
                options.RejectionStatusCode = 429;

                options.AddFixedWindowLimiter("LoginPolicy", opt =>
                {
                    opt.PermitLimit = 5;
                    opt.Window = TimeSpan.FromMinutes(1);
                    opt.QueueLimit = 0;
                    opt.AutoReplenishment = true;
                });

                options.AddFixedWindowLimiter("signalr", opt =>
                {
                    opt.PermitLimit = config.GetValue<int>("RateLimit:SignalR:PermitLimit");
                    opt.Window = TimeSpan.FromSeconds(config.GetValue<int>("RateLimit:SignalR:WindowSeconds"));
                    opt.QueueLimit = 5;
                    opt.AutoReplenishment = true;
                });

                options.AddFixedWindowLimiter("web", opt =>
                {
                    opt.PermitLimit = config.GetValue<int>("RateLimit:Web:PermitLimit");
                    opt.Window = TimeSpan.FromSeconds(config.GetValue<int>("RateLimit:Web:WindowSeconds"));
                    opt.QueueLimit = 0;
                    opt.AutoReplenishment = true;
                });

                options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
                {
                    var ip = RemoteIpAddress.GetRemoteIpAddress(context) ?? "unknown";

                    var path = context.Request.Path.ToString().ToLower();
                    var isSignalRHub = path.Contains("/hub") || path.Contains("/signalr");

                    if (isSignalRHub)
                    {
                        return RateLimitPartition.GetFixedWindowLimiter(
                            partitionKey: $"signalr:{ip}",
                            factory: _ => new FixedWindowRateLimiterOptions
                            {
                                PermitLimit = config.GetValue<int>("RateLimit:SignalR:PermitLimit"),
                                Window = TimeSpan.FromSeconds(config.GetValue<int>("RateLimit:SignalR:WindowSeconds")),
                                QueueLimit = 5,
                                AutoReplenishment = true
                            });
                    }
                    else if (path.Contains("/authuser/login") && context.Request.Method == "POST")
                    {
                        return RateLimitPartition.GetFixedWindowLimiter(
                            partitionKey: $"login:{ip}",
                            factory: _ => new FixedWindowRateLimiterOptions
                            {
                                PermitLimit = 5,
                                Window = TimeSpan.FromMinutes(1),
                                QueueLimit = 0,
                                AutoReplenishment = true
                            });
                    }
                    else
                    {
                        return RateLimitPartition.GetFixedWindowLimiter(
                            partitionKey: $"web:{ip}",
                            factory: _ => new FixedWindowRateLimiterOptions
                            {
                                PermitLimit = config.GetValue<int>("RateLimit:Web:PermitLimit"),
                                Window = TimeSpan.FromSeconds(config.GetValue<int>("RateLimit:Web:WindowSeconds")),
                                QueueLimit = 0,
                                AutoReplenishment = true
                            });
                    }
                });

                options.OnRejected = async (context, token) =>
                {
                    var ip = RemoteIpAddress.GetRemoteIpAddress(context.HttpContext) ?? "unknown";
                    var path = context.HttpContext.Request.Path;

                    if (!BannedIps.ContainsKey(ip))
                        BannedIps.TryAdd(ip, DateTime.UtcNow.AddHours(24));

                    CleanupExpiredBans();

                    Console.WriteLine($"⚠️ Rate limit exceeded - IP: {ip}, Path: {path}, Time: {DateTime.UtcNow}");

                    context.HttpContext.Response.StatusCode = 429;
                    context.HttpContext.Response.ContentType = "application/json";

                    var response = new
                    {
                        error = "Too many requests",
                        message = "You sent too many requests, you've been banned for 24 hours.",
                        retryAfter = 86400
                    };

                    await context.HttpContext.Response.WriteAsJsonAsync(response, cancellationToken: token);
                };
            });
            services.AddSingleton(BannedIps);
            return services;
        }

        public static IApplicationBuilder UseCustomSecurity(this IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                var dbContext = context.RequestServices.GetRequiredService<ApplicationDbContext>();
                await UpdateSettingsCacheIfNeeded(dbContext);

                var ext = Path.GetExtension(context.Request.Path.Value).ToLowerInvariant();
                if (_staticExtensions.Contains(ext))
                {
                    await next();
                    return;
                }

                var _httpContextAccessor = context.RequestServices.GetRequiredService<IHttpContextAccessor>();
                var _webHelperService = context.RequestServices.GetRequiredService<IWebHelperService>();
                var bannedIps = context.RequestServices.GetRequiredService<ConcurrentDictionary<string, DateTime>>();

                var ip = RemoteIpAddress.GetRemoteIpAddress(context);
                if (ip != null && bannedIps.TryGetValue(ip, out var banEnd))
                {
                    if (DateTime.UtcNow < banEnd)
                    {
                        context.Response.StatusCode = 403;
                        await context.Response.WriteAsync("Your IP is banned for 24 hours, come back later.");
                        return;
                    }
                    bannedIps.TryRemove(ip, out _);
                }

                var ua = context.Request.Headers.UserAgent.ToString();
                if (_blockedAgents.Any(bot => ua.Contains(bot, StringComparison.OrdinalIgnoreCase)))
                {
                    context.Response.StatusCode = 403;
                    await context.Response.WriteAsync("Are you a bot or something? Goodbye.");
                    return;
                }

                if (context.User.Identity?.IsAuthenticated == true)
                {
                    var sessionIp = _httpContextAccessor.HttpContext.Session.GetString("OriginalIP");
                    var sessionUa = _httpContextAccessor.HttpContext.Session.GetString("OriginalUA");
                    if (sessionIp != null && (sessionIp != ip || sessionUa != ua))
                    {
                        await Microsoft.AspNetCore.Authentication.AuthenticationHttpContextExtensions.SignOutAsync(context);
                        _httpContextAccessor.HttpContext.Session.Clear();
                        context.Response.Redirect("/AuthUser/Login?error=security_breach");
                        return;
                    }
                }
                await next();
            });
            app.UseRateLimiter();
            return app;
        }

        private static async Task UpdateSettingsCacheIfNeeded(ApplicationDbContext dbContext)
        {
            if (DateTime.UtcNow.Subtract(_lastSettingsUpdate).TotalMinutes < SETTINGS_CACHE_MINUTES)
                return;

            if (Interlocked.CompareExchange(ref _settingsUpdateInProgress, 1, 0) != 0)
                return;

            try
            {
                if (DateTime.UtcNow.Subtract(_lastSettingsUpdate).TotalMinutes < SETTINGS_CACHE_MINUTES)
                    return;

                var settings = await dbContext.SecuritySettings.Where(s => s.IsActive && !s.IsDeleted).AsNoTracking().ToListAsync();

                _staticExtensions = new HashSet<string>(
                    settings
                        .Where(s => s.Type == "StaticExtension")
                        .Select(s => s.Value),
                    StringComparer.OrdinalIgnoreCase);

                _blockedAgents = new HashSet<string>(
                    settings
                        .Where(s => s.Type == "BlockedAgent")
                        .Select(s => s.Value),
                    StringComparer.OrdinalIgnoreCase);

                _lastSettingsUpdate = DateTime.UtcNow;

                Console.WriteLine($"✅ SecuritySettings cache updated. Extensions: {_staticExtensions.Count}, Agents: {_blockedAgents.Count}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ SecuritySettings cache güncellenirken hata: {ex.Message}");
            }
            finally
            {
                Interlocked.Exchange(ref _settingsUpdateInProgress, 0);
            }
        }

        private static void CleanupExpiredBans()
        {
            if (DateTime.UtcNow.Subtract(_lastCleanup).TotalMinutes < CLEANUP_INTERVAL_MINUTES)
                return;

            var expired = BannedIps.Where(x => x.Value < DateTime.UtcNow).ToList();
            foreach (var entry in expired)
                BannedIps.TryRemove(entry.Key, out _);

            _lastCleanup = DateTime.UtcNow;

            if (expired.Count > 0)
                Console.WriteLine($"🧹 BannedIps cleanup: {expired.Count} expired entries removed.");
        }
    }
}