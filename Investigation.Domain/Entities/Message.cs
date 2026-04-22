using Investigation.Domain.Entities.UserEntities;
using Investigation.Shared.Domain.EntityFramework;

namespace Investigation.Domain.Entities
{
    public class Message : BaseEntity
    {
        public string Content { get; set; }
        public DateTime SentAt { get; set; } = DateTime.UtcNow;
        public bool IsRead { get; set; }

        public string SenderId { get; set; }
        public string ReceiverId { get; set; }

        public AppUser Sender { get; set; }
        public AppUser Receiver { get; set; }
    }
}
