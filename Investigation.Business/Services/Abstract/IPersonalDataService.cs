using Investigation.Domain.Entities;

namespace Investigation.Business.Services.Abstract
{
    public interface IPersonalDataService
    {
        IQueryable<PersonalData> GetAllAsync();
        IQueryable<PersonalData> GetAllForAdminAsync();
        Task<IEnumerable<PersonalData>> GetAllForSignalRAsync();
        Task<PersonalData> GetByIdAsync(int? id);
        Task<bool> CreateAsync(PersonalData entity);
        Task<bool> UpdateAsync(PersonalData entity);
        Task<bool> DeleteAsync(PersonalData entity, int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        IQueryable<PersonalData> GetAllForSitemap();
    }
}
