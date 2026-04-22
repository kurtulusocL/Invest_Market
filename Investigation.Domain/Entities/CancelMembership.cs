using Investigation.Domain.Entities.UserEntities;
using Investigation.Shared.Domain.EntityFramework;

namespace Investigation.Domain.Entities
{
    public class CancelMembership : BaseEntity
    {
        public string Title { get; set; }
        public string Desc { get; set; }
        public bool IsRequestCancelled { get; set; } = false; //is user cancelled to his request
        public bool IsCancelled { get; set; } = false;
        public DateTime? CancelDate { get; set; }
        public DateTime? RequestCancelledDate { get; set; }
        public int Hit { get; set; } = 0;

        public string AppUserId { get; set; }
        public int CancelMembershipCategoryId { get; set; }

        public virtual AppUser AppUser { get; set; }
        public virtual CancelMembershipCategory CancelMembershipCategory { get; set; }
    }
}
