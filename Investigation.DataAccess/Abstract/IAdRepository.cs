using Investigation.Domain.Entities;
using Investigation.Shared.DataAccess;

namespace Investigation.DataAccess.Abstract
{
    public interface IAdRepository : IEntityRepository<Ad>
    {
        IEnumerable<Ad> GetAllPersonalizedAdsForUser(string userId);       
        Ad ReadNonUniqueHit(int? id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
    }
}
