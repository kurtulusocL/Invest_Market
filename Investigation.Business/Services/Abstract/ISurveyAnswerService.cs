using Investigation.Domain.Entities;

namespace Investigation.Business.Services.Abstract
{
    public interface ISurveyAnswerService
    {
        IQueryable<SurveyAnswer> GetAllIncludingAsync();
        IQueryable<SurveyAnswer> GetAllIncludingBySurveyResponseIdAsync(int? surveyResponseId);
        IQueryable<SurveyAnswer> GetAllIncludingBySurveyQuestionOptionIdAsync(int? questionOptionId);
        IQueryable<SurveyAnswer> GetAllIncludingBySurveyQuestionIdAsync(int? surveyQuestionId);
        IQueryable<SurveyAnswer> GetAllIncludingByUserIdAsync(string appUserId);
        IQueryable<SurveyAnswer> GetAllIncludingForAdminAsync();
        Task<IEnumerable<SurveyAnswer>> GetAllForSignalRAsync();
        Task<SurveyAnswer> GetByIdAsync(int? id);
        Task<bool> DeleteAsync(SurveyAnswer entity, int id);
        Task<bool> DeleteAllByIdAsync(List<int> ids);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        int SurveyAnswerCounter();
    }
}
