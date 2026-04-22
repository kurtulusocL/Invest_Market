using Investigation.Domain.Entities;
using Investigation.Shared.DataAccess;

namespace Investigation.DataAccess.Abstract
{
    public interface ISurveyRepository : IEntityRepository<Survey>
    {
        Task<Survey?> GetBySlugAsync(string slug);
        Task<bool> SubmitSurveyAnswersAsync(int surveyId, Dictionary<int, int> answers);
        Task<IEnumerable<Survey>> GetAllIncludingMostPopularSurveysAsync();
        Task<IEnumerable<Survey>> GetAllIncludingLessPopularSurveysAsync();
        int SurveyCounter();
        Task<bool> SetCurrentlyOnlineSurveyAsync(int id);
        Task<bool> SetOfflineSurveyAsync(int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
    }
}
