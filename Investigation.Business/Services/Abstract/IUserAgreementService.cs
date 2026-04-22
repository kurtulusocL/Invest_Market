using Investigation.Domain.Entities;

namespace Investigation.Business.Services.Abstract
{
    public interface IUserAgreementService
    {
        IQueryable<UserAgreement> GetAllAsync();
        IQueryable<UserAgreement> GetAllForAdminAsync();
        Task<IEnumerable<UserAgreement>> GetAllForSignalRAsync();
        Task<UserAgreement> GetByIdAsync(int? id);
        Task<bool> CreateAsync(UserAgreement entity);
        Task<bool> UpdateAsync(UserAgreement entity);
        Task<bool> DeleteAsync(UserAgreement entity, int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        IQueryable<UserAgreement> GetAllForSitemap();
    }
}
