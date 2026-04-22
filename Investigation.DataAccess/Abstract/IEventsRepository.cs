using Investigation.Domain.Entities;
using Investigation.Shared.DataAccess;

namespace Investigation.DataAccess.Abstract
{
    public interface IEventsRepository : IEntityRepository<Events>
    {
        Task<Events?> GetBySlugAsync(string slug);
        Events HitRead(int id);
        Task<bool> LikeAsync(int id);
        Task<bool> SetOnlineAsync(int id);
        Task<bool> SetOfflineAsync(int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
    }
}
