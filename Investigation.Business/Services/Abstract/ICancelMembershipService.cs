using Investigation.Domain.Entities;

namespace Investigation.Business.Services.Abstract
{
    public interface ICancelMembershipService
    {
        IQueryable<CancelMembership> GetAllIncludingAsync();
        IQueryable<CancelMembership> GetAllIncludingByCancelledMembershipAsync();
        IQueryable<CancelMembership> GetAllIncludingByNotCancelledMembershipAsync();
        IQueryable<CancelMembership> GetAllIncludingByCancelledRequestAsync();
        IQueryable<CancelMembership> GetAllIncludingBySeenRequestAsync();
        IQueryable<CancelMembership> GetAllIncludingByNotSeenRequestAsync();
        IQueryable<CancelMembership> GetAllIncludingByCancelMembershipCategoryIdAsync(int cancelMembershipCategoryId);
        IQueryable<CancelMembership> GetAllIncludingByUserIdAsync(string appUserId);
        IQueryable<CancelMembership> GetAllIncludingForAdminAsync();
        IQueryable<CancelMembership> GetAllIncludingCancelMembershipForUserByUserIdAsync(string userId);
        Task<IEnumerable<CancelMembership>> GetAllForSignalRAsync();
        Task<CancelMembership> GetByIdAsync(int? id);
        Task<bool> CreateAsync(string title, string desc, int cancelMembershipCategoryId, string appUserId);
        Task<bool> DeleteAsync(CancelMembership entity, int id);
        Task<bool> SetAccountCancelAsync(int id);
        Task<bool> SetAccountNotCancelAsync(int id);
        Task<bool> SetRequestCancelAsync(int id);
        Task<bool> SetRequestNotCancelAsync(int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        CancelMembership ReadNonUniqueHit(int id);
    }
}
