using Investigation.Domain.Entities.UserEntities;
using Investigation.Shared.Domain.EntityFramework;

namespace Investigation.Domain.Entities
{
    public class SurveyResponse : BaseEntity
    {
        public DateTime StartedAt { get; set; } = DateTime.UtcNow;
        public DateTime? CompletedAt { get; set; }
        public bool IsCompleted { get; set; } = false;

        public int? SurveyId { get; set; }
        public string AppUserId { get; set; }

        public virtual Survey Survey { get; set; }
        public virtual AppUser AppUser { get; set; }

        public virtual ICollection<SurveyAnswer> SurveyAnswers { get; set; }
    }
}
