using Investigation.Domain.Entities;

namespace Investigation.Business.Services.Abstract
{
    public interface IQuestionOptionService
    {
        IQueryable<QuestionOption> GetAllIncludingAsync();
        IQueryable<QuestionOption> GetAllIncludingBySurveyQuestionIdAsync(int surveyQuestionId);
        IQueryable<QuestionOption> GetAllIncludingForAdminAsync();
        Task<IEnumerable<QuestionOption>> GetAllForSignalRAsync();
        Task<QuestionOption> GetByIdAsync(int? id);
        Task<bool> CreateAsync(string optionText, int orderIndex, int surveyQuestionId);
        Task<bool> UpdateAsync(string optionText, int orderIndex, int surveyQuestionId, int id);
        Task<bool> DeleteAsync(QuestionOption entity, int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        IQueryable<QuestionOption> GetAllQuestionOptionsForSurveyVoteBySurveyQuestionId(int surveyQuestionId);
    }
}
