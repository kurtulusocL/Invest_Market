using Investigation.Domain.Entities;

namespace Investigation.Business.Services.Abstract
{
    public interface ISurveyQuestionService
    {
        IQueryable<SurveyQuestion> GetAllIncludingAsync();
        IQueryable<SurveyQuestion> GetAllIncludingByAnswerQuantityAsync();
        IQueryable<SurveyQuestion> GetAllIncludingBySurveyIdAsync(int? surveyId);
        IQueryable<SurveyQuestion> GetAllIncludingForAdminAsync();
        IQueryable<SurveyQuestion> GetAllIncludingQuestionForVoteBySurveyIdAsync(int? surveyId);
        Task<SurveyQuestion> GetIncludingQuestionForVoteBySurveyIdAsync(int? surveyId);
        Task<IEnumerable<SurveyQuestion>> GetAllForSignalRAsync();
        Task<SurveyQuestion> GetByIdAsync(int? id);
        Task<bool> CreateAsync(string questionText, int orderIndex, bool isRequired, int? surveyId);
        Task<bool> UpdateAsync(string questionText, int orderIndex, bool isRequired, int? surveyId, int id);
        Task<bool> DeleteAsync(SurveyQuestion entity, int id);
        Task<bool> DeleteAllByIdAsync(List<int> ids);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        SurveyQuestion GetIncludingQuestionForVoteBySurveyId(int? surveyId);
    }
}
