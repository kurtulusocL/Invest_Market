using System.Net;
using Investigation.DataAccess.Concrete.EntityFramework.Context.MSSQL;
using Investigation.Shared.Audits;
using Investigation.Shared.Dtos.BlockDtos;
using Investigation.Shared.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Investigation.Business.Constants.Middlewares
{
    public class BlockUserMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<BlockUserMiddleware> _logger;
        private static volatile IReadOnlyDictionary<string, byte> _blockedCache = new Dictionary<string, byte>();
        private static DateTime _lastCacheUpdate = DateTime.MinValue;
        private static int _updateInProgress = 0;
        private const int CACHE_REFRESH_MINUTES = 1;

        private static readonly HashSet<string> _localhostAddresses = new(StringComparer.OrdinalIgnoreCase)
        {
            "localhost", "127.0.0.1", "::1", "0:0:0:0:0:0:0:1"
        };

        public BlockUserMiddleware(RequestDelegate next, ILogger<BlockUserMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, ApplicationDbContext dbContext)
        {
            if (IsLocalRequest(context))
            {
                await _next(context);
                return;
            }
            await UpdateCacheIfNeeded(dbContext);

            if (_blockedCache.Count == 0)
            {
                await _next(context);
                return;
            }

            var clientInfo = GetClientInfo(context);
            if (clientInfo == null)
            {
                await _next(context);
                return;
            }

            if (IsBlockedInMemory(clientInfo))
            {
                _logger.LogWarning("Blocked request from Remote:{Remote} Local:{Local} Host:{Host}",
                    clientInfo.RemoteIpAddress, clientInfo.LocalIpAddress, clientInfo.Host);

                await SendBlockedResponse(context);
                return;
            }

            await _next(context);
        }

        private bool IsLocalRequest(HttpContext context)
        {
            var host = context.Request.Host.Host;

            if (!string.IsNullOrEmpty(host) && _localhostAddresses.Contains(host))
                return true;

            var remoteIp = context.Connection.RemoteIpAddress;
            if (remoteIp != null && IPAddress.IsLoopback(remoteIp))
                return true;

            return false;
        }

        private async Task UpdateCacheIfNeeded(ApplicationDbContext dbContext)
        {
            if (DateTime.Now.Subtract(_lastCacheUpdate).TotalMinutes < CACHE_REFRESH_MINUTES)
                return;

            if (Interlocked.CompareExchange(ref _updateInProgress, 1, 0) != 0)
                return;

            try
            {
                if (DateTime.Now.Subtract(_lastCacheUpdate).TotalMinutes < CACHE_REFRESH_MINUTES)
                    return;

                var blockedItems = await dbContext.Blockeds
                    .Where(i => i.IsActive && !i.IsDeleted).Select(b => new
                    {
                        b.RemoteIpAddress,
                        b.IpAddressVPN,
                        b.DeviceFingerprint,
                        b.LocalIpAddress,
                        b.Host
                    }).AsNoTracking().ToListAsync();

                var newCache = new Dictionary<string, byte>(StringComparer.OrdinalIgnoreCase);

                foreach (var item in blockedItems)
                {
                    TryAddToCache(newCache, "REMOTE", item.RemoteIpAddress);
                    TryAddToCache(newCache, "VPN", item.IpAddressVPN);
                    TryAddToCache(newCache, "FINGERPRINT", item.DeviceFingerprint);
                    TryAddToCache(newCache, "LOCAL", item.LocalIpAddress);
                    TryAddToCache(newCache, "HOST", item.Host?.ToLowerInvariant());
                }

                Volatile.Write(ref _blockedCache, newCache);
                _lastCacheUpdate = DateTime.Now;
                _logger.LogInformation("Block cache updated. Total entries: {Count}", newCache.Count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Block cache güncellenirken hata oluştu.");
            }
            finally
            {
                Interlocked.Exchange(ref _updateInProgress, 0);
            }
        }

        private static void TryAddToCache(Dictionary<string, byte> cache, string prefix, string? value)
        {
            if (!string.IsNullOrWhiteSpace(value))
                cache.TryAdd($"{prefix}:{value.Trim()}", 1);
        }

        private bool IsBlockedInMemory(ClientInfoDto clientInfo)
        {
            var cache = _blockedCache;

            if (IsMatch(cache, "LOCAL", clientInfo.LocalIpAddress)) return true;
            if (IsMatch(cache, "REMOTE", clientInfo.RemoteIpAddress)) return true;
            if (IsMatch(cache, "FINGERPRINT", clientInfo.DeviceFingerprint)) return true;
            if (IsMatch(cache, "LOCAL", clientInfo.LocalIpAddress)) return true;
            if (IsMatch(cache, "VPN", clientInfo.IpAddressVPN)) return true;
            if (IsMatch(cache, "HOST", clientInfo.Host?.ToLowerInvariant())) return true;

            return false;
        }

        private static bool IsMatch(IReadOnlyDictionary<string, byte> cache, string prefix, string? value)
        {
            return !string.IsNullOrWhiteSpace(value) &&
                   cache.ContainsKey($"{prefix}:{value.Trim()}");
        }

        private ClientInfoDto? GetClientInfo(HttpContext context)
        {
            try
            {
                return new ClientInfoDto
                {
                    RemoteIpAddress = RemoteIpAddress.GetRemoteIpAddress(context),
                    IpAddressVPN = IpAddressWithVpn.GetClientIPAddress(context),
                    DeviceFingerprint = DeviceInfoHelper.GetDeviceFingerprint(context),
                    LocalIpAddress = DeviceInfoHelper.GetLocalIpFromCookie(context),
                    Host = context.Request.Host.Host,
                    UserAgent = context.Request.Headers.UserAgent.ToString()
                };
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "İstemci bilgisi alınırken hata oluştu.");
                return null;
            }
        }

        private static async Task SendBlockedResponse(HttpContext context)
        {
            if (context.Response.HasStarted) return;

            context.Response.Clear();
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            context.Response.ContentType = "text/plain; charset=utf-8";
            await context.Response.WriteAsync("Access to this site has been blocked.");
            await context.Response.CompleteAsync();
        }
    }
}
