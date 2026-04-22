using Investigation.Domain.Entities;

namespace Investigation.Business.Services.Abstract
{
    public interface ICommentAnswerService
    {
        IQueryable<CommentAnswer> GetAllIncludingAsync();
        IQueryable<CommentAnswer> GetAllIncludingByUserIdAsync(string appUserId);
        IQueryable<CommentAnswer> GetAllIncludingByCommentIdAsync(int? commentId);
        IQueryable<CommentAnswer> GetAllIncludingForAdminAsync();
        IQueryable<CommentAnswer> GetAllIncludingCommentAnswersForUserByUserIdAsync(string userId);
        IQueryable<CommentAnswer> GetAllIncludingCommentAnswersForUserByCommentIdAsync(int? commentId);
        IQueryable<CommentAnswer> GetAllIncludingCommentAnswersForCommentAnswerOwnerByUserIdAsync(string userId);
        Task<IEnumerable<CommentAnswer>> GetAllForSignalRAsync();
        Task<CommentAnswer> GetByIdAsync(int? id);
        Task<bool> CreateAsync(string text, int? commentId, string appUserId);
        Task<bool> DeleteAsync(CommentAnswer entity, int id);
        Task<bool> DeleteAllByIdAsync(List<int> ids);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        bool Create(string text, int? commentId, string appUserId);
        int CommentAnswerCounter();
        IQueryable<CommentAnswer> GetAllIncludingCommentAnswersForUserByUserId(string userId);
        IQueryable<CommentAnswer> GetAllIncludingCommentAnswersForUserByCommentId(int? commentId);
        IQueryable<CommentAnswer> GetAllIncludingCommentAnswersByCommentId(int? commentId);
    }
}
