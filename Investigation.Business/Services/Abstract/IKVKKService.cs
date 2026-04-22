using Investigation.Domain.Entities;

namespace Investigation.Business.Services.Abstract
{
    public interface IKVKKService
    {
        IQueryable<KVKK> GetAllAsync();
        IQueryable<KVKK> GetAllForAdminAsync();
        Task<IEnumerable<KVKK>> GetAllForSignalRAsync();
        Task<KVKK> GetByIdAsync(int? id);
        Task<bool> CreateAsync(KVKK entity);
        Task<bool> UpdateAsync(KVKK entity);
        Task<bool> DeleteAsync(KVKK entity, int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        IQueryable<KVKK> GetAllForSitemap();
    }
}
