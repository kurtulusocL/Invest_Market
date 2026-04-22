using System.Collections.Concurrent;
using System.Text.Json;

namespace Investigation.Shared.Audits
{
    public static class ISPDetectionService
    {
        private static readonly ConcurrentDictionary<string, string> _ispCache = new();

        private static readonly Dictionary<string, string> KnownISPs = new()
        {
            {"Vodafone", "Vodafone"},
            {"Türk Telekom", "Türk Telekom"},
            {"Türkcell", "Türkcell"},
            {"Superonline", "Superonline"},
            {"TurkNet", "TurkNet"},
            {"Millenicom", "Millenicom"},
            {"Altice", "Altice"},
            {"Verizon", "Verizon"},
            {"AT&T", "AT&T"},
            {"Orange", "Orange"},
            {"Deutsche Telekom", "Deutsche Telekom"},
            {"Telefonica", "Telefonica"}
        };

        public static string GetISP(string ipAddress)
        {
            if (string.IsNullOrWhiteSpace(ipAddress))
                return "Unknown ISP";
            try
            {
                if (_ispCache.TryGetValue(ipAddress, out string cachedISP))
                    return cachedISP;

                var ispFromDb = GetISPFromLocalDatabase(ipAddress);
                if (!string.IsNullOrEmpty(ispFromDb))
                {
                    _ispCache[ipAddress] = ispFromDb;
                    return ispFromDb;
                }

                var ispFromApi = GetISPFromExternalAPI(ipAddress);
                if (!string.IsNullOrEmpty(ispFromApi))
                {
                    _ispCache[ipAddress] = ispFromApi;
                    return ispFromApi;
                }
                return "Unknown ISP";
            }
            catch
            {
                return "ISP Not Detected";
            }
        }

        private static string GetISPFromExternalAPI(string ipAddress)
        {
            try
            {
                using var client = new HttpClient();
                client.Timeout = TimeSpan.FromSeconds(3);
                var apis = new[]
                {
                    $"http://ip-api.com/json/{ipAddress}?fields=isp",
                    $"https://ipapi.co/{ipAddress}/json/",
                    $"https://ipinfo.io/{ipAddress}/json"
                };
                foreach (var apiUrl in apis)
                {
                    try
                    {
                        var response = client.GetStringAsync(apiUrl).Result;

                        if (apiUrl.Contains("ip-api.com"))
                        {
                            var json = JsonSerializer.Deserialize<JsonElement>(response);
                            if (json.TryGetProperty("isp", out var isp))
                            {
                                var ispValue = isp.GetString();
                                if (!string.IsNullOrEmpty(ispValue))
                                    return ispValue;
                            }
                        }
                        else if (apiUrl.Contains("ipapi.co"))
                        {
                            var json = JsonSerializer.Deserialize<JsonElement>(response);
                            if (json.TryGetProperty("org", out var org))
                            {
                                var orgValue = org.GetString();
                                if (!string.IsNullOrEmpty(orgValue))
                                    return orgValue;
                            }
                        }
                        else if (apiUrl.Contains("ipinfo.io"))
                        {
                            var json = JsonSerializer.Deserialize<JsonElement>(response);
                            if (json.TryGetProperty("org", out var org))
                            {
                                var orgValue = org.GetString();
                                if (!string.IsNullOrEmpty(orgValue))
                                    return orgValue;
                            }
                        }
                    }
                    catch
                    {
                        continue;
                    }
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        private static string GetISPFromLocalDatabase(string ipAddress)
        {
            var ispName = KnownISPs.Keys.FirstOrDefault(isp => ipAddress.StartsWith(isp, StringComparison.OrdinalIgnoreCase));
            return ispName != null ? KnownISPs[ispName] : null;
        }
    }
}
