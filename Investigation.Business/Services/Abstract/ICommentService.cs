using Investigation.Domain.Entities;

namespace Investigation.Business.Services.Abstract
{
    public interface ICommentService
    {
        IQueryable<Comment> GetAllIncludingAsync();
        IQueryable<Comment> GetAllIncludingByTodaysCommentsAsync();
        IQueryable<Comment> GetAllIncludingByUserIdAsync(string appUserId);
        IQueryable<Comment> GetAllIncludingByCompanyIdAsync(int? companyId);
        IQueryable<Comment> GetAllIncludingByBlogIdAsync(int? blogId);
        IQueryable<Comment> GetAllIncludingByPostIdAsync(int? postId);
        IQueryable<Comment> GetAllIncludingForAdminAsync();
        IQueryable<Comment> GetAllIncludingCommentForUserByUserIdAsync(string userId);
        IQueryable<Comment> GetAllIncludingCommentsForCommentOwnerByUserIdAsync(string userId);
        Task<IEnumerable<Comment>> GetAllForSignalRAsync();
        Task<Comment> GetByIdAsync(int? id);
        Task<bool> CreateBlogCommentAsync(string text, int? blogId, string appUserId);
        Task<bool> CreatePostCommentAsync(string text, int? postId, string appUserId);
        Task<bool> CreateCompanyCommentAsync(string text, int? companyId, string appUserId);
        Task<bool> DeleteAsync(Comment entity, int id);
        Task<bool> DeleteAllByIdAsync(List<int> ids);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        IQueryable<Comment> GetAllIncludingTodaysCommentForAdminHeader();
        IQueryable<Comment> GetAllIncludingInvestorBlogCommentsByBlogId(int? blogId);
        IQueryable<Comment> GetAllIncludingInvestorPostCommentByPostId(int? postId);
        IQueryable<Comment> GetAllIncludingCompanyPostCommentByPostId(int? postId);
        IQueryable<Comment> GetAllIncludingCompanyBlogCommentByBlogId(int? blogId);
        IQueryable<Comment> GetAllIncludingCompanyCommentByCompanyId(int? companyId);
        Comment GetCommentForFormById(int? commentId);
        int CommentCounter();
    }
}
