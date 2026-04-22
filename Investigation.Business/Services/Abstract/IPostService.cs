using Investigation.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Investigation.Business.Services.Abstract
{
    public interface IPostService
    {
        IQueryable<Post> GetAllIncludingAsync();
        IQueryable<Post> GetAllIncludingCommentablesAsync();
        IQueryable<Post> GetAllIncludingNotCommentablesAsync();
        IQueryable<Post> GetAllIncludingByCompanyIdAsync(int? companyId);
        IQueryable<Post> GetAllIncludingByInvestorIdAsync(int? investorId);
        IQueryable<Post> GetAllIncludingByUserIdAsync(string appUserId);
        IQueryable<Post> GetAllIncludingForAdminAsync();
        IQueryable<Post> GetAllIncludingPostForInvestorByInvestorIdAsync(int? investorId);
        IQueryable<Post> GetAllIncludingPostForCompanyByCompanyIdAsync(int? companyId);
        IQueryable<Post> GetAllIncludingMostLikedPostsAsync();
        IQueryable<Post> GetAllIncludingMostSavedPostsAsync();
        IQueryable<Post> GetAllIncludingMostHitPostsAsync();
        IQueryable<Post> GetAllIncludingPostTodayAsync();
        Task<IEnumerable<Post>> GetAllForSignalRAsync();
        Task<Post> GetByIdAsync(int? id);
        Task<Post?> GetBySlugAsync(string slug);
        Task<bool> CreateCompanyPostAsync(string text, bool isCommentable, int? companyId, string appUserId, IFormFile? image);
        Task<bool> CreateInvestorPostAsync(string text, bool isCommentable, int? investorId, string appUserId, IFormFile? image);
        Task<bool> UpdateCompanyPostAsync(string text, bool isCommentable, int? companyId, string appUserId, IFormFile? image, int id);
        Task<bool> UpdateInvestorPostAsync(string text, bool isCommentable, int? investorId, string appUserId, IFormFile? image, int id);
        Task<bool> DeleteAsync(Post entity, int id);
        Task<bool> DeleteAllByIdAsync(List<int> ids);
        Task<bool> SetCommentablePostAsync(int id);
        Task<bool> SetNotCommentablePostAsync(int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        int PostCounter();
        IQueryable<Post> GetAllIncludingPostForInvestorByInvestorId(int? investorId);
        IQueryable<Post> GetAllIncludingPostForCompanyByCompanyId(int? companyId);
        IQueryable<Post> GetAllIncludingPopularPosts();
        IQueryable<Post> GetAllIncludingLastPostForIndex();
        IQueryable<Post> GetAllIncludingLastPostForTimeline();
        IQueryable<Post> GetAllIncludingPostForInvestorDetail(int? investorId);
        IQueryable<Post> GetAllIncludingPostForCompanyDetail(int? companyId);
        Post GetPostForFormById(int? id);
    }
}
