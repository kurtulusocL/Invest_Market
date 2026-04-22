using System.Linq.Expressions;
using Investigation.Business.Services.Abstract;
using Investigation.DataAccess.Abstract;
using Investigation.Domain.Entities;

namespace Investigation.Business.Services.Concrete
{
    public class BlogCategoryManager : IBlogCategoryService
    {
        readonly IBlogCategoryRepository _blogCategoryRepository;
        public BlogCategoryManager(IBlogCategoryRepository blogCategoryRepository)
        {
            _blogCategoryRepository = blogCategoryRepository;
        }

        public async Task<bool> CreateAsync(BlogCategory entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var result = await _blogCategoryRepository.AddAsync(entity);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> DeleteAsync(BlogCategory entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var data = await _blogCategoryRepository.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _blogCategoryRepository.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public async Task<IEnumerable<BlogCategory>> GetAllForSignalRAsync()
        {
            try
            {
                var data = await _blogCategoryRepository.GetAllIncludeAsync(new Expression<Func<BlogCategory, bool>>[]
                {
                   
                }, null, y => y.Blogs);
                return data.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<BlogCategory>();
            }
        }

        public IQueryable<BlogCategory> GetAllForSiteMap()
        {
            try
            {
                return _blogCategoryRepository.GetAll(i => i.IsDeleted == false && i.IsActive == true).OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<BlogCategory>().AsQueryable();
            }
        }

        public IQueryable<BlogCategory> GetAllIncludingAsync()
        {
            try
            {
                var data = _blogCategoryRepository.GetAllInclude(new Expression<Func<BlogCategory, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Blogs);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<BlogCategory>().AsQueryable();
            }
        }

        public IQueryable<BlogCategory> GetAllIncludingBlogCategories()
        {
            try
            {
                return _blogCategoryRepository.GetAllInclude(new Expression<Func<BlogCategory, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Blogs.Count()>0
                }, null, y => y.Blogs).OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<BlogCategory>().AsQueryable();
            }
        }

        public IQueryable<BlogCategory> GetAllIncludingByBlogQuantityAsync()
        {
            try
            {
                var data = _blogCategoryRepository.GetAllInclude(new Expression<Func<BlogCategory, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Blogs);
                return data.OrderByDescending(i => i.Blogs.Count());
            }
            catch (Exception)
            {
                return Enumerable.Empty<BlogCategory>().AsQueryable();
            }
        }

        public IQueryable<BlogCategory> GetAllIncludingForAddBlogAsync()
        {
            try
            {
                var data = _blogCategoryRepository.GetAllInclude(new Expression<Func<BlogCategory, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Blogs);
                return data.OrderBy(i => i.Name);
            }
            catch (Exception)
            {
                return Enumerable.Empty<BlogCategory>().AsQueryable();
            }
        }

        public IQueryable<BlogCategory> GetAllIncludingForAdminAsync()
        {
            try
            {
                var data = _blogCategoryRepository.GetAllInclude(new Expression<Func<BlogCategory, bool>>[]
                {

                }, null, y => y.Blogs);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<BlogCategory>().AsQueryable();
            }
        }

        public IQueryable<BlogCategory> GetAllIncludingForAdminHome()
        {
            try
            {
                return _blogCategoryRepository.GetAllInclude(new Expression<Func<BlogCategory, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Blogs).OrderByDescending(i => i.Blogs.Count());
            }
            catch (Exception)
            {
                return Enumerable.Empty<BlogCategory>().AsQueryable();
            }
        }

        public async Task<BlogCategory> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                return await _blogCategoryRepository.GetIncludeAsync(i => i.Id == id, y => y.Blogs);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _blogCategoryRepository.SetActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _blogCategoryRepository.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _blogCategoryRepository.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _blogCategoryRepository.SetNotDeletedAsync(id);
            return result;
        }

        public async Task<bool> UpdateAsync(BlogCategory entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                entity.UpdatedDate = DateTime.UtcNow;
                var result = await _blogCategoryRepository.UpdateAsync(entity);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while updating the entity.", ex);
            }
        }
    }
}
