using Investigation.Shared.Domain.EntityFramework;

namespace Investigation.Domain.Entities
{
    public class Blocked : BaseEntity
    {
        public string? RemoteIpAddress { get; set; }
        public string? IpAddressVPN { get; set; }
        public string? DeviceFingerprint { get; set; }
        public string? LocalIpAddress { get; set; }
        public string? Host { get; set; }
        public string? Note { get; set; }
    }
}