using System.Net;
using Microsoft.AspNetCore.Http;

namespace Investigation.Shared.Helpers
{
    public static class DeviceInfoHelper
    {
        public static string? GetDeviceFingerprint(HttpContext context)
        {
            context.Request.Cookies.TryGetValue("deviceFingerprint", out var fingerprint);
            return string.IsNullOrWhiteSpace(fingerprint) ? null : fingerprint;
        }

        public static string? GetLocalIpFromCookie(HttpContext context)
        {
            context.Request.Cookies.TryGetValue("localIp", out var localIp);
            if (string.IsNullOrWhiteSpace(localIp)) return null;
            return IPAddress.TryParse(localIp, out _) ? localIp : null;
        }
    }
}
