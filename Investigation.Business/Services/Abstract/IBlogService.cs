using Investigation.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Investigation.Business.Services.Abstract
{
    public interface IBlogService
    {
        IQueryable<Blog> GetAllIncludingAsync();
        IQueryable<Blog> GetAllIncludingByBlogCategoryIdAsync(int blogCategoryId);
        IQueryable<Blog>  GetAllIncludingByUserIdAsync(string appUserId);
        IQueryable<Blog> GetAllIncludingByInvestorIdAsync(int? investorId);
        IQueryable<Blog> GetAllIncludingByCompanyIdAsync(int? companyId);
        IQueryable<Blog> GetAllIncludingByMostLikedBlogAsync();
        IQueryable<Blog> GetAllIncludingMostByHitBlogAsync();
        IQueryable<Blog> GetAllIncludingyMostSavedBlogAsync();
        IQueryable<Blog> GetAllIncludingForAdminAsync();
        IQueryable<Blog> GetAllIncludingBlogForInvestorByInvestorIdAsync(int? investorId);
        IQueryable<Blog> GetAllIncludingBlogForCompanyByCompanyIdAsync(int? companyId);
        IQueryable<Blog> GetAllIncludingBlogsForPublicUser();
        IQueryable<Blog> GetAllIncludingMostLikedBlogsAsync();
        IQueryable<Blog> GetAllIncludingMostSavedBlogsAsync();
        IQueryable<Blog> GetAllIncludingMostHitBlogsAsync();
        IQueryable<Blog> GetAllIncludingBlogTodayAsync();
        Task<IEnumerable<Blog>> GetAllForSignalRAsync();
        Task<Blog> GetByIdAsync(int? id);
        Task<Blog?> GetBySlugAsync(string slug);
        Task<Blog?> GetBySlugForPublicBlogDetailAsync(string slug);
        Task<Blog> GetPublicBlogByIdAsync(int? id);
        Task<bool> CreateInvestorBlogAsync(string title, string subtitle, string? detail, string content, int blogCategoryId, int? investorId, string appUserId, IFormFile image);
        Task<bool> CreateCompanyBlogAsync(string title, string subtitle, string? detail, string content, int blogCategoryId, int? companyId, string appUserId, IFormFile image);
        Task<bool> UpdateInvestorBlogAsync(string title, string subtitle, string? detail, string content, int blogCategoryId, int? investorId, string appUserId, IFormFile image, int id);
        Task<bool> UpdateCompanyBlogAsync(string title, string subtitle, string? detail, string content, int blogCategoryId, int? companyId, string appUserId, IFormFile image, int id);
        Task<bool> DeleteAsync(Blog entity, int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        IQueryable<Blog> GetAllForSitemap();
        IQueryable<Blog> GetAllIncludingPopularBlog();
        IQueryable<Blog> GetAllIncludingLastBlogForIndex();
        IQueryable<Blog> GetAllIncludingLastBlogForTimeline();
        IQueryable<Blog> GetAllIncludingBlogForInvestorDetail(int? investorId);
        IQueryable<Blog> GetAllIncludingBlogByCompanyId(int? companyId);
        Blog GetBlogForFormById(int? id);
        int BlogCounter();
    }
}
