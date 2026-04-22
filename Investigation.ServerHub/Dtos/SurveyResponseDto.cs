
namespace Investigation.ServerHub.Dtos
{
    public class SurveyResponseDto
    {
        public int Id { get; set; }
        public DateTime StartedAt { get; set; } = DateTime.UtcNow;
        public DateTime? CompletedAt { get; set; }
        public bool IsCompleted { get; set; } = false;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public DateTime? SuspendedDate { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;

        public int? SurveyDtoId { get; set; }
        public string AppUserDtoId { get; set; }

        public virtual SurveyDto SurveyDto { get; set; }
        public virtual AppUserDto AppUserDto { get; set; }

        public virtual ICollection<SurveyAnswerDto> SurveyAnswersDto { get; set; }
        public int SurveyAnswerCount { get; set; }
    }
}
