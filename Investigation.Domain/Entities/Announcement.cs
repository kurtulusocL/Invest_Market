using Investigation.Shared.Domain.EntityFramework;

namespace Investigation.Domain.Entities
{
    public class Announcement : BaseEntity
    {
        public string Title { get; set; }
        public string? Subtitle { get; set; }
        public string Content { get; set; }
        public string? ImageUrl { get; set; }

        public int AnnouncementCategoryId { get; set; }
        public int? InvestorId { get; set; }
        public int? CompanyId { get; set; }

        public virtual AnnouncementCategory AnnouncementCategory { get; set; }
        public virtual Investor Investor { get; set; }
        public virtual Company Company { get; set; }

        public virtual ICollection<Hit> Hits { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
    }
}
