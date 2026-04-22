using Investigation.Domain.Entities.UserEntities;
using Investigation.Shared.Domain.EntityFramework;

namespace Investigation.Domain.Entities
{
    public class Blog : BaseEntity
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string? Detail { get; set; }
        public string Content { get; set; }
        public string CoverImage { get; set; }

        public string AppUserId { get; set; }
        public int BlogCategoryId { get; set; }
        public int? CompanyId { get; set; }
        public int? InvestorId { get; set; }

        public virtual AppUser AppUser { get; set; }
        public virtual BlogCategory BlogCategory { get; set; }
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
