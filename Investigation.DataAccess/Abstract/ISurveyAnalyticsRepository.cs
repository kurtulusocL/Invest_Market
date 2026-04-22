using Investigation.Domain.Entities;
using Investigation.Shared.DataAccess;

namespace Investigation.DataAccess.Abstract
{
    public interface ISurveyAnalyticsRepository : IEntityRepository<SurveyAnalytics>
    {
        Task<SurveyAnalytics> GetIncludingClosedSurveyDataBySurveyIdAsync(int surveyId);
        Task LoadSurveyQuestionsAndOptionsAsync(SurveyAnalytics analytics);
        Task UpdateSurveyAnalyticsAsync(int surveyId);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
    }
}
