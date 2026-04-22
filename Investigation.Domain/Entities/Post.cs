using Investigation.Domain.Entities.UserEntities;
using Investigation.Shared.Domain.EntityFramework;

namespace Investigation.Domain.Entities
{
    public class Post : BaseEntity
    {
        public string? Text { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsCommentable { get; set; }

        public string AppUserId { get; set; }
        public int? CompanyId { get; set; }
        public int? InvestorId { get; set; }

        public virtual AppUser AppUser { get; set; }
        public virtual Company Company { get; set; }
        public virtual Investor Investor { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Hit> Hits { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<Picture> Pictures { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
        public virtual ICollection<SavedContent> SavedContents { get; set; }
    }
}
