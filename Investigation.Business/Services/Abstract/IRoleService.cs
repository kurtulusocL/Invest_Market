using Investigation.Domain.Entities.UserEntities;
using Investigation.Shared.Dtos.UserDto;

namespace Investigation.Business.Services.Abstract
{
    public interface IRoleService
    {
        IQueryable<AppRole> GetAllAsync();
        Task<IEnumerable<RoleUserCountDto>> GetAllUserCountsByRoleAsync();
        IQueryable<AppRole> GetAllForAdminAsync();
        Task<IEnumerable<AppRole>> GetAllForSignalRAsync();
        Task<AppRole> GetByIdAsync(string id);
        Task<bool> CreateAsync(AppRole entity);
        Task<bool> UpdateAsync(AppRole entity);
        Task<bool> DeleteAsync(AppRole entity, string id);
        Task<bool> SetActiveAsync(string id);
        Task<bool> SetDeActiveAsync(string id);
        Task<bool> SetDeletedAsync(string id);
        Task<bool> SetNotDeletedAsync(string id);
    }
}
