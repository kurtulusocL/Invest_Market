using Investigation.Shared.Domain.EntityFramework;

namespace Investigation.Domain.Entities
{
    public class Contact : BaseEntity
    {
        public string BusinessEmail { get; set; }
        public string OtherEmail { get; set; }
        public string? PhoneNumber { get; set; }
        public string Location { get; set; }
        public string? LocationMap { get; set; }
        public string? Mernis { get; set; }
        public string? KEPAddress { get; set; }
    }
}
