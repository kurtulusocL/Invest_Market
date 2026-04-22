
namespace Investigation.Shared.Audits
{
    public static class Platform
    {
        public static string GetPlatform(string userAgent)
        {
            if (userAgent.Contains("Windows")) return "Windows";
            if (userAgent.Contains("Mac")) return "Mac";
            if (userAgent.Contains("Linux")) return "Linux";
            if (userAgent.Contains("Android")) return "Android";
            if (userAgent.Contains("iOS")) return "iOS";
            return "Unknown";
        }
    }
}
