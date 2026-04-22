using System.Linq.Expressions;
using Investigation.Business.Services.Abstract;
using Investigation.DataAccess.Abstract;
using Investigation.Domain.Entities;

namespace Investigation.Business.Services.Concrete
{
    public class AnnouncementCategoryManager : IAnnouncementCategoryService
    {
        readonly IAnnouncementCategoryRepository _announcementCategoryService;
        public AnnouncementCategoryManager(IAnnouncementCategoryRepository announcementCategoryRepository)
        {
            _announcementCategoryService = announcementCategoryRepository;
        }

        public async Task<bool> CreateAsync(AnnouncementCategory entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var result = await _announcementCategoryService.AddAsync(entity);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> DeleteAsync(AnnouncementCategory entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var data = await _announcementCategoryService.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _announcementCategoryService.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public async Task<IEnumerable<AnnouncementCategory>> GetAllForSignalRAsync()
        {
            try
            {
                var data = await _announcementCategoryService.GetAllIncludeAsync(new Expression<Func<AnnouncementCategory, bool>>[]
                {

                }, null, y => y.Announcements);
                return data.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<AnnouncementCategory>();
            }
        }

        public IQueryable<AnnouncementCategory> GetAllIncludingAnnouncementCategory()
        {
            try
            {
                return _announcementCategoryService.GetAllInclude(new Expression<Func<AnnouncementCategory, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Announcements.Count()>0
                }, null, y => y.Announcements).OrderByDescending(i => i.Announcements.Count());
            }
            catch (Exception)
            {
                return Enumerable.Empty<AnnouncementCategory>().AsQueryable();
            }
        }

        public IQueryable<AnnouncementCategory> GetAllIncludingAsync()
        {
            try
            {
                var data = _announcementCategoryService.GetAllInclude(new Expression<Func<AnnouncementCategory, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Announcements);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<AnnouncementCategory>().AsQueryable();
            }
        }

        public IQueryable<AnnouncementCategory> GetAllIncludingByAnnouncementQuantityAsync()
        {
            try
            {
                var data = _announcementCategoryService.GetAllInclude(new Expression<Func<AnnouncementCategory, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Announcements);
                return data.OrderByDescending(i => i.Announcements.Count());
            }
            catch (Exception)
            {
                return Enumerable.Empty<AnnouncementCategory>().AsQueryable();
            }
        }

        public IQueryable<AnnouncementCategory> GetAllIncludingForAddAnnouncementAsync()
        {
            try
            {
                var data = _announcementCategoryService.GetAllInclude(new Expression<Func<AnnouncementCategory, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Announcements);
                return data.OrderBy(i => i.Name);
            }
            catch (Exception)
            {
                return Enumerable.Empty<AnnouncementCategory>().AsQueryable();
            }
        }

        public IQueryable<AnnouncementCategory> GetAllIncludingForAdminAsync()
        {
            try
            {
                var data = _announcementCategoryService.GetAllInclude(new Expression<Func<AnnouncementCategory, bool>>[]
                {

                }, null, y => y.Announcements);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<AnnouncementCategory>().AsQueryable();
            }
        }

        public IQueryable<AnnouncementCategory> GetAllIncludingForAdminHome()
        {
            try
            {
                return _announcementCategoryService.GetAllInclude(new Expression<Func<AnnouncementCategory, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Announcements).OrderByDescending(i => i.Announcements.Count());
            }
            catch (Exception)
            {
                return Enumerable.Empty<AnnouncementCategory>().AsQueryable();
            }
        }

        public async Task<AnnouncementCategory> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                return await _announcementCategoryService.GetIncludeAsync(i => i.Id == id, y => y.Announcements);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _announcementCategoryService.SetActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _announcementCategoryService.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _announcementCategoryService.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _announcementCategoryService.SetNotDeletedAsync(id);
            return result;
        }

        public async Task<bool> UpdateAsync(AnnouncementCategory entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                entity.UpdatedDate = DateTime.UtcNow;
                var result = await _announcementCategoryService.UpdateAsync(entity);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while updating the entity.", ex);
            }
        }
    }
}
