using Investigation.Domain.Entities;

namespace Investigation.Business.Services.Abstract
{
    public interface ISurveyAnalyticsService
    {
        IQueryable<SurveyAnalytics> GetAllIncludingAsync();
        IQueryable<SurveyAnalytics> GetAllIncludingByTotalResponseAsync();
        IQueryable<SurveyAnalytics> GetAllIncludingByCompletionRateAsync();
        IQueryable<SurveyAnalytics> GetAllIncludingBySurveyIdAsync(int surveyId);
        IQueryable<SurveyAnalytics> GetAllIncludingClosedSurveyDataBySurveyIdAsync(int surveyId);
        IQueryable<SurveyAnalytics> GetAllIncludingForAdminAsync();
        Task<SurveyAnalytics> GetIncludingClosedSurveyDataBySurveyIdAsync(int surveyId);
        Task<IEnumerable<SurveyAnalytics>> GetAllForSignalRAsync();
        Task UpdateSurveyAnalyticsAsync(int surveyId);
        Task<SurveyAnalytics> GetByIdAsync(int? id);
        Task<bool> DeleteAsync(SurveyAnalytics entity, int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        SurveyAnalytics GetSurveyInformationForSurveyAnalyticBySurveyId(int surveyId);
    }
}
