using Investigation.Domain.Entities.UserEntities;
using Investigation.Shared.Domain.EntityFramework;

namespace Investigation.Domain.Entities
{
    public class UserProfileImage : BaseEntity
    {
        public string ImageUrl { get; set; }

        public string AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; }
    }
}
