using Investigation.Domain.Entities;
using Investigation.Shared.DataAccess;

namespace Investigation.DataAccess.Abstract
{
    public interface IPostRepository : IEntityRepository<Post>
    {
        Task<Post?> GetBySlugAsync(string slug);
        int PostCounter();
        Task<bool> SetCommentablePostAsync(int id);
        Task<bool> SetNotCommentablePostAsync(int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
    }
}
