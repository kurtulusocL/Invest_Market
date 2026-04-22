using Investigation.Domain.Entities;

namespace Investigation.Business.Services.Abstract
{
    public interface IDataPolicyService
    {
        IQueryable<DataPolicy> GetAllAsync();
        IQueryable<DataPolicy> GetAllForAdminAsync();
        Task<IEnumerable<DataPolicy>> GetAllForSignalRAsync();
        Task<DataPolicy> GetByIdAsync(int? id);
        Task<bool> CreateAsync(DataPolicy entity);
        Task<bool> UpdateAsync(DataPolicy entity);
        Task<bool> DeleteAsync(DataPolicy entity, int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        IQueryable<DataPolicy> GetAllForSitemap();
    }
}
