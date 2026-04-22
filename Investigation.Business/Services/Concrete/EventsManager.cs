using System.Linq.Expressions;
using Investigation.Business.Services.Abstract;
using Investigation.DataAccess.Abstract;
using Investigation.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Investigation.Business.Services.Concrete
{
    public class EventsManager : IEventsService
    {
        readonly IEventsRepository _eventsRepository;
        public EventsManager(IEventsRepository eventsRepository)
        {
            _eventsRepository = eventsRepository;
        }

        public async Task<bool> CreateAsync(Events entity, IFormFile? image)
        {
            try
            {
                var errors = new List<string>();
                if (image != null)
                {
                    var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/img/events/");
                    if (!Directory.Exists(directoryPath))
                    {
                        Console.WriteLine($"Path is preparing: {directoryPath}");
                        Directory.CreateDirectory(directoryPath);
                    }
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                    var filePath = Path.Combine(directoryPath, fileName);
                    try
                    {
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await image.CopyToAsync(stream);
                        }
                        entity.ImageUrl = fileName;
                        var result = await _eventsRepository.AddAsync(entity);
                        if (!result)
                        {
                            errors.Add($"Error {fileName}.");
                        }
                        return true;
                    }
                    catch (Exception ex)
                    {
                        errors.Add($"Error {fileName} : {ex.Message}");
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> DeleteAllAsync(List<int> ids)
        {
            try
            {
                if (ids == null || !ids.Any())
                    throw new ArgumentNullException(nameof(ids), "id list was null or empty");

                var result = await _eventsRepository.DeleteByIdsAsync(ids.Cast<object>());
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while bulk deleting entities.", ex);
            }
        }

        public async Task<bool> DeleteAsync(Events entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var data = await _eventsRepository.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _eventsRepository.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public IQueryable<Events> GetAllIncludingAsync()
        {
            try
            {
                var data = _eventsRepository.GetAllInclude(new Expression<Func<Events, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.EventsCategory, y => y.EventsParticipants);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Events>().AsQueryable();
            }
        }

        public IQueryable<Events> GetAllIncludingByEndDateAsync()
        {
            try
            {
                var data = _eventsRepository.GetAllInclude(new Expression<Func<Events, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.EventsCategory, y => y.EventsParticipants);
                return data.OrderByDescending(i => i.EndDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Events>().AsQueryable();
            }
        }

        public IQueryable<Events> GetAllIncludingByOfflineEventsAsync()
        {
            try
            {
                var data = _eventsRepository.GetAllInclude(new Expression<Func<Events, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsOnline==false
                }, null, y => y.EventsCategory, y => y.EventsParticipants);
                return data.OrderByDescending(i => i.EndDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Events>().AsQueryable();
            }
        }

        public IQueryable<Events> GetAllIncludingByOnlineEventsAsync()
        {
            try
            {
                var data = _eventsRepository.GetAllInclude(new Expression<Func<Events, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsOnline==true
                }, null, y => y.EventsCategory, y => y.EventsParticipants);
                return data.OrderByDescending(i => i.EndDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Events>().AsQueryable();
            }
        }

        public IQueryable<Events> GetAllIncludingByStartDateAsync()
        {
            try
            {
                var data = _eventsRepository.GetAllInclude(new Expression<Func<Events, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.EventsCategory, y => y.EventsParticipants);
                return data.OrderByDescending(i => i.StartedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Events>().AsQueryable();
            }
        }

        public IQueryable<Events> GetAllIncludingEventsForSitemap()
        {
            try
            {
                return _eventsRepository.GetAllInclude(new Expression<Func<Events, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.EventsCategory).OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Events>().AsQueryable();
            }
        }

        public IQueryable<Events> GetAllIncludingForAdminAsync()
        {
            try
            {
                var data = _eventsRepository.GetAllInclude(new Expression<Func<Events, bool>>[]
                {

                }, null, y => y.EventsCategory, y => y.EventsParticipants);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Events>().AsQueryable();
            }
        }

        public IQueryable<Events> GetAllIncludingForEventsCategoryIdAsync(int eventsCategoryId)
        {
            try
            {
                var data = _eventsRepository.GetAllIncludeById(eventsCategoryId, "EventsCategoryId", new Expression<Func<Events, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.EventsCategory, y => y.EventsParticipants);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Events>().AsQueryable();
            }
        }

        public IQueryable<Events> GetAllIncludingOpenEventsByCategoryIdAsync(int eventsCategoryId)
        {
            try
            {
                var data = _eventsRepository.GetAllIncludeById(eventsCategoryId, "EventsCategoryId", new Expression<Func<Events, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsOnline==true
                }, y => y.EventsCategory, y => y.EventsParticipants);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Events>().AsQueryable();
            }
        }

        public IQueryable<Events> GetAllIncludingUpComingEvents()
        {
            try
            {
                return _eventsRepository.GetAllInclude(new Expression<Func<Events, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsOnline==true
                }, null, y => y.EventsCategory, y => y.EventsParticipants).OrderByDescending(i => i.StartedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Events>().AsQueryable();
            }
        }

        public IQueryable<Events> GetAllIncludingRandomOpenEvents()
        {
            try
            {
                return _eventsRepository.GetAllInclude(new Expression<Func<Events, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsOnline==true
                }, null, y => y.EventsCategory, y => y.EventsParticipants).OrderByDescending(i => Guid.NewGuid()).Take(10);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Events>().AsQueryable();
            }
        }
        public async Task<Events?> GetBySlugAsync(string slug)
        {
            var match = await _eventsRepository.GetBySlugAsync(slug);
            if (match == null)
            {
                return null;
            }
            return await GetByIdAsync(match.Id);
        }
        public async Task<Events> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                return await _eventsRepository.GetIncludeAsync(i => i.Id == id, y => y.EventsCategory, y => y.EventsParticipants);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public Events HitRead(int id)
        {
            return _eventsRepository.HitRead(id);
        }

        public async Task<bool> LikeAsync(int id)
        {
            var result = await _eventsRepository.LikeAsync(id);
            return result;
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _eventsRepository.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _eventsRepository.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _eventsRepository.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _eventsRepository.SetNotDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetOfflineAsync(int id)
        {
            var result = await _eventsRepository.SetOfflineAsync(id);
            return result;
        }

        public async Task<bool> SetOnlineAsync(int id)
        {
            var result = await _eventsRepository.SetOnlineAsync(id);
            return result;
        }

        public async Task<bool> UpdateAsync(Events entity, IFormFile? image)
        {
            try
            {
                var errors = new List<string>();
                if (image != null)
                {
                    var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/img/events/");
                    if (!Directory.Exists(directoryPath))
                    {
                        Console.WriteLine($"Path is preparing: {directoryPath}");
                        Directory.CreateDirectory(directoryPath);
                    }
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                    var filePath = Path.Combine(directoryPath, fileName);
                    try
                    {
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await image.CopyToAsync(stream);
                        }
                        entity.ImageUrl = fileName;
                        entity.UpdatedDate = DateTime.UtcNow;
                        var result = await _eventsRepository.UpdateAsync(entity);
                        if (!result)
                        {
                            errors.Add($"Error {fileName}.");
                        }
                        return true;
                    }
                    catch (Exception ex)
                    {
                        errors.Add($"Error {fileName} : {ex.Message}");
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while updating the entity.", ex);
            }
        }

        public async Task<IEnumerable<Events>> GetAllForSignalRAsync()
        {
            try
            {
                var data = await _eventsRepository.GetAllIncludeAsync(new Expression<Func<Events, bool>>[]
                {
                    
                }, null, y => y.EventsCategory, y => y.EventsParticipants);
                return data.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Events>();
            }
        }
    }
}
