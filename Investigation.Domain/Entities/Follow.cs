using Investigation.Domain.Entities.UserEntities;
using Investigation.Shared.Domain.EntityFramework;

namespace Investigation.Domain.Entities
{
    public class Follow : BaseEntity
    {
        public bool IsFollowed { get; set; }
        public bool IsCanceled { get; set; }
        public DateTime FollowDate { get; set; }
        public DateTime? UnfollowDate { get; set; }
        public DateTime? CanceledFollowDate { get; set; }

        public string? FollowerUserId { get; set; }
        public int? FollowerCompanyId { get; set; }
        public string? FollowedUserId { get; set; }
        public int? FollowedCompanyId { get; set; }

        public virtual AppUser FollowerUser { get; set; }
        public virtual AppUser FollowedUser { get; set; }
        public virtual Company FollowerCompany { get; set; }
        public virtual Company FollowedCompany { get; set; }
    }
}
