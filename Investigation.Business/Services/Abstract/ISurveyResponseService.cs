using Investigation.Domain.Entities;

namespace Investigation.Business.Services.Abstract
{
    public interface ISurveyResponseService
    {
        IQueryable<SurveyResponse> GetAllIncludingAsync();
        IQueryable<SurveyResponse> GetAllIncludingByStartedDateAsync();
        IQueryable<SurveyResponse> GetAllIncludingByCompletedDateAsync();
        IQueryable<SurveyResponse> GetAllIncludingBySurveyIdAsync(int? surveyId);
        IQueryable<SurveyResponse> GetAllIncludingByUserIdAsync(string appUserId);
        IQueryable<SurveyResponse> GetAllIncludingForAdminAsync();
        Task<IEnumerable<SurveyResponse>> GetAllForSignalRAsync();
        Task<SurveyResponse> GetByIdAsync(int? id);
        Task<bool> DeleteAsync(SurveyResponse entity, int id);
        Task<bool> DeleteAllByIdAsync(List<int> ids);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        int SurveyResponseCounter();
    }
}
