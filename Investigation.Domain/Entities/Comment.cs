using Investigation.Domain.Entities.UserEntities;
using Investigation.Shared.Domain.EntityFramework;

namespace Investigation.Domain.Entities
{
    public class Comment : BaseEntity
    {
        public string Text { get; set; }

        public string AppUserId { get; set; }
        public int? BlogId { get; set; }
        public int? CompanyId { get; set; }
        public int? PostId { get; set; }

        public virtual AppUser AppUser { get; set; }
        public virtual Blog Blog { get; set; }
        public virtual Company Company { get; set; }
        public virtual Post Post { get; set; }

        public virtual ICollection<Hit> Hits { get; set; }
        public virtual ICollection<CommentAnswer> CommentAnswers { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
    }
}
