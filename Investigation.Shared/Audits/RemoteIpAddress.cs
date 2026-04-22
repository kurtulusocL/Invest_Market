using System.Net;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;

namespace Investigation.Shared.Audits
{
    public static class RemoteIpAddress
    {
        public static string GetRemoteIpAddress(HttpContext context)
        {
            try
            {
                var forwardedFor = context.Request.Headers["X-Forwarded-For"].ToString();
                if (!string.IsNullOrEmpty(forwardedFor))
                {
                    var ip = forwardedFor.Split(',')[0].Trim();
                    if (IsValidIpAddress(ip)) return ip;
                }

                var forwarded = context.Request.Headers["Forwarded"].ToString();
                if (!string.IsNullOrEmpty(forwarded))
                {
                    var match = Regex.Match(forwarded, @"for=(?:""?\[?([^\]"";,]+)\]?""?)", RegexOptions.IgnoreCase);
                    if (match.Success && IsValidIpAddress(match.Groups[1].Value))
                        return match.Groups[1].Value.Trim();
                }

                var cfIp = context.Request.Headers["CF-Connecting-IP"].ToString();
                if (!string.IsNullOrEmpty(cfIp) && IsValidIpAddress(cfIp)) return cfIp.Trim();

                var trueClientIp = context.Request.Headers["True-Client-IP"].ToString();
                if (!string.IsNullOrEmpty(trueClientIp) && IsValidIpAddress(trueClientIp)) return trueClientIp.Trim();

                var remoteIp = context.Connection.RemoteIpAddress;
                if (remoteIp == null) return "Unknown";

                return remoteIp.IsIPv4MappedToIPv6
                    ? remoteIp.MapToIPv4().ToString()
                    : remoteIp.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while obtaining remote IP: {ex.Message}");
                return "Unknown";
            }
        }

        private static bool IsValidIpAddress(string? ip)
        {
            return !string.IsNullOrWhiteSpace(ip) && IPAddress.TryParse(ip, out _);
        }
    }
}