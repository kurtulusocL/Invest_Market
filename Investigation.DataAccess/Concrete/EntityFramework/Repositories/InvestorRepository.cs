using Investigation.DataAccess.Abstract;
using Investigation.DataAccess.Concrete.EntityFramework.Context.MSSQL;
using Investigation.Domain.Entities;
using Investigation.Shared.DataAccess.EntityFramework;
using Investigation.Shared.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Investigation.DataAccess.Concrete.EntityFramework.Repositories
{
    public class InvestorRepository : EntityRepositoryBase<Investor, ApplicationDbContext>, IInvestorRepository
    {
        readonly ApplicationDbContext _context;
        public InvestorRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Investor?> GetBySlugAsync(string slug)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(slug) || slug.Length != 8)
                    return null;

                var candidates = await _context.Investors.AsNoTracking().Where(n => n.IsActive && !n.IsDeleted)
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
                return await _context.Investors.FirstOrDefaultAsync(n => n.Id == match.Id && n.IsActive && !n.IsDeleted);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred.", ex);
            }
        }
        public async Task<bool> SetInvestorLookingForCompanyAsync(int id)
        {
            try
            {
                var isLookingForCompany = await _context.Set<Investor>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (isLookingForCompany != null)
                {
                    isLookingForCompany.IsLookingForCompany = true;
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting Investor Looking For Company the entity.", ex);
            }
        }

        public async Task<bool> SetInvestorNotLookingForCompanyAsync(int id)
        {
            try
            {
                var isLookingForCompany = await _context.Set<Investor>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (isLookingForCompany != null)
                {
                    isLookingForCompany.IsLookingForCompany = false;
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting Investor Not Looking For Company the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            try
            {
                var data = await _context.Set<Investor>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (data == null)
                {
                    return false;
                }
                data.IsActive = true;

                var announcements = await _context.Set<Announcement>().Where(a => a.InvestorId == id).ToListAsync();
                foreach (var announcement in announcements)
                {
                    announcement.IsActive = true;
                }

                var hits = await _context.Set<Hit>().Where(a => a.InvestorId == id).ToListAsync();
                foreach (var hit in hits)
                {
                    hit.IsActive = true;
                }

                var likes = await _context.Set<Like>().Where(a => a.InvestorId == id).ToListAsync();
                foreach (var like in likes)
                {
                    like.IsActive = true;
                }

                var recentlyInvests = await _context.Set<RecentlyInvest>().Where(a => a.InvestorId == id).ToListAsync();
                foreach (var recentlyInvest in recentlyInvests)
                {
                    recentlyInvest.IsActive = true;
                }

                var posts = await _context.Set<Post>().Where(a => a.InvestorId == id).ToListAsync();
                foreach (var post in posts)
                {
                    post.IsActive = true;
                }

                var reports = await _context.Set<Report>().Where(a => a.InvestorId == id).ToListAsync();
                foreach (var report in reports)
                {
                    report.IsActive = true;
                }

                var savedContents = await _context.Set<SavedContent>().Where(a => a.InvestorId == id).ToListAsync();
                foreach (var savedContent in savedContents)
                {
                    savedContent.IsActive = true;
                }

                var surveys = await _context.Set<Survey>().Where(a => a.InvestorId == id).ToListAsync();
                foreach (var survey in surveys)
                {
                    survey.IsActive = true;
                }

                var userSocialMedias = await _context.Set<UserSocialMedia>().Where(a => a.InvestorId == id).ToListAsync();
                foreach (var userSocialMedia in userSocialMedias)
                {
                    userSocialMedia.IsActive = true;
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
                var data = await _context.Set<Investor>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (data == null)
                {
                    return false;
                }
                data.IsActive = false;
                data.SuspendedDate = DateTime.UtcNow;

                var announcements = await _context.Set<Announcement>().Where(a => a.InvestorId == id).ToListAsync();
                foreach (var announcement in announcements)
                {
                    announcement.IsActive = false;
                    announcement.SuspendedDate = DateTime.UtcNow;
                }

                var hits = await _context.Set<Hit>().Where(a => a.InvestorId == id).ToListAsync();
                foreach (var hit in hits)
                {
                    hit.IsActive = false;
                    hit.SuspendedDate = DateTime.UtcNow;
                }

                var likes = await _context.Set<Like>().Where(a => a.InvestorId == id).ToListAsync();
                foreach (var like in likes)
                {
                    like.IsActive = false;
                    like.SuspendedDate = DateTime.UtcNow;
                }

                var recentlyInvests = await _context.Set<RecentlyInvest>().Where(a => a.InvestorId == id).ToListAsync();
                foreach (var recentlyInvest in recentlyInvests)
                {
                    recentlyInvest.IsActive = false;
                    recentlyInvest.SuspendedDate = DateTime.UtcNow;
                }

                var posts = await _context.Set<Post>().Where(a => a.InvestorId == id).ToListAsync();
                foreach (var post in posts)
                {
                    post.IsActive = false;
                    post.SuspendedDate = DateTime.UtcNow;
                }

                var reports = await _context.Set<Report>().Where(a => a.InvestorId == id).ToListAsync();
                foreach (var report in reports)
                {
                    report.IsActive = false;
                    report.SuspendedDate = DateTime.UtcNow;
                }

                var savedContents = await _context.Set<SavedContent>().Where(a => a.InvestorId == id).ToListAsync();
                foreach (var savedContent in savedContents)
                {
                    savedContent.IsActive = false;
                    savedContent.SuspendedDate = DateTime.UtcNow;
                }

                var surveys = await _context.Set<Survey>().Where(a => a.InvestorId == id).ToListAsync();
                foreach (var survey in surveys)
                {
                    survey.IsActive = false;
                    survey.SuspendedDate = DateTime.UtcNow;
                }

                var userSocialMedias = await _context.Set<UserSocialMedia>().Where(a => a.InvestorId == id).ToListAsync();
                foreach (var userSocialMedia in userSocialMedias)
                {
                    userSocialMedia.IsActive = false;
                    userSocialMedia.SuspendedDate = DateTime.UtcNow;
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
                var data = await _context.Set<Investor>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (data == null)
                {
                    return false;
                }
                data.IsDeleted = true;
                data.DeletedDate = DateTime.UtcNow;

                var announcements = await _context.Set<Announcement>().Where(a => a.InvestorId == id).ToListAsync();
                foreach (var announcement in announcements)
                {
                    announcement.IsDeleted = true;
                    announcement.DeletedDate = DateTime.UtcNow;
                }

                var hits = await _context.Set<Hit>().Where(a => a.InvestorId == id).ToListAsync();
                foreach (var hit in hits)
                {
                    hit.IsDeleted = true;
                    hit.DeletedDate = DateTime.UtcNow;
                }

                var likes = await _context.Set<Like>().Where(a => a.InvestorId == id).ToListAsync();
                foreach (var like in likes)
                {
                    like.IsDeleted = true;
                    like.DeletedDate = DateTime.UtcNow;
                }

                var recentlyInvests = await _context.Set<RecentlyInvest>().Where(a => a.InvestorId == id).ToListAsync();
                foreach (var recentlyInvest in recentlyInvests)
                {
                    recentlyInvest.IsDeleted = true;
                    recentlyInvest.DeletedDate = DateTime.UtcNow;
                }

                var posts = await _context.Set<Post>().Where(a => a.InvestorId == id).ToListAsync();
                foreach (var post in posts)
                {
                    post.IsDeleted = true;
                    post.DeletedDate = DateTime.UtcNow;
                }

                var reports = await _context.Set<Report>().Where(a => a.InvestorId == id).ToListAsync();
                foreach (var report in reports)
                {
                    report.IsDeleted = true;
                    report.DeletedDate = DateTime.UtcNow;
                }

                var savedContents = await _context.Set<SavedContent>().Where(a => a.InvestorId == id).ToListAsync();
                foreach (var savedContent in savedContents)
                {
                    savedContent.IsDeleted = true;
                    savedContent.DeletedDate = DateTime.UtcNow;
                }

                var surveys = await _context.Set<Survey>().Where(a => a.InvestorId == id).ToListAsync();
                foreach (var survey in surveys)
                {
                    survey.IsDeleted = true;
                    survey.DeletedDate = DateTime.UtcNow;
                }

                var userSocialMedias = await _context.Set<UserSocialMedia>().Where(a => a.InvestorId == id).ToListAsync();
                foreach (var userSocialMedia in userSocialMedias)
                {
                    userSocialMedia.IsDeleted = true;
                    userSocialMedia.DeletedDate = DateTime.UtcNow;
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
                var data = await _context.Set<Investor>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (data == null)
                {
                    return false;
                }
                data.IsDeleted = false;

                var announcements = await _context.Set<Announcement>().Where(a => a.InvestorId == id).ToListAsync();
                foreach (var announcement in announcements)
                {
                    announcement.IsDeleted = false;
                }

                var hits = await _context.Set<Hit>().Where(a => a.InvestorId == id).ToListAsync();
                foreach (var hit in hits)
                {
                    hit.IsDeleted = false;
                }

                var likes = await _context.Set<Like>().Where(a => a.InvestorId == id).ToListAsync();
                foreach (var like in likes)
                {
                    like.IsDeleted = false;
                }

                var recentlyInvests = await _context.Set<RecentlyInvest>().Where(a => a.InvestorId == id).ToListAsync();
                foreach (var recentlyInvest in recentlyInvests)
                {
                    recentlyInvest.IsDeleted = false;
                }

                var posts = await _context.Set<Post>().Where(a => a.InvestorId == id).ToListAsync();
                foreach (var post in posts)
                {
                    post.IsDeleted = false;
                }

                var reports = await _context.Set<Report>().Where(a => a.InvestorId == id).ToListAsync();
                foreach (var report in reports)
                {
                    report.IsDeleted = false;
                }

                var savedContents = await _context.Set<SavedContent>().Where(a => a.InvestorId == id).ToListAsync();
                foreach (var savedContent in savedContents)
                {
                    savedContent.IsDeleted = false;
                }

                var surveys = await _context.Set<Survey>().Where(a => a.InvestorId == id).ToListAsync();
                foreach (var survey in surveys)
                {
                    survey.IsDeleted = false;
                }

                var userSocialMedias = await _context.Set<UserSocialMedia>().Where(a => a.InvestorId == id).ToListAsync();
                foreach (var userSocialMedia in userSocialMedias)
                {
                    userSocialMedia.IsDeleted = false;
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting NotDeleted the entity.", ex);
            }
        }

        public int InvestorCounter()
        {
            try
            {
                return _context.Investors.Count();
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public IEnumerable<Investor> GetAllIncludingMostPopularInvestors()
        {
            try
            {
                var popularInvestors = _context.Investors
                       .Where(i => i.IsActive == true && i.IsDeleted == false)
                       .Include(i => i.InvestorCategory)
                       .Include(i => i.Country)
                       .Include(i => i.AppUser)
                       .Include(i => i.Blogs)
                       .Include(i => i.Posts)
                       .Include(i => i.Hits)
                       .Include(i => i.Likes)
                       .Include(i => i.Surveys)
                       .Include(i => i.RecentlyInvests)
                       .Include(i => i.SavedContents).AsSplitQuery()
                       .OrderByDescending(i =>
                           i.Blogs.Count() * 10.0 +
                           i.Posts.Count() * 10.0 +
                           i.Hits.Count() * 15.0 +
                           i.Likes.Count() * 30.0 +
                           i.Surveys.Count() * 5.0 +
                           i.RecentlyInvests.Count() * 5.0 +
                           i.SavedContents.Count() * 25.0)
                       .Take(15).ToList();
                return popularInvestors;
            }
            catch (Exception)
            {
                return new List<Investor>();
            }
        }

        public async Task<IEnumerable<Investor>> GetAllIncludingMostPopularInvestorsAsync()
        {
            try
            {
                var popularInvestors = await _context.Investors
                       .Where(i => i.IsActive == true && i.IsDeleted == false)
                       .Include(i => i.AppUser)
                       .Include(i => i.InvestorCategory)
                       .Include(i => i.Country)
                       .Include(i => i.AppUser)
                       .Include(i => i.Blogs)
                       .Include(i => i.Posts)
                       .Include(i => i.Hits)
                       .Include(i => i.Likes)
                       .Include(i => i.Surveys)
                       .Include(i => i.RecentlyInvests)
                       .Include(i => i.SavedContents).AsSplitQuery()
                       .OrderByDescending(i =>
                           i.Blogs.Count() * 10.0 +
                           i.Posts.Count() * 10.0 +
                           i.Hits.Count() * 15.0 +
                           i.Likes.Count() * 30.0 +
                           i.Surveys.Count() * 5.0 +
                           i.RecentlyInvests.Count() * 5.0 +
                           i.SavedContents.Count() * 25.0)
                       .Take(120).ToListAsync();
                return popularInvestors;
            }
            catch (Exception)
            {
                return new List<Investor>();
            }
        }

        public async Task<IEnumerable<Investor>> GetAllIncludingUnPopularInvestorsAsync()
        {
            try
            {
                var unpopularInvestors = await _context.Investors
                    .Where(i => i.IsActive == true && i.IsDeleted == false)
                    .Include(i => i.AppUser)
                    .Include(i => i.InvestorCategory)
                    .Include(i => i.Country)
                    .Include(i => i.AppUser)
                    .Include(i => i.Blogs)
                    .Include(i => i.Posts)
                    .Include(i => i.Hits)
                    .Include(i => i.Likes)
                    .Include(i => i.Surveys)
                    .Include(i => i.RecentlyInvests)
                    .Include(i => i.SavedContents).AsSplitQuery()
                    .OrderBy(i =>
                        i.Blogs.Count() * 10.0 +
                        i.Posts.Count() * 10.0 +
                        i.Hits.Count() * 15.0 +
                        i.Likes.Count() * 30.0 +
                        i.Surveys.Count() * 5.0 +
                        i.RecentlyInvests.Count() * 5.0 +
                        i.SavedContents.Count() * 25.0)
                    .Take(120).ToListAsync();

                return unpopularInvestors;
            }
            catch (Exception)
            {
                return new List<Investor>();
            }
        }
    }
}
