using Investigation.Shared.Domain.EntityFramework;

namespace Investigation.Domain.Entities
{
    public class AnnouncementCategory:BaseEntity
    {
        public string Name { get; set; }

        public virtual ICollection<Announcement> Announcements { get; set; }
    }
}
