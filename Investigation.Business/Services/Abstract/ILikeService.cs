using Investigation.Domain.Entities;
using Investigation.Domain.Entities.UserEntities;

namespace Investigation.Business.Services.Abstract
{
    public interface ILikeService
    {
        IQueryable<Like> GetAllIncludingAsync();
        IQueryable<Like> GetAllIncludingByMostLikedValueAsync();
        IQueryable<Like> GetAllIncludingByLessLikedValueAsync();
        IQueryable<Like> GetAllIncludingByMostDisLikedValueAsync();
        IQueryable<Like> GetAllIncludingByLessDisLikedValueAsync();
        IQueryable<Like> GetAllIncludingByBlogIdAsync(int? blogId);
        IQueryable<Like> GetAllIncludingByCommentIdAsync(int? commentId);
        IQueryable<Like> GetAllIncludingByCommentAnswerIdAsync(int? commentAnswerId);
        IQueryable<Like> GetAllIncludingByCompanyIdAsync(int? companyId);
        IQueryable<Like> GetAllIncludingByInvestorIdAsync(int? investorId);
        IQueryable<Like> GetAllIncludingByPostIdAsync(int? postId);
        IQueryable<Like> GetAllIncludingBySurveyIdAsync(int? surveyId);
        IQueryable<Like> GetAllIncludingByUserIdAsync(string appUserId);
        IQueryable<Like> GetAllIncludingForAdminAsync();
        IQueryable<Like> GetAllIncludingLikesForUserByUserIdAsync(string userId);
        IQueryable<Like> GetAllIncludingLikesForLikeOwnerByUserIdAsync(string userId);
        IQueryable<AppUser> GetAllIncludingLikedContentsPeopleForOwnerByContentIdAsync(int? blogId = null, int? postId = null, int? companyId = null, int? investorId = null, int? surveyId = null, int? commentId = null, int? commentAnswerId = null);
        Task<IEnumerable<Like>> GetAllForSignalRAsync();
        Task<Like> GetByIdAsync(int? id);
        Task<bool> BlogLikeAsync(int? blogId, string appUserId, int currentValue, bool isLiked);
        Task<bool> BlogDisLikeAsync(int? blogId, string appUserId, int currentValue, bool isLiked);
        Task<bool> CommentLikeAsync(int? commentId, string appUserId, int currentValue, bool isLiked);
        Task<bool> CommentDisLikeAsync(int? commentId, string appUserId, int currentValue, bool isLiked);
        Task<bool> CommentAnswerLikeAsync(int? commentAnswerId, string appUserId, int currentValue, bool isLiked);
        Task<bool> CommentAnswerDisLikeAsync(int? commentAnswerId, string appUserId, int currentValue, bool isLiked);
        Task<bool> CompanyLikeAsync(int? companyId, string appUserId, int currentValue, bool isLiked);
        Task<bool> CompanyDisLikeAsync(int? companyId, string appUserId, int currentValue, bool isLiked);
        Task<bool> InvestorLikeAsync(int? investorId, string appUserId, int currentValue, bool isLiked);
        Task<bool> InvestorDisLikeAsync(int? investorId, string appUserId, int currentValue, bool isLiked);
        Task<bool> PostLikeAsync(int? postId, string appUserId, int currentValue, bool isLiked);
        Task<bool> PostDisLikeAsync(int? postId, string appUserId, int currentValue, bool isLiked);
        Task<bool> SurveyLikeAsync(int? surveyId, string appUserId, int currentValue, bool isLiked);
        Task<bool> SurveyDisLikeAsync(int? surveyId, string appUserId, int currentValue, bool isLiked);
        Task<bool> DeleteAsync(Like entity, int id);
        Task<bool> DeleteAllByIdAsync(List<int> ids);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        IQueryable<Like> GetAllIncludingCompanyLikesPeopleByCompanyId(string userId);
        int LikeCounter();
    }
}