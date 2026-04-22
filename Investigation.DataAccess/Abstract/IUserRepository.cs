using Investigation.Domain.Entities.UserEntities;
using Investigation.Shared.DataAccess;

namespace Investigation.DataAccess.Abstract
{
    public interface IUserRepository : IEntityRepository<AppUser>
    {
        Task<AppUser?> GetBySlugAsync(string slug);
        Task<IEnumerable<AppUser>> GetAllIncludingMostPopularEntrepreneursAsync();
        Task<IEnumerable<AppUser>> GetAllIncludingUnPopularEntrepreneursAsync();
        IEnumerable<AppUser> GetAllIncludingMostPopularEntrepreneurs();
        int UserCounter();
        Task<bool> SetActiveLoginConfirmCodeAsync(string id); 
        Task<bool> SetDeActiveLoginConfirmCodeAsync(string id);
        Task<bool> SetActiveRegisterConfirmCodeAsync(string id);
        Task<bool> SetDeActiveRegisterConfirmCodeAsync(string id);
        Task<bool> SetFollowableAsync(string id);
        Task<bool> SetNotFollowableAsync(string id);
        Task<bool> SetActiveAsync(string id);
        Task<bool> SetDeActiveAsync(string id);
        Task<bool> SetDeletedAsync(string id);
        Task<bool> SetNotDeletedAsync(string id);
        Task<AppUser?> GetCurrentUserAsync();
        Guid? GetCurrentUserId();
    }
}
