using Investigation.Domain.Entities;
using Investigation.Shared.DataAccess;

namespace Investigation.DataAccess.Abstract
{
    public interface IVisibilitySettingRepository : IEntityRepository<VisibilitySetting>
    {
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
    }
}
