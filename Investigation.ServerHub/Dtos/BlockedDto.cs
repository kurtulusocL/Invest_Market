
namespace Investigation.ServerHub.Dtos
{
    public class BlockedDto
    {
        public int Id { get; set; }
        public string? RemoteIpAddress { get; set; }
        public string? IpAddressVPN { get; set; }
        public string? DeviceFingerprint { get; set; }
        public string? LocalIpAddress { get; set; }
        public string? Host { get; set; }
        public string? Note { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public DateTime? SuspendedDate { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
    }
}
