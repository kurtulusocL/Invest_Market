using Investigation.DataAccess.Abstract;
using Investigation.DataAccess.Concrete.EntityFramework.Context.MSSQL;
using Investigation.Domain.Entities;
using Investigation.Shared.DataAccess.EntityFramework;
using Investigation.Shared.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Investigation.DataAccess.Concrete.EntityFramework.Repositories
{
    public class EventsRepository : EntityRepositoryBase<Events, ApplicationDbContext>, IEventsRepository
    {
        readonly ApplicationDbContext _context;
        public EventsRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Events?> GetBySlugAsync(string slug)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(slug) || slug.Length != 8)
                    return null;

                var candidates = await _context.Eventses.AsNoTracking().Where(n => n.IsActive && !n.IsDeleted)
                    .Select(n => new
                    {
                        n.Id,
                        n.CreatedDate
                    }).ToListAsync();

                var match = candidates.FirstOrDefault(c =>
                {
                    var computedSlug = SecureSlugHelper.Generate(c.Id, c.CreatedDate);
                    return string.Equals(computedSlug, slug, StringComparison.OrdinalIgnoreCase);
                });

                if (match == null)
                {
                    return null;
                }
                return await _context.Eventses.FirstOrDefaultAsync(n => n.Id == match.Id && n.IsActive && !n.IsDeleted);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred.", ex);
            }
        }
        public Events HitRead(int id)
        {
            try
            {
                var hitRead = _context.Set<Events>().Where(i => i.Id == id).FirstOrDefault();
                if (hitRead != null && hitRead.Hit >= 0)
                {
                    hitRead.Hit++;
                    _context.SaveChanges();
                    return hitRead;
                }
                return hitRead;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting Hit the entity.", ex);
            }
        }

        public async Task<bool> LikeAsync(int id)
        {
            try
            {
                var like = await _context.Set<Events>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (like != null && like.Like >= 0)
                {
                    like.Like++;
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while liking the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            try
            {
                var data = await _context.Set<Events>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (data == null)
                {
                    return false;
                }
                data.IsActive = true;

                var eventsParticipants = await _context.Set<EventsParticipant>().Where(a => a.EventsId == id).ToListAsync();
                foreach (var eventsParticipant in eventsParticipants)
                {
                    eventsParticipant.IsActive = true;
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting Active the entity.", ex);
            }
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            try
            {
                var data = await _context.Set<Events>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (data == null)
                {
                    return false;
                }
                data.IsActive = false;
                data.SuspendedDate = DateTime.UtcNow;

                var eventsParticipants = await _context.Set<EventsParticipant>().Where(a => a.EventsId == id).ToListAsync();
                foreach (var eventsParticipant in eventsParticipants)
                {
                    eventsParticipant.IsActive = false;
                    eventsParticipant.SuspendedDate= DateTime.UtcNow;
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting DeActive the entity.", ex);
            }
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            try
            {
                var data = await _context.Set<Events>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (data == null)
                {
                    return false;
                }
                data.IsDeleted = true;
                data.DeletedDate = DateTime.UtcNow;

                var eventsParticipants = await _context.Set<EventsParticipant>().Where(a => a.EventsId == id).ToListAsync();
                foreach (var eventsParticipant in eventsParticipants)
                {
                    eventsParticipant.IsDeleted = true;
                    eventsParticipant.DeletedDate = DateTime.UtcNow;
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting Deleted the entity.", ex);
            }
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            try
            {
                var data = await _context.Set<Events>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (data == null)
                {
                    return false;
                }
                data.IsDeleted = false;

                var eventsParticipants = await _context.Set<EventsParticipant>().Where(a => a.EventsId == id).ToListAsync();
                foreach (var eventsParticipant in eventsParticipants)
                {
                    eventsParticipant.IsDeleted = false;
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting Not Deleted the entity.", ex);
            }
        }

        public async Task<bool> SetOfflineAsync(int id)
        {
            try
            {
                var isOnline = await _context.Set<Events>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (isOnline != null)
                {
                    isOnline.IsOnline = false;
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting Offline the entity.", ex);
            }
        }

        public async Task<bool> SetOnlineAsync(int id)
        {
            try
            {
                var isOnline = await _context.Set<Events>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (isOnline != null)
                {
                    isOnline.IsOnline = true;
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting Online the entity.", ex);
            }
        }
    }
}
