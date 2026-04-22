using Investigation.Domain.Entities;

namespace Investigation.Business.Services.Abstract
{
    public interface IBlockedMessageUserService
    {
        IQueryable<MessageUserBlockList> GetAllIncludingAsync();
        IQueryable<MessageUserBlockList> GetAllIncludingByBlockedAsync();
        IQueryable<MessageUserBlockList> GetAllIncludingByUnblockedAsync();
        IQueryable<MessageUserBlockList> GetAllIncludingByRemovedMessageUserAsync();
        IQueryable<MessageUserBlockList> GetAllIncludingByUnRemovedMessageUserAsync();
        IQueryable<MessageUserBlockList> GetAllIncludingByBlockedIdAsync(string blockedId);
        IQueryable<MessageUserBlockList> GetAllIncludingByBlockerIdAsync(string blockerId);
        IQueryable<MessageUserBlockList> GetAllIncludingForAdminAsync();
        Task<IEnumerable<MessageUserBlockList>> GetAllForSignalRAsync();
        Task<MessageUserBlockList> GetByIdAsync(int? id);
        Task<bool> DeleteAsync(MessageUserBlockList entity, int id);
        Task<bool> DeleteAllByIdAsync(List<int> ids);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
    }
}
