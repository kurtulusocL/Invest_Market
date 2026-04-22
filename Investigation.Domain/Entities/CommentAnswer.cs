using Investigation.Domain.Entities.UserEntities;
using Investigation.Shared.Domain.EntityFramework;

namespace Investigation.Domain.Entities
{
    public class CommentAnswer : BaseEntity
    {
        public string Text { get; set; }

        public string AppUserId { get; set; }
        public int? CommentId { get; set; }

        public virtual AppUser AppUser { get; set; }
        public virtual Comment Comment { get; set; }

        public virtual ICollection<Hit> Hits { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
    }
}
