using Investigation.Domain.Entities;
using Investigation.Shared.DataAccess;

namespace Investigation.DataAccess.Abstract
{
    public interface ISurveyQuestionRepository : IEntityRepository<SurveyQuestion>
    {
        Task<SurveyQuestion?> GetBySlugAsync(string slug);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
    }
}
