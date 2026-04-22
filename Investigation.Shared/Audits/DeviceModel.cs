using UAParser;

namespace Investigation.Shared.Audits
{
    public static class DeviceModel
    {
        public static string GetDeviceModel(string userAgent)
        {
            if (string.IsNullOrEmpty(userAgent))
            {
                return "Unknown Model";
            }

            var parser = Parser.GetDefault();
            var clientInfo = parser.Parse(userAgent);

            var device = clientInfo.Device;
            var os = clientInfo.OS;

            if (device.Family != "Other" && !string.IsNullOrEmpty(device.Brand))
            {
                return $"{device.Brand} {device.Model}";
            }

            if (os.Family.Contains("Mac OS"))
            {
                return "Apple Macintosh";
            }
            else if (os.Family.Contains("Windows"))
            {
                return $"Windows PC ({os.Family} {os.Major}.{os.Minor})";
            }
            else if (os.Family.Contains("Linux") && !userAgent.Contains("Android"))
            {
                return $"Linux PC ({os.Family})";
            }
            return "Unknown Model";
        }
    }
}
