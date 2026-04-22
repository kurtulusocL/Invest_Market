using Investigation.DataAccess.Abstract;
using Investigation.DataAccess.Concrete.EntityFramework.Context.MSSQL;
using Investigation.Domain.Entities;
using Investigation.Shared.DataAccess.EntityFramework;
using Investigation.Shared.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Investigation.DataAccess.Concrete.EntityFramework.Repositories
{
    public class NewsRepository : EntityRepositoryBase<News, ApplicationDbContext>, INewsRepository
    {
        readonly ApplicationDbContext _context;
        public NewsRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<News?> GetBySlugAsync(string slug)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(slug) || slug.Length != 8)
                    return null;

                var candidates = await _context.Newses.AsNoTracking().Where(n => n.IsActive && !n.IsDeleted)
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
                return await _context.Newses.FirstOrDefaultAsync(n => n.Id == match.Id && n.IsActive && !n.IsDeleted);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred.", ex);
            }
        }
        public News HitRead(int id)
        {
            try
            {
                var hitRead = _context.Set<News>().Where(i => i.Id == id).FirstOrDefault();
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
                var like = await _context.Set<News>().Where(i => i.Id == id).FirstOrDefaultAsync();
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
                var active = await _context.Set<News>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (active != null)
                {
                    active.IsActive = true;
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
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
                var active = await _context.Set<News>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (active != null)
                {
                    active.IsActive = false;
                    active.SuspendedDate = DateTime.Now.ToLocalTime();
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
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
                var deleted = await _context.Set<News>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (deleted != null)
                {
                    deleted.IsDeleted = true;
                    deleted.DeletedDate = DateTime.Now.ToLocalTime();
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
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
                var deleted = await _context.Set<News>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (deleted != null)
                {
                    deleted.IsDeleted = false;
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting Not Deleted the entity.", ex);
            }
        }
    }
}
