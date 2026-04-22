using Investigation.Domain.Entities;
using Investigation.Shared.DataAccess;

namespace Investigation.DataAccess.Abstract
{
    public interface IRecentlyInvestRepository : IEntityRepository<RecentlyInvest>
    {
        Task<bool> SetHasExitInvestAsync(int id);
        Task<bool> SetHasNotExitInvestAsync(int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
    }
}
