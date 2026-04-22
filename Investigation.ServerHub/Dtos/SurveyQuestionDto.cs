
namespace Investigation.ServerHub.Dtos
{
    public class SurveyQuestionDto
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public bool IsRequired { get; set; }
        public int OrderIndex { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public DateTime? SuspendedDate { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;

        public int? SurveyDtoId { get; set; }
        public virtual SurveyDto SurveyDto { get; set; }

        public virtual ICollection<QuestionOptionDto> QuestionOptionsDto { get; set; }
        public virtual ICollection<SurveyAnswerDto> SurveyAnswersDto { get; set; }

        public int QuestionOptionsCount { get; set; }
        public int SurveyAnswerCount { get; set; }
    }
}
