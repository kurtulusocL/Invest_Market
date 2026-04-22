using Investigation.Domain.Entities;
using Investigation.Shared.DataAccess;

namespace Investigation.DataAccess.Abstract
{
    public interface ICancelMembershipRepository : IEntityRepository<CancelMembership>
    {
        CancelMembership ReadNonUniqueHit(int id);
        Task<bool> SetAccountCancelAsync(int id);
        Task<bool> SetAccountNotCancelAsync(int id);
        Task<bool> SetRequestCancelAsync(int id);
        Task<bool> SetRequestNotCancelAsync(int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
    }
}
