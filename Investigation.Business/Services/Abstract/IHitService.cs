using Investigation.Domain.Entities;
using Investigation.Domain.Entities.UserEntities;

namespace Investigation.Business.Services.Abstract
{
    public interface IHitService
    {
        IQueryable<Hit> GetAllIncludingAsync();
        IQueryable<Hit> GetAllIncludingByMostHitValueAsync();
        IQueryable<Hit> GetAllIncludingByLessHitValueAsync();
        IQueryable<Hit> GetAllIncludingByUserIdAsync(string appUserId);
        IQueryable<Hit> GetAllIncludingByAdIdAsync(int? adId);
        IQueryable<Hit> GetAllIncludingByAnnouncementIdAsync(int? announcementId);
        IQueryable<Hit> GetAllIncludingByBlogIdAsync(int? blogId);
        IQueryable<Hit> GetAllIncludingByCommentIdAsync(int? commentId);
        IQueryable<Hit> GetAllIncludingByCommentAnswerIdAsync(int? commentAnswerId);
        IQueryable<Hit> GetAllIncludingByCompanyIdAsync(int? companyId);
        IQueryable<Hit> GetAllIncludingByCompanyFinanceIdAsync(int? companyFinanceId);
        IQueryable<Hit> GetAllIncludingByCompanyPintechIdAsync(int? companyPintechId);
        IQueryable<Hit> GetAllIncludingByCompanyStageIdAsync(int? companyStageId);
        IQueryable<Hit> GetAllIncludingByInvestorIdAsync(int? investorId);
        IQueryable<Hit> GetAllIncludingByPostIdAsync(int? postId);
        IQueryable<Hit> GetAllIncludingBySurveyIdAsync(int? surveyId);
        IQueryable<Hit> GetAllIncludingForAdminAsync();
        IQueryable<Hit> GetAllIncludingHitsForUserByUserIdAsync(string userId);
        IQueryable<Hit> GetAllIncludingHitsForHitOwnerByUserIdAsync(string userId);
        IQueryable<AppUser> GetAllIncludingHitContentsPeopleForOwnerByContentIdAsync(int? blogId = null, int? postId = null, int? companyId = null, int? investorId = null, int? surveyId = null, int? commentId = null, int? commentAnswerId = null, int? announcementId = null, int? companyFinanceId = null, int? companyStageId = null, int? companyPintechId = null);
        Task<IEnumerable<Hit>> GetAllForSignalRAsync();
        Task<Hit> GetByIdAsync(int? id);
        Hit AdHit(int? id, string appUserId, int currentValue);
        Hit AnnouncementHit(int? id, string appUserId, int currentValue);
        Hit BlogHit(int? id, string appUserId, int currentValue);
        Hit CommentHit(int? id, string appUserId, int currentValue);
        Hit CommentAnswerHit(int? id, string appUserId, int currentValue);
        Hit CompanyHit(int? id, string appUserId, int currentValue);
        Hit CompanyFinanceHit(int? id, string appUserId, int currentValue);
        Hit CompanyPintechHit(int? id, string appUserId, int currentValue);
        Hit CompanyStageHit(int? id, string appUserId, int currentValue);
        Hit InvestorHit(int? id, string appUserId, int currentValue);
        Hit PostHit(int? id, string appUserId, int currentValue);
        Hit SurveyHit(int? id, string appUserId, int currentValue);
        Task<bool> DeleteAsync(Hit entity, int id);
        Task<bool> DeleteAllByIdAsync(List<int> ids);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        IQueryable<Hit> GetAllIncludingCompanyHitsPeopleByCompanyId(string userId);
        int HitCounter();
    }
}
