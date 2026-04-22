using Investigation.Domain.Entities;

namespace Investigation.Business.Services.Abstract
{
    public interface IExceptionLoggerService
    {
        IQueryable<ExceptionLogger> GetAllAsync();
        IQueryable<ExceptionLogger> GetAllForAdminAsync();
        Task<IEnumerable<ExceptionLogger>> GetAllForSignalRAsync();
        Task<ExceptionLogger> GetByIdAsync(int? id);
        Task<bool> DeleteAsync(ExceptionLogger entity, int id);
        Task<bool> DeleteAllByIdAsync(List<int> ids);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
    }
}
