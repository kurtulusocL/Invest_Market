using Investigation.Domain.Entities.UserEntities;

namespace Investigation.Business.Services.Abstract
{
    public interface IUserService
    {
        IQueryable<AppUser> GetAllIncludingAsync();
        IQueryable<AppUser> GetAllIncludingUserByCountryAsync();
        IQueryable<AppUser> GetAllIncludingByAdminAsync();
        IQueryable<AppUser> GetAllIncludingByCompanyAsync();
        IQueryable<AppUser> GetAllIncludingByInvestorAsync();
        IQueryable<AppUser> GetAllIncludingByDeletedAdminAsync();
        IQueryable<AppUser> GetAllIncludingBySuspendedAdminAsync();
        IQueryable<AppUser> GetAllIncludingByUserAsync();
        IQueryable<AppUser> GetAllIncludingByDeletedUserAsync();
        IQueryable<AppUser> GetAllIncludingBySuspendedUserAsync();
        IQueryable<AppUser> GetAllIncludingByActiveLoginConfirmCodeAdminAsync();
        IQueryable<AppUser> GetAllIncludingByActiveRegisterConfirmCodeAdminAsync();
        IQueryable<AppUser> GetAllIncludingByActiveLoginConfirmCodeUserAsync();
        IQueryable<AppUser> GetAllIncludingByActiveRegisterConfirmCodeUserAsync();
        IQueryable<AppUser> GetAllIncludingForManagementAsync();
        Task<IEnumerable<AppUser>> GetAllIncludingMostPopularEntrepreneursAsync();
        Task<IEnumerable<AppUser>> GetAllIncludingUnPopularEntrepreneursAsync();
        IQueryable<AppUser> GetAllIncludingEntrepreneursAsync();
        IQueryable<AppUser> GetAllIncludingEntrepreneurTodayAsync();
        IQueryable<AppUser> GetAllIncludingSearchResult(string key);
        Task<IEnumerable<AppUser>> GetAllForSignalRAsync();
        Task<AppUser> GetByIdAsync(string id);
        Task<AppUser?> GetBySlugAsync(string slug);
        Task<AppUser> GetInvestorForProfileByUserId(string userId);
        Task<AppUser> GetCompanyForProfileByUserId(string userId);
        Task<AppUser> GetUserProfileByIdAsync(string userId);
        Task<bool> DeleteAsync(AppUser entity, string id);
        Task<bool> DeleteAllByIdAsync(List<string> ids);
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
        IQueryable<AppUser> GetAllIncludeLastJoinedUserForAdminHome();
        IQueryable<AppUser> GetAllIncludingTodaysUsersForAdminHeader();
        IQueryable<AppUser> GetAllIncludingLastEntrepreneur();
        IEnumerable<AppUser> GetAllIncludingMostPopularEntrepreneurs();
        AppUser GetUserById(string appUserId);
        int UserCounter();
        Task<AppUser?> GetCurrentUserAsync();
        Guid? GetCurrentUserId();
    }
}
