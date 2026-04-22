using Investigation.Domain.Entities;

namespace Investigation.Business.Services.Abstract
{
    public interface ISecuritySettingService
    {
        IQueryable<SecuritySetting> GetAllAsync();
        IQueryable<SecuritySetting> GetAllForAdminAsync();
        IQueryable<SecuritySetting> GetAllByStaticExtensionsAsync();
        IQueryable<SecuritySetting> GetAllByBlockedAgentAsync();
        Task<IEnumerable<SecuritySetting>> GetAllForSignalRAsync();
        Task<SecuritySetting>GetByIdAsync(int? id);
        Task<bool> CreateAsync(SecuritySetting entity);
        Task<bool> UpdateAsync(SecuritySetting entity);
        Task<bool> DeleteAsync(SecuritySetting entity, int id);
        Task<bool> DeleteAllByIdAsync(List<int> ids);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
    }
}
