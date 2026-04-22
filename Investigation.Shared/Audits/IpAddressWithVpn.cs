using System.Net;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;

namespace Investigation.Shared.Audits
{
    public static class IpAddressWithVpn
    {
        public static string GetClientIPAddress(HttpContext context)
        {
            var forwardedHeader = context.Request.Headers["X-Forwarded-For"].ToString();
            if (!string.IsNullOrEmpty(forwardedHeader))
            {
                var match = Regex.Match(forwardedHeader, @"for=(?:""?\[?([^\]"";]+)\]?""?)", RegexOptions.IgnoreCase);
                if (match.Success) return match.Groups[1].Value;

                var ip = forwardedHeader.Split(',')[0].Trim();
                if (IPAddress.TryParse(ip, out _)) return ip;
            }

            // Forwarded (RFC 7239)
            var forwarded = context.Request.Headers["Forwarded"].ToString();
            if (!string.IsNullOrEmpty(forwarded))
            {
                var forwardedIp = forwarded.Split(',')
                    .Select(x => x.Split('=').ElementAtOrDefault(1))
                    .FirstOrDefault(x => !string.IsNullOrEmpty(x));

                if (!string.IsNullOrEmpty(forwardedIp) && IPAddress.TryParse(forwardedIp, out _))
                    return forwardedIp;
            }

            // Cloudflare
            var cfIp = context.Request.Headers["CF-Connecting-IP"].ToString();
            if (!string.IsNullOrEmpty(cfIp) && IPAddress.TryParse(cfIp, out _)) return cfIp;

            // Akamai / CDN
            var trueClientIp = context.Request.Headers["True-Client-IP"].ToString();
            if (!string.IsNullOrEmpty(trueClientIp) && IPAddress.TryParse(trueClientIp, out _)) return trueClientIp;

            // Direkt bağlantı
            var remoteIp = context.Connection.RemoteIpAddress;
            if (remoteIp == null) return "Unknown";

            return remoteIp.IsIPv4MappedToIPv6
                ? remoteIp.MapToIPv4().ToString()
                : remoteIp.ToString();
        }
    }
}
