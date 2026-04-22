using Investigation.Domain.Entities;

namespace Investigation.Business.Services.Abstract
{
    public interface ICancelMembershipCategoryService
    {
        IQueryable<CancelMembershipCategory> GetAllIncludingAsync();
        IQueryable<CancelMembershipCategory> GetAllIncludingByCancelMembershipQuantityAsync();
        IQueryable<CancelMembershipCategory> GetAllIncludingForAddCancelMembershipAsync();
        IQueryable<CancelMembershipCategory> GetAllIncludingForAdminAsync();
        Task<IEnumerable<CancelMembershipCategory>> GetAllForSignalRAsync();
        Task<CancelMembershipCategory> GetByIdAsync(int? id);
        Task<bool> CreateAsync(CancelMembershipCategory entity);
        Task<bool> UpdateAsync(CancelMembershipCategory entity);
        Task<bool> DeleteAsync(CancelMembershipCategory entity, int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        IQueryable<CancelMembershipCategory> GetAllIncludingForAdminHome();
    }
}
