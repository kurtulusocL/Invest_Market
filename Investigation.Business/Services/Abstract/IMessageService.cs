using Investigation.Domain.Entities;

namespace Investigation.Business.Services.Abstract
{
    public interface IMessageService
    {
        IQueryable<Message> GetAllIncludingAsync();
        IQueryable<Message> GetAllIncludingByReadAsync();
        IQueryable<Message> GetAllIncludingBySenderIdAsync(string senderId);
        IQueryable<Message> GetAllIncludingByRecieverIdAsync(string recieverId);
        IQueryable<Message> GetAllIncludingForAdminAsync();
        Task<IEnumerable<Message>> GetAllForSignalRAsync();
        Task<Message> GetByIdAsync(int? id);
        Task<bool> DeleteAsync(Message entity, int id);
        Task<bool> DeleteAllByIdAsync(List<int> ids);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
    }
}
