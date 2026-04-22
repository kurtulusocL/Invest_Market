
namespace Investigation.ServerHub.Dtos
{
    public class SurveyDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public bool IsOnline { get; set; } = true;
        public DateTime StartDate { get; set; }
        public DateTime ClosedDate { get; set; }
        public bool IsAnonymous { get; set; } = false;
        public bool AllowMultipleResponses { get; set; } = false;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public DateTime? SuspendedDate { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;

        public string AppUserDtoId { get; set; }
        public int? CompanyDtoId { get; set; }
        public int? InvestorDtoId { get; set; }

        public virtual AppUserDto AppUserDto { get; set; }
        public virtual CompanyDto CompanyDto { get; set; }
        public virtual InvestorDto InvestorDto { get; set; }

        public virtual ICollection<HitDto> HitsDto { get; set; }      
        public virtual ICollection<LikeDto> LikesDto { get; set; }       
        public virtual ICollection<ReportDto> ReportsDto { get; set; }       
        public virtual ICollection<SurveyAnalyticsDto> SurveyAnalyticsDto { get; set; }        
        public virtual ICollection<SurveyQuestionDto> SurveyQuestionsDto { get; set; }       
        public virtual ICollection<SurveyResponseDto> SurveyResponsesDto { get; set; }       
        public virtual ICollection<SavedContentDto> SavedContentsDto { get; set; }

        public int HitCount { get; set; }
        public int LikeCount { get; set; }
        public int ReportCount { get; set; }
        public int SurveyAnalyicsCount { get; set; }
        public int SurveyQuestionCount { get; set; }
        public int SurveyResponseCount { get; set; }
        public int SavedContentCount { get; set; }

    }
}
