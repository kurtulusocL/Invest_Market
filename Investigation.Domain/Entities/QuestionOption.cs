using Investigation.Shared.Domain.EntityFramework;

namespace Investigation.Domain.Entities
{
    public class QuestionOption : BaseEntity
    {
        public string OptionText { get; set; }
        public int OrderIndex { get; set; }

        public int SurveyQuestionId { get; set; }
        public virtual SurveyQuestion SurveyQuestion { get; set; }

        public virtual ICollection<SurveyAnswer> SurveyAnswers { get; set; }
    }
}
