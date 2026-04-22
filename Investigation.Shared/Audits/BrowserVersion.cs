using UAParser;

namespace Investigation.Shared.Audits
{
    public static class BrowserVersion
    {
        public static string GetBrowserVersion(string userAgent)
        {
            if (string.IsNullOrEmpty(userAgent))
                return "Anonymous";

            var parser = Parser.GetDefault();
            var clientInfo = parser.Parse(userAgent);
            return clientInfo.UA.Major + "." + clientInfo.UA.Minor;
        }
    }
}
