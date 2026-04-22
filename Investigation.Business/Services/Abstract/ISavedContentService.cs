using Investigation.Domain.Entities;
using Investigation.Domain.Entities.UserEntities;

namespace Investigation.Business.Services.Abstract
{
    public interface ISavedContentService
    {
        IQueryable<SavedContent> GetAllIncludingAsync();
        IQueryable<SavedContent> GetAllIncludingByDisSavedAsync();
        IQueryable<SavedContent> GetAllIncludingByDisSavedByUserIdAsync(string appUserId);
        IQueryable<SavedContent> GetAllIncludingByUserIdAsync(string appUserId);
        IQueryable<SavedContent> GetAllIncludingSavedByBlogIdAsync(int? blogId);
        IQueryable<SavedContent> GetAllIncludingSavedBySectorNewsIdAsync(int? sectorNewsId);
        IQueryable<SavedContent> GetAllIncludingSavedByCompanyIdAsync(int? companyId);
        IQueryable<SavedContent> GetAllIncludingSavedByInvestorIdAsync(int? investorId);
        IQueryable<SavedContent> GetAllIncludingSavedByPostIdAsync(int? postId);
        IQueryable<SavedContent> GetAllIncludingSavedBySurveyIdAsync(int? surveyId);
        IQueryable<SavedContent> GetAllIncludingNotSavedByBlogIdAsync(int? blogId);
        IQueryable<SavedContent> GetAllIncludingNotSavedBySectorNewsIdAsync(int? sectorNewsId);
        IQueryable<SavedContent> GetAllIncludingNotSavedByCompanyIdAsync(int? companyId);
        IQueryable<SavedContent> GetAllIncludingNotSavedByInvestorIdAsync(int? investorId);
        IQueryable<SavedContent> GetAllIncludingNotSavedByPostIdAsync(int? postId);
        IQueryable<SavedContent> GetAllIncludingNotSavedBySurveyIdAsync(int? surveyId);
        IQueryable<SavedContent> GetAllIncludingForAdminAsync();
        IQueryable<SavedContent> GetAllIncludingSavedContentsForUserByUserIdAsync(string userId);
        IQueryable<SavedContent> GetAllIncludingSavedContentsForSavedContentOwnerByUserIdAsync(string userId);
        IQueryable<AppUser> GetAllIncludingSavedContentsPeopleForOwnerByContentIdAsync(int? blogId = null, int? postId = null, int? companyId = null, int? investorId = null, int? surveyId = null);
        Task<IEnumerable<SavedContent>> GetAllForSignalRAsync();
        Task<SavedContent> GetByIdAsync(int? id);
        Task<bool> SaveBlogAsync(bool isSaved, int? blogId, string appUserId);
        Task<bool> SaveSectorNewsAsync(bool isSaved, int? sectorNewsId, string appUserId);
        Task<bool> SaveCompanyAsync(bool isSaved, int? companyId, string appUserId);
        Task<bool> SaveInvestorAsync(bool isSaved, int? investorId, string appUserId);
        Task<bool> SavePostAsync(bool isSaved, int? postId, string appUserId);
        Task<bool> SaveSurveyAsync(bool isSaved, int? surveyId, string appUserId);
        Task<bool> NotSaveBlogAsync(bool isSaved, int? blogId, string appUserId);
        Task<bool> NotSaveSectorNewsAsync(bool isSaved, int? sectorNewsId, string appUserId);
        Task<bool> NotSaveCompanyAsync(bool isSaved, int? companyId, string appUserId);
        Task<bool> NotSaveInvestorAsync(bool isSaved, int? investorId, string appUserId);
        Task<bool> NotSavePostAsync(bool isSaved, int? postId, string appUserId);
        Task<bool> NotSaveSurveyAsync(bool isSaved, int? surveyId, string appUserId);
        Task<bool> DeleteAsync(SavedContent entity, int id);
        Task<bool> DeleteAllByIdAsync(List<int> ids);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        IQueryable<SavedContent> GetAllIncludingCompanySavedsPeopleByCompanyId(string userId);
        int SavedContentCounter();
    }
}
