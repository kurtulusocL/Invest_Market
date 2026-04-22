
namespace Investigation.ServerHub.Dtos
{
    public class CompanyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slogan { get; set; }
        public string ShortBio { get; set; }
        public string Desc { get; set; }
        public DateTime FoundationDate { get; set; }
        public bool IsLookingForInvest { get; set; }
        public string LinkedIn { get; set; }
        public string? GitHub { get; set; }
        public string LogoUrl { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public DateTime? SuspendedDate { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;

        public string AppUserDtoId { get; set; }
        public int CompanyCategoryDtoId { get; set; }
        public int CountryDtoId { get; set; }
        public int SectorDtoId { get; set; }
        public int? SubSectorDtoId { get; set; }

        public virtual AppUserDto AppUserDto { get; set; }
        public virtual CompanyCategoryDto CompanyCategoryDto { get; set; }
        public virtual CountryDto CountryDto { get; set; }
        public virtual SectorDto SectorDto { get; set; }
        public virtual SubSectorDto SubSectorDto { get; set; }

        public virtual ICollection<AnnouncementDto> AnnouncementsDto { get; set; }
        public virtual ICollection<BlogDto> BlogsDto { get; set; }
        public virtual ICollection<CompanyContactDto> CompanyContactsDto { get; set; }
        public virtual ICollection<CompanyFinanceDto> CompanyFinancesDto { get; set; }
        public virtual ICollection<CompanyPintechDto> CompanyPintechesDto { get; set; }
        public virtual ICollection<CompanyStageDto> CompanyStagesDto { get; set; }
        public virtual ICollection<CompanyTeamDto> CompanyTeamsDto { get; set; }
        public virtual ICollection<CommentDto> CommentsDto { get; set; }
        public virtual ICollection<HitDto> HitsDto { get; set; }
        public virtual ICollection<LikeDto> LikesDto { get; set; }
        public virtual ICollection<PictureDto> PicturesDto { get; set; }
        public virtual ICollection<PostDto> PostsDto { get; set; }
        public virtual ICollection<ReportDto> ReportsDto { get; set; }
        public virtual ICollection<SavedContentDto> SavedContentsDto { get; set; }
        public virtual ICollection<SurveyDto> SurveysDto { get; set; }
        public virtual ICollection<UserSocialMediaDto> UserSocialMediasDto { get; set; }

        public int AnnouncementCount { get; set; }
        public int BlogCount { get; set; }
        public int CompanyContactCount { get; set; }
        public int CompanyFinanceCount { get; set; }
        public int CompanyPintechCount { get; set; }
        public int CompanyStageCount { get; set; }
        public int CompanyTeamCount { get; set; }
        public int CommentCount { get; set; }
        public int HitCount { get; set; }
        public int LikeCount { get; set; }
        public int PictureCount { get; set; }
        public int PostCount { get; set; }
        public int ReportCount { get; set; }
        public int SavedContentCount { get; set; }
        public int SurveyCount { get; set; }
        public int UserSocialMediaCount { get; set; }
    }
}
