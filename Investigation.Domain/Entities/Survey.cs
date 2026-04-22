using Investigation.Domain.Entities.UserEntities;
using Investigation.Shared.Domain.EntityFramework;

namespace Investigation.Domain.Entities
{
    public class Survey : BaseEntity
    {
        public string Title { get; set; }
        public string Desc { get; set; }
        public bool IsOnline { get; set; } = true;
        public DateTime StartDate { get; set; }
        public DateTime ClosedDate { get; set; }
        public bool IsAnonymous { get; set; } = false;
        public bool AllowMultipleResponses { get; set; } = false;

        public string AppUserId { get; set; }
        public int? CompanyId { get; set; }
        public int? InvestorId { get; set; }

        public virtual AppUser AppUser { get; set; }
        public virtual Company Company { get; set; }
        public virtual Investor Investor { get; set; }

        public virtual ICollection<Hit> Hits { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
        public virtual ICollection<SurveyAnalytics> SurveyAnalytics { get; set; }
        public virtual ICollection<SurveyQuestion> SurveyQuestions { get; set; }
        public virtual ICollection<SurveyResponse> SurveyResponses { get; set; }       
        public virtual ICollection<SavedContent> SavedContents { get; set; }
    }
}
