
namespace Investigation.Shared.Dtos.BlockDtos
{
    public class ClientInfoDto
    {
        public string? DeviceFingerprint { get; set; }
        public string? LocalIpAddress { get; set; }
        public string RemoteIpAddress { get; set; }
        public string IpAddressVPN { get; set; }
        public string Host { get; set; }
        public string UserAgent { get; set; }
    }
}
