
namespace Investigation.ServerHub.Dtos
{
    public class QuestionOptionDto
    {
        public int Id { get; set; }
        public string OptionText { get; set; }
        public int OrderIndex { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public DateTime? SuspendedDate { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;

        public int SurveyQuestionDtoId { get; set; }
        public virtual SurveyQuestionDto SurveyQuestionDto { get; set; }

        public virtual ICollection<SurveyAnswerDto> SurveyAnswersDto { get; set; }
        public int SurveyAnswerCount { get; set; }
    }
}
