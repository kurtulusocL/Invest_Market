using Investigation.DataAccess.Abstract;
using Investigation.DataAccess.Concrete.EntityFramework.Context.MSSQL;
using Investigation.Domain.Entities;
using Investigation.Shared.DataAccess.EntityFramework;
using Investigation.Shared.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Investigation.DataAccess.Concrete.EntityFramework.Repositories
{
    public class BlogRepository : EntityRepositoryBase<Blog, ApplicationDbContext>, IBlogRepository
    {
        readonly ApplicationDbContext _context;
        public BlogRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Blog?> GetBySlugAsync(string slug)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(slug) || slug.Length != 8)
                    return null;

                var candidates = await _context.Blogs.AsNoTracking().Where(n => n.IsActive && !n.IsDeleted)
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
                return await _context.Blogs.FirstOrDefaultAsync(n => n.Id == match.Id && n.IsActive && !n.IsDeleted);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred.", ex);
            }
        }
        public int BlogCounter()
        {
            try
            {
                return _context.Blogs.Count();
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            try
            {
                var data = await _context.Set<Blog>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (data == null)
                {
                    return false;
                }
                data.IsActive = true;

                var comments = await _context.Set<Comment>().Where(a => a.BlogId == id).ToListAsync();
                foreach (var comment in comments)
                {
                    comment.IsActive = true;
                }

                var hits = await _context.Set<Hit>().Where(a => a.BlogId == id).ToListAsync();
                foreach (var hit in hits)
                {
                    hit.IsActive = true;
                }

                var likes = await _context.Set<Like>().Where(a => a.BlogId == id).ToListAsync();
                foreach (var like in likes)
                {
                    like.IsActive = true;
                }

                var pictures = await _context.Set<Picture>().Where(a => a.BlogId == id).ToListAsync();
                foreach (var picture in pictures)
                {
                    picture.IsActive = true;
                }

                var reports = await _context.Set<Report>().Where(a => a.BlogId == id).ToListAsync();
                foreach (var report in reports)
                {
                    report.IsActive = true;
                }

                var savedContents = await _context.Set<SavedContent>().Where(a => a.BlogId == id).ToListAsync();
                foreach (var savedContent in savedContents)
                {
                    savedContent.IsActive = true;
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
                var data = await _context.Set<Blog>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (data == null)
                {
                    return false;
                }
                data.IsActive = false;
                data.SuspendedDate = DateTime.UtcNow;

                var comments = await _context.Set<Comment>().Where(a => a.BlogId == id).ToListAsync();
                foreach (var comment in comments)
                {
                    comment.IsActive = false;
                    comment.SuspendedDate = DateTime.UtcNow;
                }

                var hits = await _context.Set<Hit>().Where(a => a.BlogId == id).ToListAsync();
                foreach (var hit in hits)
                {
                    hit.IsActive = false;
                    hit.SuspendedDate = DateTime.UtcNow;
                }

                var likes = await _context.Set<Like>().Where(a => a.BlogId == id).ToListAsync();
                foreach (var like in likes)
                {
                    like.IsActive = false;
                    like.SuspendedDate = DateTime.UtcNow;
                }

                var pictures = await _context.Set<Picture>().Where(a => a.BlogId == id).ToListAsync();
                foreach (var picture in pictures)
                {
                    picture.IsActive = false;
                    picture.SuspendedDate = DateTime.UtcNow;
                }

                var reports = await _context.Set<Report>().Where(a => a.BlogId == id).ToListAsync();
                foreach (var report in reports)
                {
                    report.IsActive = false;
                    report.SuspendedDate = DateTime.UtcNow;
                }

                var savedContents = await _context.Set<SavedContent>().Where(a => a.BlogId == id).ToListAsync();
                foreach (var savedContent in savedContents)
                {
                    savedContent.IsActive = false;
                    savedContent.SuspendedDate = DateTime.UtcNow;
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
                var data = await _context.Set<Blog>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (data == null)
                {
                    return false;
                }
                data.IsDeleted = true;
                data.DeletedDate = DateTime.UtcNow;

                var comments = await _context.Set<Comment>().Where(a => a.BlogId == id).ToListAsync();
                foreach (var comment in comments)
                {
                    comment.IsDeleted = true;
                    comment.DeletedDate = DateTime.UtcNow;
                }

                var hits = await _context.Set<Hit>().Where(a => a.BlogId == id).ToListAsync();
                foreach (var hit in hits)
                {
                    hit.IsDeleted = true;
                    hit.DeletedDate = DateTime.UtcNow;
                }

                var likes = await _context.Set<Like>().Where(a => a.BlogId == id).ToListAsync();
                foreach (var like in likes)
                {
                    like.IsDeleted = true;
                    like.DeletedDate = DateTime.UtcNow;
                }

                var pictures = await _context.Set<Picture>().Where(a => a.BlogId == id).ToListAsync();
                foreach (var picture in pictures)
                {
                    picture.IsDeleted = true;
                    picture.DeletedDate = DateTime.UtcNow;
                }

                var reports = await _context.Set<Report>().Where(a => a.BlogId == id).ToListAsync();
                foreach (var report in reports)
                {
                    report.IsDeleted = true;
                    report.DeletedDate = DateTime.UtcNow;
                }

                var savedContents = await _context.Set<SavedContent>().Where(a => a.BlogId == id).ToListAsync();
                foreach (var savedContent in savedContents)
                {
                    savedContent.IsDeleted = true;
                    savedContent.DeletedDate = DateTime.UtcNow;
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
                var data = await _context.Set<Blog>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (data == null)
                {
                    return false;
                }
                data.IsDeleted = true;

                var comments = await _context.Set<Comment>().Where(a => a.BlogId == id).ToListAsync();
                foreach (var comment in comments)
                {
                    comment.IsDeleted = false;
                }

                var hits = await _context.Set<Hit>().Where(a => a.BlogId == id).ToListAsync();
                foreach (var hit in hits)
                {
                    hit.IsDeleted = false;
                }

                var likes = await _context.Set<Like>().Where(a => a.BlogId == id).ToListAsync();
                foreach (var like in likes)
                {
                    like.IsDeleted = false;
                }

                var pictures = await _context.Set<Picture>().Where(a => a.BlogId == id).ToListAsync();
                foreach (var picture in pictures)
                {
                    picture.IsDeleted = false;
                }

                var reports = await _context.Set<Report>().Where(a => a.BlogId == id).ToListAsync();
                foreach (var report in reports)
                {
                    report.IsDeleted = false;
                }

                var savedContents = await _context.Set<SavedContent>().Where(a => a.BlogId == id).ToListAsync();
                foreach (var savedContent in savedContents)
                {
                    savedContent.IsDeleted = false;
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting NotDeleted the entity.", ex);
            }
        }
    }
}
