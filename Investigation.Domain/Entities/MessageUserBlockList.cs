using Investigation.Domain.Entities.UserEntities;
using Investigation.Shared.Domain.EntityFramework;

namespace Investigation.Domain.Entities
{
    public class MessageUserBlockList : BaseEntity
    {
        public bool IsBlocked { get; set; } = true;
        public bool IsRemoved { get; set; } = false;
        public DateTime BlockedAt { get; set; } = DateTime.UtcNow;
        public string BlockedUserName { get; set; }
       
        public string BlockerId { get; set; }
        public string BlockedId { get; set; }

        public AppUser Blocker { get; set; }
        public AppUser Blocked { get; set; }
    }
}
