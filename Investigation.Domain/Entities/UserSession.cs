using Investigation.Domain.Entities.UserEntities;
using Investigation.Shared.Domain.EntityFramework;

namespace Investigation.Domain.Entities
{
    public class UserSession : BaseEntity
    {
        public string Username { get; set; }
        public DateTime LoginDate { get; set; }
        public DateTime? LogoutDate { get; set; }
        public bool IsOnline { get; set; }
        public int? OnlineDurationSeconds { get; set; }
        public DateTime? LastHeartbeat { get; set; }

        public string AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; }
    }
}
