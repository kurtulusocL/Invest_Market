using Investigation.Shared.Domain.EntityFramework;

namespace Investigation.Domain.Entities
{
    public class Audit : BaseEntity
    {
        public string? UserName { get; set; }
        public string? UserId { get; set; }       
        public string AreaAccessed { get; set; }
        public string Browser { get; set; }
        public string Device { get; set; }
        public string Language { get; set; }
        public string BrowserVersion { get; set; }
        public bool IsMobile { get; set; }
        public string DeviceModel { get; set; }
        public string Platform { get; set; }      
        public string RemoteIpAddress { get; set; }
        public string IpAddressVPN { get; set; }
        public string Host { get; set; }
        public string ProxyConnection { get; set; }
        public string InternetServiceProvider { get; set; }
        public int? Port { get; set; }
        public string? DeviceFingerprint { get; set; }
        public string? LocalIpAddress { get; set; }

        public virtual ICollection<BlackList> BlackLists { get; set; }
    }
}
