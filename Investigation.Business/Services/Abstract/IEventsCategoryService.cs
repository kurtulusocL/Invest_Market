using Investigation.Domain.Entities;

namespace Investigation.Business.Services.Abstract
{
    public interface IEventsCategoryService
    {
        IQueryable<EventsCategory> GetAllIncludingAsync();
        IQueryable<EventsCategory> GetAllIncludingByEventsQuantityAsync();
        IQueryable<EventsCategory> GetAllIncludingForAddEventsAsync();
        IQueryable<EventsCategory> GetAllIncludingForAdminAsync();
        Task<IEnumerable<EventsCategory>> GetAllForSignalRAsync();
        Task<EventsCategory> GetByIdAsync(int? id);
        Task<bool> CreateAsync(EventsCategory entity);
        Task<bool> UpdateAsync(EventsCategory entity);
        Task<bool> DeleteAsync(EventsCategory entity, int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        IQueryable<EventsCategory> GetAllIncludingEventsCategory();
        IQueryable<EventsCategory> GetAllForSitemap();
    }
}
