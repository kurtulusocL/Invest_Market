using Investigation.Domain.Entities.UserEntities;
using Investigation.Shared.Domain.EntityFramework;

namespace Investigation.Domain.Entities
{
    public class SurveyAnswer : BaseEntity
    {
        public string AppUserId { get; set; }
        public int? SurveyResponseId { get; set; }
        public int? SurveyQuestionId { get; set; }
        public int? QuestionOptionId { get; set; }

        public virtual AppUser AppUser { get; set; }
        public virtual SurveyResponse SurveyResponse { get; set; }
        public virtual SurveyQuestion SurveyQuestion { get; set; }
        public virtual QuestionOption QuestionOption { get; set; }
    }
}
