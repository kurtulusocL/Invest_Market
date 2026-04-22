using Investigation.Shared.Domain.EntityFramework;

namespace Investigation.Domain.Entities
{
    public class BlackList : BaseEntity
    {
        public string RemoteIpAddress { get; set; }
        public string IpAddressVPN { get; set; }
        public string? DeviceFingerprint { get; set; }
        public string? LocalIpAddress { get; set; }
        public DateTime ExpirationDate { get; set; }

        public int? AuditId { get; set; }
        public virtual Audit Audit { get; set; }
    }
}
