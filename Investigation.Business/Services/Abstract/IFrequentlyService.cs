using Investigation.Domain.Entities;

namespace Investigation.Business.Services.Abstract
{
    public interface IFrequentlyService
    {
        IQueryable<Frequently> GetAllAsync();
        IQueryable<Frequently> GetAllForAdminAsync();
        Task<IEnumerable<Frequently>> GetAllForSignalRAsync();
        Task<Frequently> GetByIdAsync(int? id);
        Task<bool> CreateAsync(Frequently entity);
        Task<bool> UpdateAsync(Frequently entity);
        Task<bool> DeleteAsync(Frequently entity, int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        IQueryable<Frequently> GetAllFrequentlyForPublic();
        IQueryable<Frequently> GetAllForSitemap();
    }
}
