using Investigation.Shared.Domain.EntityFramework;

namespace Investigation.Domain.Entities
{
    public class SurveyQuestion : BaseEntity
    {
        public string QuestionText { get; set; }
        public bool IsRequired { get; set; }
        public int OrderIndex { get; set; }

        public int? SurveyId { get; set; }
        public virtual Survey Survey { get; set; }

        public virtual ICollection<QuestionOption> QuestionOptions { get; set; }
        public virtual ICollection<SurveyAnswer> SurveyAnswers { get; set; }
    }
}
