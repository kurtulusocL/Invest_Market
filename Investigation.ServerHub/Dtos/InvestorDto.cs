
namespace Investigation.ServerHub.Dtos
{
    public class InvestorDto
    {
        public int Id { get; set; }
        public string Bio { get; set; }
        public string InvestArea { get; set; }
        public DateTime SinceWhen { get; set; }
        public bool IsLookingForCompany { get; set; }
        public string? EmailAddress { get; set; }
        public string? PhoneNumber { get; set; }
        public string? CoverImageUrl { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public DateTime? SuspendedDate { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;

        public string AppUserDtoId { get; set; }
        public int CountryDtoId { get; set; }
        public int InvestorCategoryDtoId { get; set; }

        public virtual AppUserDto AppUserDto { get; set; }
        public virtual CountryDto CountryDto { get; set; }
        public virtual InvestorCategoryDto InvestorCategoryDto { get; set; }

        public virtual ICollection<AnnouncementDto> AnnouncementsDto { get; set; }       
        public virtual ICollection<BlogDto> BlogsDto { get; set; }      
        public virtual ICollection<HitDto> HitsDto { get; set; }        
        public virtual ICollection<LikeDto> LikesDto { get; set; }       
        public virtual ICollection<RecentlyInvestDto> RecentlyInvestsDto { get; set; }       
        public virtual ICollection<PostDto> PostsDto { get; set; }       
        public virtual ICollection<ReportDto> ReportsDto { get; set; }       
        public virtual ICollection<SavedContentDto> SavedContentsDto { get; set; }        
        public virtual ICollection<SurveyDto> SurveysDto { get; set; }       
        public virtual ICollection<UserSocialMediaDto> UserSocialMediasDto { get; set; }

        public int AnnouncementCount { get; set; }
        public int BlogCount { get; set; }
        public int HitCount { get; set; }
        public int LikeCount { get; set; }
        public int RecentlyInvestCount { get; set; }
        public int PostCount { get; set; }
        public int ReportCount { get; set; }
        public int SavedContentCount { get; set; }
        public int SurveyCount { get; set; }
        public int UserSocialMediaCount { get; set; }
    }
}
