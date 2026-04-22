using Investigation.Shared.Domain.EntityFramework;

namespace Investigation.Domain.Entities
{
    public class SurveyAnalytics : BaseEntity
    {
        public string AnalyticsDataJson { get; set; }
        public int TotalResponses { get; set; }
        public decimal CompletionRate { get; set; }
        public int AverageCompletionTimeSeconds { get; set; }
        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;

        public int SurveyId { get; set; }
        public virtual Survey Survey { get; set; }
    }
}
