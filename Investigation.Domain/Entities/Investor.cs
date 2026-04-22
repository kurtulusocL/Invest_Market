using Investigation.Domain.Entities.UserEntities;
using Investigation.Shared.Domain.EntityFramework;

namespace Investigation.Domain.Entities
{
    public class Investor : BaseEntity
    {
        public string Bio { get; set; }
        public string InvestArea { get; set; }
        public DateTime SinceWhen { get; set; }
        public bool IsLookingForCompany { get; set; }
        public string? EmailAddress { get; set; }
        public string? PhoneNumber { get; set; }
        public string? CoverImageUrl { get; set; }

        public string AppUserId { get; set; }
        public int CountryId { get; set; }
        public int InvestorCategoryId { get; set; }

        public virtual AppUser AppUser { get; set; }
        public virtual Country Country { get; set; }
        public virtual InvestorCategory InvestorCategory { get; set; }

        public virtual ICollection<Announcement> Announcements { get; set; }
        public virtual ICollection<Blog> Blogs { get; set; }
        public virtual ICollection<Hit> Hits { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<RecentlyInvest> RecentlyInvests { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
        public virtual ICollection<SavedContent> SavedContents { get; set; }
        public virtual ICollection<Survey> Surveys { get; set; }
        public virtual ICollection<UserSocialMedia> UserSocialMedias { get; set; }
    }
}
