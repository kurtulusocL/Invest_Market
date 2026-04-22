using Investigation.Domain.Entities;

namespace Investigation.Business.Services.Abstract
{
    public interface IUserSessionService
    {
        IQueryable<UserSession> GetAllIncludingAsync();
        IQueryable<UserSession> GetAllIncludingByCurrentlyOnlineAsync();
        IQueryable<UserSession> GetAllIncludingByLogoutDateAsync();
        IQueryable<UserSession> GetAllIncludingByUserIdAsync(string appuserId);
        IQueryable<UserSession> GetAllIncludingForAdminAsync();
        Task<IEnumerable<UserSession>> GetAllForSignalRAsync();
        Task<UserSession> GetByIdAsync(int? id);
        Task<bool> DeleteAsync(UserSession entity, int id);
        Task<bool> DeleteAllByIdAsync(List<int> ids);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        IQueryable<UserSession> GetAllIncludingForAdminHome();
    }
}
