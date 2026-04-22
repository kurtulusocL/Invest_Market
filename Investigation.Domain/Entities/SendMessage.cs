using Investigation.Shared.Domain.EntityFramework;

namespace Investigation.Domain.Entities
{
    public class SendMessage : BaseEntity
    {
        public string NameSurname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string MessageTitle { get; set; }
        public string MessageSubject { get; set; }
        public string MessageContent { get; set; }
    }
}
