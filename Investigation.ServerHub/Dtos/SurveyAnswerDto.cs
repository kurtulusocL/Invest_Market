
namespace Investigation.ServerHub.Dtos
{
    public class SurveyAnswerDto
    {
        public int Id { get; set; }
        public string AppUserDtoId { get; set; }
        public int? SurveyResponseDtoId { get; set; }
        public int? SurveyQuestionDtoId { get; set; }
        public int? QuestionOptionDtoId { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public DateTime? SuspendedDate { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;

        public virtual AppUserDto AppUserDto { get; set; }
        public virtual SurveyResponseDto SurveyResponseDto { get; set; }
        public virtual SurveyQuestionDto SurveyQuestionDto { get; set; }
        public virtual QuestionOptionDto QuestionOptionDto { get; set; }
    }
}
