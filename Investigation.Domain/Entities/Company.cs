using Investigation.Domain.Entities.UserEntities;
using Investigation.Shared.Domain.EntityFramework;

namespace Investigation.Domain.Entities
{
    public class Company : BaseEntity
    {
        public string Name { get; set; }
        public string Slogan { get; set; }
        public string ShortBio { get; set; }
        public string Desc { get; set; }
        public DateTime FoundationDate { get; set; }        
        public bool IsLookingForInvest { get; set; }
        public bool IsFollowable { get; set; } = true;
        public string LinkedIn { get; set; }
        public string? GitHub { get; set; }
        public string LogoUrl { get; set; }

        public string AppUserId { get; set; }
        public int CompanyCategoryId { get; set; }
        public int CountryId { get; set; }
        public int SectorId { get; set; }
        public int? SubSectorId { get; set; }

        public virtual AppUser AppUser { get; set; }
        public virtual CompanyCategory CompanyCategory { get; set; }
        public virtual Country Country { get; set; }
        public virtual Sector Sector { get; set; }
        public virtual SubSector SubSector { get; set; }

        public virtual ICollection<Announcement> Announcements { get; set; }
        public virtual ICollection<Blog> Blogs { get; set; }
        public virtual ICollection<CompanyContact> CompanyContacts { get; set; }
        public virtual ICollection<CompanyFinance> CompanyFinances { get; set; }
        public virtual ICollection<CompanyPintech> CompanyPinteches { get; set; }
        public virtual ICollection<CompanyStage> CompanyStages { get; set; }
        public virtual ICollection<CompanyTeam> CompanyTeams { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Follow> CompanyFollowers { get; set; } = new HashSet<Follow>();
        public virtual ICollection<Follow> CompanyFollowings { get; set; } = new HashSet<Follow>();
        public virtual ICollection<Hit> Hits { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<Picture> Pictures { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
        public virtual ICollection<SavedContent> SavedContents { get; set; }
        public virtual ICollection<Survey> Surveys { get; set; }
        public virtual ICollection<UserSocialMedia> UserSocialMedias { get; set; }
    }
}
