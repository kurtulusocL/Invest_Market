
namespace Investigation.Shared.Audits
{
    public static class DeviceType
    {
        public static string GetDeviceType(string userAgent)
        {
            if (userAgent.Contains("Mobile")) return "Mobile";
            if (userAgent.Contains("Tablet")) return "Tablet";
            return "Desktop";
        }
    }
}
