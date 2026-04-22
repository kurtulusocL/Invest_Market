using System.Linq.Expressions;
using Investigation.Business.Services.Abstract;
using Investigation.DataAccess.Abstract;
using Investigation.Domain.Entities;

namespace Investigation.Business.Services.Concrete
{
    public class EventsCategoryManager : IEventsCategoryService
    {
        readonly IEventsCategoryRepository _eventsCategoryRepository;
        public EventsCategoryManager(IEventsCategoryRepository eventsCategoryRepository)
        {
            _eventsCategoryRepository = eventsCategoryRepository;
        }

        public async Task<bool> CreateAsync(EventsCategory entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var result = await _eventsCategoryRepository.AddAsync(entity);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> DeleteAsync(EventsCategory entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "Entit was null");

                var data = await _eventsCategoryRepository.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _eventsCategoryRepository.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public async Task<IEnumerable<EventsCategory>> GetAllForSignalRAsync()
        {
            try
            {
                var data = await _eventsCategoryRepository.GetAllIncludeAsync(new Expression<Func<EventsCategory, bool>>[]
                {
                   
                }, null, y => y.Events);
                return data.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<EventsCategory>();
            }
        }

        public IQueryable<EventsCategory> GetAllForSitemap()
        {
            try
            {
                return _eventsCategoryRepository.GetAll(i => i.IsActive == true && i.IsDeleted == false).OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<EventsCategory>().AsQueryable();
            }
        }

        public IQueryable<EventsCategory> GetAllIncludingAsync()
        {
            try
            {
                var data = _eventsCategoryRepository.GetAllInclude(new Expression<Func<EventsCategory, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Events);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<EventsCategory>().AsQueryable();
            }
        }

        public IQueryable<EventsCategory> GetAllIncludingByEventsQuantityAsync()
        {
            try
            {
                var data = _eventsCategoryRepository.GetAllInclude(new Expression<Func<EventsCategory, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Events);
                return data.OrderByDescending(i => i.Events.Count());
            }
            catch (Exception)
            {
                return Enumerable.Empty<EventsCategory>().AsQueryable();
            }
        }

        public IQueryable<EventsCategory> GetAllIncludingEventsCategory()
        {
            try
            {
                return _eventsCategoryRepository.GetAllInclude(new Expression<Func<EventsCategory, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Events.Count()>0
                }, null, y => y.Events).OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<EventsCategory>().AsQueryable();
            }
        }

        public IQueryable<EventsCategory> GetAllIncludingForAddEventsAsync()
        {
            try
            {
                var data = _eventsCategoryRepository.GetAllInclude(new Expression<Func<EventsCategory, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Events);
                return data.OrderByDescending(i => i.Events.Count());
            }
            catch (Exception)
            {
                return Enumerable.Empty<EventsCategory>().AsQueryable();
            }
        }

        public IQueryable<EventsCategory> GetAllIncludingForAdminAsync()
        {
            try
            {
                var data = _eventsCategoryRepository.GetAllInclude(new Expression<Func<EventsCategory, bool>>[]
                {

                }, null, y => y.Events);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<EventsCategory>().AsQueryable();
            }
        }

        public async Task<EventsCategory> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                return await _eventsCategoryRepository.GetIncludeAsync(i => i.Id == id, y => y.Events);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _eventsCategoryRepository.SetActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _eventsCategoryRepository.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _eventsCategoryRepository.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _eventsCategoryRepository.SetNotDeletedAsync(id);
            return result;
        }

        public async Task<bool> UpdateAsync(EventsCategory entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                entity.UpdatedDate = DateTime.UtcNow;
                var result = await _eventsCategoryRepository.UpdateAsync(entity);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while updating the entity.", ex);
            }
        }
    }
}
