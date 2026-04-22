using System.Linq.Expressions;
using Investigation.Business.Services.Abstract;
using Investigation.DataAccess.Abstract;
using Investigation.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Investigation.Business.Services.Concrete
{
    public class EventsParticipantManager : IEventsParticipantService
    {
        readonly IEventsParticipantRepository _eventsParticipantRepository;
        public EventsParticipantManager(IEventsParticipantRepository eventsParticipantRepository)
        {
            _eventsParticipantRepository = eventsParticipantRepository;
        }

        public async Task<bool> CreateAsync(string nameSurname, string title, DateTime joinTime, string shortDescription, int? eventsId, IFormFile image)
        {
            try
            {
                if (eventsId == null)
                    throw new ArgumentNullException(nameof(eventsId), "eventsId was null");

                var errors = new List<string>();
                if (image != null)
                {
                    var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/img/events/participant/");
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

                        var entity = new EventsParticipant
                        {
                            NameSurname = nameSurname,
                            Title = title,
                            JoinTime = joinTime,
                            ShortDescription = shortDescription,
                            EventsId = eventsId
                        };

                        entity.ImageUrl = fileName;
                        var results = await _eventsParticipantRepository.AddAsync(entity);
                        if (!results)
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

                var result = await _eventsParticipantRepository.DeleteByIdsAsync(ids.Cast<object>());
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while bulk deleting entities.", ex);
            }
        }

        public async Task<bool> DeleteAsync(EventsParticipant entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var data = await _eventsParticipantRepository.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _eventsParticipantRepository.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public async Task<IEnumerable<EventsParticipant>> GetAllForSignalRAsync()
        {
            try
            {
                var data = await _eventsParticipantRepository.GetAllIncludeAsync(new Expression<Func<EventsParticipant, bool>>[]
                {
                    
                }, null, y => y.Events);
                return data.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<EventsParticipant>();
            }
        }

        public IQueryable<EventsParticipant> GetAllIncludingAsync()
        {
            try
            {
                var data = _eventsParticipantRepository.GetAllInclude(new Expression<Func<EventsParticipant, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Events);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<EventsParticipant>().AsQueryable();
            }
        }

        public IQueryable<EventsParticipant> GetAllIncludingByEventsIdAsync(int? eventsId)
        {
            try
            {
                if (eventsId == null)
                    throw new ArgumentNullException(nameof(eventsId), "eventsId was null");

                var data = _eventsParticipantRepository.GetAllIncludeById(eventsId, "EventsId", new Expression<Func<EventsParticipant, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Events);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<EventsParticipant>().AsQueryable();
            }
        }

        public IQueryable<EventsParticipant> GetAllIncludingByJoinDateAsync()
        {
            try
            {
                var data = _eventsParticipantRepository.GetAllInclude(new Expression<Func<EventsParticipant, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Events);
                return data.OrderByDescending(i => i.JoinTime);
            }
            catch (Exception)
            {
                return Enumerable.Empty<EventsParticipant>().AsQueryable();
            }
        }

        public IQueryable<EventsParticipant> GetAllIncludingEventsParticipantByEventsId(int? eventsId)
        {
            try
            {
                if (eventsId == null)
                    throw new ArgumentNullException(nameof(eventsId), "eventsId was null");

                return _eventsParticipantRepository.GetAllIncludeById(eventsId, "EventsId", new Expression<Func<EventsParticipant, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Events).OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<EventsParticipant>().AsQueryable();
            }
        }

        public IQueryable<EventsParticipant> GetAllIncludingEventsParticipantsRandom()
        {
            try
            {
                return _eventsParticipantRepository.GetAllInclude(new Expression<Func<EventsParticipant, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Events.IsActive==true&&i.Events.IsDeleted==false
                }, y => y.Events).OrderByDescending(i => Guid.NewGuid()).Take(20);
            }
            catch (Exception)
            {
                return Enumerable.Empty<EventsParticipant>().AsQueryable();
            }
        }

        public IQueryable<EventsParticipant> GetAllIncludingForAdminAsync()
        {
            try
            {
                var data = _eventsParticipantRepository.GetAllInclude(new Expression<Func<EventsParticipant, bool>>[]
                {

                }, null, y => y.Events);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<EventsParticipant>().AsQueryable();
            }
        }

        public IQueryable<EventsParticipant> GetAllIncludingForSitemap()
        {
            try
            {
                return _eventsParticipantRepository.GetAllInclude(new Expression<Func<EventsParticipant, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Events).OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<EventsParticipant>().AsQueryable();
            }
        }

        public async Task<EventsParticipant> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                return await _eventsParticipantRepository.GetIncludeAsync(i => i.Id == id, y => y.Events);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _eventsParticipantRepository.SetActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _eventsParticipantRepository.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _eventsParticipantRepository.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _eventsParticipantRepository.SetNotDeletedAsync(id);
            return result;
        }

        public async Task<bool> UpdateAsync(string nameSurname, string title, DateTime joinTime, string shortDescription, int? eventsId, IFormFile image, int id)
        {
            try
            {
                if (eventsId == null)
                    throw new ArgumentNullException(nameof(eventsId), "eventsId was null");

                var errors = new List<string>();
                if (image != null)
                {
                    var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/img/events/participant/");
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

                        var entity = new EventsParticipant
                        {
                            NameSurname = nameSurname,
                            Title = title,
                            JoinTime = joinTime,
                            ShortDescription = shortDescription,
                            EventsId = eventsId,
                            Id = id,
                            UpdatedDate = DateTime.UtcNow
                        };

                        entity.ImageUrl = fileName;
                        var results = await _eventsParticipantRepository.UpdateAsync(entity);
                        if (!results)
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
    }
}
