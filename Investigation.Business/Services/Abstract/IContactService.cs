using Investigation.Domain.Entities;

namespace Investigation.Business.Services.Abstract
{
    public interface IContactService
    {
        IQueryable<Contact> GetAllIncludingAsync();
        IQueryable<Contact> GetAllIncludingForAdminAsync();
        Task<IEnumerable<Contact>> GetAllForSignalRAsync();
        Task<Contact> GetByIdAsync(int? id);
        Task<bool> CreateAsync(Contact entity);
        Task<bool> UpdateAsync(Contact entity);
        Task<bool> DeleteAsync(Contact entity, int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        IQueryable<Contact> GetAllForSitemap();
        IQueryable<Contact> GetAllContactForUser();
    }
}
