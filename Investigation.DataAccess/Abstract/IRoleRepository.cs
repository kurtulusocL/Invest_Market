using Investigation.Domain.Entities.UserEntities;
using Investigation.Shared.DataAccess;
using Investigation.Shared.Dtos.UserDto;

namespace Investigation.DataAccess.Abstract
{
    public interface IRoleRepository : IEntityRepository<AppRole>
    {
        Task<IEnumerable<RoleUserCountDto>> GetAllUserCountsByRoleAsync(CancellationToken cancellationToken = default);
        Task<bool> SetActiveAsync(string id);
        Task<bool> SetDeActiveAsync(string id);
        Task<bool> SetDeletedAsync(string id);
        Task<bool> SetNotDeletedAsync(string id);
    }
}
