using Investigation.Domain.Entities;

namespace Investigation.Business.Services.Abstract
{
    public interface ILayoutInfoService
    {
        IQueryable<LayoutInfo> GetAllAsync();
        IQueryable<LayoutInfo> GetAllForAdminAsync();
        Task<IEnumerable<LayoutInfo>> GetAllForSignalRAsync();
        Task<LayoutInfo> GetByIdAsync(int? id);
        Task<bool> CreateAsync(LayoutInfo entity);
        Task<bool> UpdateAsync(LayoutInfo entity);
        Task<bool> DeleteAsync(LayoutInfo entity, int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        IQueryable<LayoutInfo> GetAllForSitemap();
        IQueryable<LayoutInfo> GetAllForShared();
    }
}
