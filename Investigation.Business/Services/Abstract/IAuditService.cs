using Investigation.Domain.Entities;

namespace Investigation.Business.Services.Abstract
{
    public interface IAuditService
    {
        IQueryable<Audit> GetAllIncludingAsync();
        IQueryable<Audit> GetAllIncludingByUserIdAsync(string userId);
        IQueryable<Audit> GetAllIncludingByVisitorAuditAsync();
        IQueryable<Audit> GetAllIncludingByMobileAsync();
        IQueryable<Audit> GetAllIncludingForAdminAsync();
        Task<IEnumerable<Audit>> GetAllForSignalRAsync();
        Task<Audit> GetByIdAsync(int? id);
        Task<bool> DeleteAsync(Audit entity, int id);
        Task<bool> DeleteAllAsync(List<int> ids);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
    }
}
