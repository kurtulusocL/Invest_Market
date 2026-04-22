using Investigation.DataAccess.Abstract;
using Investigation.DataAccess.Concrete.EntityFramework.Context.MSSQL;
using Investigation.Domain.Entities;
using Investigation.Shared.DataAccess.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Investigation.DataAccess.Concrete.EntityFramework.Repositories
{
    public class CommentAnswerRepository : EntityRepositoryBase<CommentAnswer, ApplicationDbContext>, ICommentAnswerRepository
    {
        readonly ApplicationDbContext _context;
        public CommentAnswerRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public int CommentAnswerCounter()
        {
            try
            {
                return _context.CommentAnswers.Count();
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
                var data = await _context.Set<CommentAnswer>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (data == null)
                {
                    return false;
                }
                data.IsActive = true;

                var hits = await _context.Set<Hit>().Where(a => a.CommentAnswerId == id).ToListAsync();
                foreach (var hit in hits)
                {
                    hit.IsActive = true;
                }

                var likes = await _context.Set<Like>().Where(a => a.CommentAnswerId == id).ToListAsync();
                foreach (var like in likes)
                {
                    like.IsActive = true;
                }

                var reports = await _context.Set<Report>().Where(a => a.CommentAnswerId == id).ToListAsync();
                foreach (var report in reports)
                {
                    report.IsActive = true;
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
                var data = await _context.Set<CommentAnswer>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (data == null)
                {
                    return false;
                }
                data.IsActive = false;
                data.SuspendedDate = DateTime.UtcNow;

                var hits = await _context.Set<Hit>().Where(a => a.CommentAnswerId == id).ToListAsync();
                foreach (var hit in hits)
                {
                    hit.IsActive = false;
                    hit.SuspendedDate = DateTime.UtcNow;
                }

                var likes = await _context.Set<Like>().Where(a => a.CommentAnswerId == id).ToListAsync();
                foreach (var like in likes)
                {
                    like.IsActive = false;
                    like.SuspendedDate = DateTime.UtcNow;
                }

                var reports = await _context.Set<Report>().Where(a => a.CommentAnswerId == id).ToListAsync();
                foreach (var report in reports)
                {
                    report.IsActive = false;
                    report.SuspendedDate = DateTime.UtcNow;
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
                var data = await _context.Set<CommentAnswer>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (data == null)
                {
                    return false;
                }
                data.IsDeleted = true;
                data.DeletedDate = DateTime.UtcNow;

                var hits = await _context.Set<Hit>().Where(a => a.CommentAnswerId == id).ToListAsync();
                foreach (var hit in hits)
                {
                    hit.IsDeleted = true;
                    hit.DeletedDate = DateTime.UtcNow;
                }

                var likes = await _context.Set<Like>().Where(a => a.CommentAnswerId == id).ToListAsync();
                foreach (var like in likes)
                {
                    like.IsDeleted = true;
                    like.DeletedDate = DateTime.UtcNow;
                }

                var reports = await _context.Set<Report>().Where(a => a.CommentAnswerId == id).ToListAsync();
                foreach (var report in reports)
                {
                    report.IsDeleted = true;
                    report.DeletedDate = DateTime.UtcNow;
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
                var data = await _context.Set<CommentAnswer>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (data == null)
                {
                    return false;
                }
                data.IsDeleted = false;

                var hits = await _context.Set<Hit>().Where(a => a.CommentAnswerId == id).ToListAsync();
                foreach (var hit in hits)
                {
                    hit.IsDeleted = false;
                }

                var likes = await _context.Set<Like>().Where(a => a.CommentAnswerId == id).ToListAsync();
                foreach (var like in likes)
                {
                    like.IsDeleted = false;
                }

                var reports = await _context.Set<Report>().Where(a => a.CommentAnswerId == id).ToListAsync();
                foreach (var report in reports)
                {
                    report.IsDeleted = false;
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
