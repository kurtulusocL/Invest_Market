using Investigation.Domain.Entities;

namespace Investigation.Business.Services.Abstract
{
    public interface IAnnouncementCategoryService
    {
        IQueryable<AnnouncementCategory> GetAllIncludingAsync();
        IQueryable<AnnouncementCategory> GetAllIncludingByAnnouncementQuantityAsync();
        IQueryable<AnnouncementCategory> GetAllIncludingForAddAnnouncementAsync();
        IQueryable<AnnouncementCategory> GetAllIncludingForAdminAsync();
        Task<IEnumerable<AnnouncementCategory>> GetAllForSignalRAsync();
        Task<AnnouncementCategory> GetByIdAsync(int? id);
        Task<bool> CreateAsync(AnnouncementCategory entity);
        Task<bool> UpdateAsync(AnnouncementCategory entity);
        Task<bool> DeleteAsync(AnnouncementCategory entity, int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        IQueryable<AnnouncementCategory> GetAllIncludingForAdminHome();
        IQueryable<AnnouncementCategory> GetAllIncludingAnnouncementCategory();
    }
}
