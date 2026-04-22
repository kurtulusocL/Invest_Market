using Investigation.Domain.Entities;

namespace Investigation.Business.Services.Abstract
{
    public interface IBlogCategoryService
    {
        IQueryable<BlogCategory> GetAllIncludingAsync();
        IQueryable<BlogCategory> GetAllIncludingByBlogQuantityAsync();
        IQueryable<BlogCategory> GetAllIncludingForAddBlogAsync();
        IQueryable<BlogCategory> GetAllIncludingForAdminAsync();
        Task<IEnumerable<BlogCategory>> GetAllForSignalRAsync();
        Task<BlogCategory> GetByIdAsync(int? id);
        Task<bool> CreateAsync(BlogCategory entity);
        Task<bool> UpdateAsync(BlogCategory entity);
        Task<bool> DeleteAsync(BlogCategory entity, int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        IQueryable<BlogCategory> GetAllForSiteMap();
        IQueryable<BlogCategory> GetAllIncludingForAdminHome();
        IQueryable<BlogCategory> GetAllIncludingBlogCategories();
    }
}
