using Investigation.Domain.Entities;

namespace Investigation.Business.Services.Abstract
{
    public interface ISendMessageService
    {
        IQueryable<SendMessage> GetAllAsync();
        IQueryable<SendMessage> GetAllForAdminAsync();
        Task<IEnumerable<SendMessage>> GetAllForSignalRAsync();
        Task<SendMessage> GetByIdAsync(int? id);
        Task<bool> CreateAsync(string nameSurname, string email, string phoneNumber, string messageTitle, string messageSubject, string messageContent);
        Task<bool> DeleteAsync(SendMessage entity, int id);
        Task<bool> DeleteAllByIdAsync(List<int> ids);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
    }
}
