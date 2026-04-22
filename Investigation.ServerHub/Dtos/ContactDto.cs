
namespace Investigation.ServerHub.Dtos
{
    public class ContactDto
    {
        public int Id { get; set; }
        public string BusinessEmail { get; set; }
        public string OtherEmail { get; set; }
        public string? PhoneNumber { get; set; }
        public string Location { get; set; }
        public string? LocationMap { get; set; }
        public string? Mernis { get; set; }
        public string? KEPAddress { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public DateTime? SuspendedDate { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
    }
}
