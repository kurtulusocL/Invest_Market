
namespace Investigation.ServerHub.Dtos
{
    public class SurveyAnalyticsDto
    {
        public int Id { get; set; }
        public string AnalyticsDataJson { get; set; }
        public int TotalResponses { get; set; }
        public decimal CompletionRate { get; set; }
        public int AverageCompletionTimeSeconds { get; set; }
        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public DateTime? SuspendedDate { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;

        public int SurveyDtoId { get; set; }
        public virtual SurveyDto SurveyDto { get; set; }

    }
}
