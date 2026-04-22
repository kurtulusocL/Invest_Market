using System.Text;
using System.Text.Json;
using Investigation.Business.Constants.Services;
using Investigation.DataAccess.Concrete.EntityFramework.Context.MSSQL;
using Investigation.Domain.Entities;
using Investigation.Shared.Audits;
using Investigation.Shared.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace Investigation.Business.Attributes
{
    public class AuditLogAttribute : ActionFilterAttribute
    {
        private static readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);
        public override async void OnActionExecuting(ActionExecutingContext filterContext)
        {
            using (var scope = ServiceProviderHelper.ServiceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var encryptionService = scope.ServiceProvider.GetRequiredService<EncryptionService>();
                var request = filterContext.HttpContext.Request;
                var userAgent = request.HttpContext.Request.Headers["User-Agent"].ToString();               

                Audit audit = new Audit()
                {
                    UserName = (request.HttpContext.User.Identity.IsAuthenticated) ? encryptionService.Encrypt(request.HttpContext.Session.GetString("UserName")) : "Anonymous",
                    UserId = request.HttpContext.Session.GetString("userId"),
                    Port = request.HttpContext.Connection.RemotePort,                    
                    Browser = request.HttpContext.Request.Headers["User-Agent"].ToString(),
                    BrowserVersion = BrowserVersion.GetBrowserVersion(request.HttpContext.Request.Headers["User-Agent"].ToString()),
                    Language = request.HttpContext.Request.Headers["Accept-Language"].ToString(),
                    AreaAccessed = AreaAccessed.GetDetailedAreaAccessed(filterContext),
                    Host = request.HttpContext.Request.Host.ToString(),
                    ProxyConnection = request.HttpContext.Request.Headers["Connection"],
                    Device = DeviceType.GetDeviceType(userAgent),
                    DeviceModel = DeviceModel.GetDeviceModel(userAgent),
                    Platform = Platform.GetPlatform(userAgent),                    
                    RemoteIpAddress = RemoteIpAddress.GetRemoteIpAddress(request.HttpContext),
                    InternetServiceProvider =  ISPDetectionService.GetISP(RemoteIpAddress.GetRemoteIpAddress(request.HttpContext)),
                    IpAddressVPN = IpAddressWithVpn.GetClientIPAddress(request.HttpContext),
                    DeviceFingerprint = DeviceInfoHelper.GetDeviceFingerprint(request.HttpContext),
                    LocalIpAddress = DeviceInfoHelper.GetLocalIpFromCookie(request.HttpContext),
                    CreatedDate = DateTime.Now
                };
                filterContext.HttpContext.Items["CurrentAudit"] = audit;

                dbContext.Audits.Add(audit);
                await SaveToFileAsJsonAsync(audit);
                await dbContext.SaveChangesAsync();
                base.OnActionExecuting(filterContext);
            }
        }
        private async Task SaveToFileAsJsonAsync(Audit audit)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "AuditLogs.txt");
            var jsonString = JsonSerializer.Serialize(audit, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            await _semaphore.WaitAsync();
            try
            {
                await File.AppendAllTextAsync(filePath, jsonString + Environment.NewLine);
            }
            catch (IOException ex)
            {
                Console.WriteLine($"File writing error: {ex.Message}");
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }
}
