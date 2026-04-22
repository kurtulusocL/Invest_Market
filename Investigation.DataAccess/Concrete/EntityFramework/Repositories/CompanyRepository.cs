using Investigation.DataAccess.Abstract;
using Investigation.DataAccess.Concrete.EntityFramework.Context.MSSQL;
using Investigation.Domain.Entities;
using Investigation.Shared.DataAccess.EntityFramework;
using Investigation.Shared.Helpers;
using Microsoft.AspNetCore.Routing.Tree;
using Microsoft.EntityFrameworkCore;

namespace Investigation.DataAccess.Concrete.EntityFramework.Repositories
{
    public class CompanyRepository : EntityRepositoryBase<Company, ApplicationDbContext>, ICompanyRepository
    {
        readonly ApplicationDbContext _context;
        public CompanyRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Company?> GetBySlugAsync(string slug)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(slug) || slug.Length != 8)
                    return null;

                var candidates = await _context.Companies.AsNoTracking().Where(n => n.IsActive && !n.IsDeleted)
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
                return await _context.Companies.FirstOrDefaultAsync(n => n.Id == match.Id && n.IsActive && !n.IsDeleted);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred.", ex);
            }
        }
        public async Task<bool> SetLookingForInvestAsync(int id)
        {
            try
            {
                var isLookingForInvest = await _context.Set<Company>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (isLookingForInvest != null)
                {
                    isLookingForInvest.IsLookingForInvest = true;
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting Company Looking For Invest the entity.", ex);
            }
        }

        public async Task<bool> SetNotLookingForInvestAsync(int id)
        {
            try
            {
                var isLookingForInvest = await _context.Set<Company>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (isLookingForInvest != null)
                {
                    isLookingForInvest.IsLookingForInvest = false;
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting Company Not Looking For Invest the entity.", ex);
            }
        }

        public async Task<bool> SetFollowableAsync(int id)
        {
            try
            {
                var isFollowable = await _context.Set<Company>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (isFollowable != null)
                {
                    isFollowable.IsFollowable = true;
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting Company Followable the entity.", ex);
            }
        }

        public async Task<bool> SetNotFollowableAsync(int id)
        {
            try
            {
                var isFollowable = await _context.Set<Company>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (isFollowable != null)
                {
                    isFollowable.IsFollowable = false;
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting Company NotFollowable the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            try
            {
                var data = await _context.Set<Company>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (data == null)
                {
                    return false;
                }
                data.IsActive = true;

                var announcements = await _context.Set<Announcement>().Where(a => a.CompanyId == id).ToListAsync();
                foreach (var announcement in announcements)
                {
                    announcement.IsActive = true;
                }

                var companyContacts = await _context.Set<CompanyContact>().Where(a => a.CompanyId == id).ToListAsync();
                foreach (var companyContact in companyContacts)
                {
                    companyContact.IsActive = true;
                }

                var companyFinances = await _context.Set<CompanyFinance>().Where(a => a.CompanyId == id).ToListAsync();
                foreach (var companyFinance in companyFinances)
                {
                    companyFinance.IsActive = true;
                }

                var companyPinteches = await _context.Set<CompanyPintech>().Where(a => a.CompanyId == id).ToListAsync();
                foreach (var companyPintech in companyPinteches)
                {
                    companyPintech.IsActive = true;
                }

                var companyStages = await _context.Set<CompanyStage>().Where(a => a.CompanyId == id).ToListAsync();
                foreach (var companyStage in companyStages)
                {
                    companyStage.IsActive = true;
                }

                var companyTeams = await _context.Set<CompanyTeam>().Where(a => a.CompanyId == id).ToListAsync();
                foreach (var companyTeam in companyTeams)
                {
                    companyTeam.IsActive = true;
                }

                var comments = await _context.Set<Comment>().Where(a => a.CompanyId == id).ToListAsync();
                foreach (var comment in comments)
                {
                    comment.IsActive = true;
                }

                var followerCompanies = await _context.Set<Follow>().Where(a => a.FollowerCompanyId == id).ToListAsync();
                foreach (var follow in followerCompanies)
                {
                    follow.IsActive = true;
                }

                var followedCompanies = await _context.Set<Follow>().Where(a => a.FollowedCompanyId == id).ToListAsync();
                foreach (var followedCompany in followedCompanies)
                {
                    followedCompany.IsActive = true;
                }

                var hits = await _context.Set<Hit>().Where(a => a.CompanyId == id).ToListAsync();
                foreach (var hit in hits)
                {
                    hit.IsActive = true;
                }

                var likes = await _context.Set<Like>().Where(a => a.CompanyId == id).ToListAsync();
                foreach (var like in likes)
                {
                    like.IsActive = true;
                }

                var pictures = await _context.Set<Picture>().Where(a => a.CompanyId == id).ToListAsync();
                foreach (var picture in pictures)
                {
                    picture.IsActive = true;
                }

                var posts = await _context.Set<Post>().Where(a => a.CompanyId == id).ToListAsync();
                foreach (var post in posts)
                {
                    post.IsActive = true;
                }

                var reports = await _context.Set<Report>().Where(a => a.CompanyId == id).ToListAsync();
                foreach (var report in reports)
                {
                    report.IsActive = true;
                }

                var savedContents = await _context.Set<SavedContent>().Where(a => a.CompanyId == id).ToListAsync();
                foreach (var savedContent in savedContents)
                {
                    savedContent.IsActive = true;
                }

                var surveys = await _context.Set<Survey>().Where(a => a.CompanyId == id).ToListAsync();
                foreach (var survey in surveys)
                {
                    survey.IsActive = true;
                }

                var userSocialMedias = await _context.Set<UserSocialMedia>().Where(a => a.CompanyId == id).ToListAsync();
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
                var data = await _context.Set<Company>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (data == null)
                {
                    return false;
                }
                data.IsActive = false;
                data.SuspendedDate = DateTime.UtcNow;

                var announcements = await _context.Set<Announcement>().Where(a => a.CompanyId == id).ToListAsync();
                foreach (var announcement in announcements)
                {
                    announcement.IsActive = false;
                    announcement.SuspendedDate = DateTime.UtcNow;
                }

                var companyContacts = await _context.Set<CompanyContact>().Where(a => a.CompanyId == id).ToListAsync();
                foreach (var companyContact in companyContacts)
                {
                    companyContact.IsActive = false;
                    companyContact.SuspendedDate = DateTime.UtcNow;
                }

                var companyFinances = await _context.Set<CompanyFinance>().Where(a => a.CompanyId == id).ToListAsync();
                foreach (var companyFinance in companyFinances)
                {
                    companyFinance.IsActive = false;
                    companyFinance.SuspendedDate = DateTime.UtcNow;
                }

                var companyPinteches = await _context.Set<CompanyPintech>().Where(a => a.CompanyId == id).ToListAsync();
                foreach (var companyPintech in companyPinteches)
                {
                    companyPintech.IsActive = false;
                    companyPintech.SuspendedDate = DateTime.UtcNow;
                }

                var companyStages = await _context.Set<CompanyStage>().Where(a => a.CompanyId == id).ToListAsync();
                foreach (var companyStage in companyStages)
                {
                    companyStage.IsActive = false;
                    companyStage.SuspendedDate = DateTime.UtcNow;
                }

                var companyTeams = await _context.Set<CompanyTeam>().Where(a => a.CompanyId == id).ToListAsync();
                foreach (var companyTeam in companyTeams)
                {
                    companyTeam.IsActive = false;
                    companyTeam.SuspendedDate = DateTime.UtcNow;
                }

                var comments = await _context.Set<Comment>().Where(a => a.CompanyId == id).ToListAsync();
                foreach (var comment in comments)
                {
                    comment.IsActive = false;
                    comment.SuspendedDate = DateTime.UtcNow;
                }

                var followerCompanies = await _context.Set<Follow>().Where(a => a.FollowerCompanyId == id).ToListAsync();
                foreach (var follow in followerCompanies)
                {
                    follow.IsActive = false;
                    follow.SuspendedDate = DateTime.UtcNow;
                }

                var followedCompanies = await _context.Set<Follow>().Where(a => a.FollowedCompanyId == id).ToListAsync();
                foreach (var followedCompany in followedCompanies)
                {
                    followedCompany.IsActive = false;
                    followedCompany.SuspendedDate = DateTime.UtcNow;
                }

                var hits = await _context.Set<Hit>().Where(a => a.CompanyId == id).ToListAsync();
                foreach (var hit in hits)
                {
                    hit.IsActive = false;
                    hit.SuspendedDate = DateTime.UtcNow;
                }

                var likes = await _context.Set<Like>().Where(a => a.CompanyId == id).ToListAsync();
                foreach (var like in likes)
                {
                    like.IsActive = false;
                    like.SuspendedDate = DateTime.UtcNow;
                }

                var pictures = await _context.Set<Picture>().Where(a => a.CompanyId == id).ToListAsync();
                foreach (var picture in pictures)
                {
                    picture.IsActive = false;
                    picture.SuspendedDate = DateTime.UtcNow;
                }

                var posts = await _context.Set<Post>().Where(a => a.CompanyId == id).ToListAsync();
                foreach (var post in posts)
                {
                    post.IsActive = false;
                    post.SuspendedDate = DateTime.UtcNow;
                }

                var reports = await _context.Set<Report>().Where(a => a.CompanyId == id).ToListAsync();
                foreach (var report in reports)
                {
                    report.IsActive = false;
                    report.SuspendedDate = DateTime.UtcNow;
                }

                var savedContents = await _context.Set<SavedContent>().Where(a => a.CompanyId == id).ToListAsync();
                foreach (var savedContent in savedContents)
                {
                    savedContent.IsActive = false;
                    savedContent.SuspendedDate = DateTime.UtcNow;
                }

                var surveys = await _context.Set<Survey>().Where(a => a.CompanyId == id).ToListAsync();
                foreach (var survey in surveys)
                {
                    survey.IsActive = false;
                    survey.SuspendedDate = DateTime.UtcNow;
                }

                var userSocialMedias = await _context.Set<UserSocialMedia>().Where(a => a.CompanyId == id).ToListAsync();
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
                var data = await _context.Set<Company>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (data == null)
                {
                    return false;
                }
                data.IsDeleted = true;
                data.DeletedDate = DateTime.UtcNow;

                var announcements = await _context.Set<Announcement>().Where(a => a.CompanyId == id).ToListAsync();
                foreach (var announcement in announcements)
                {
                    announcement.IsDeleted = true;
                    announcement.DeletedDate = DateTime.UtcNow;
                }

                var companyContacts = await _context.Set<CompanyContact>().Where(a => a.CompanyId == id).ToListAsync();
                foreach (var companyContact in companyContacts)
                {
                    companyContact.IsDeleted = true;
                    companyContact.DeletedDate = DateTime.UtcNow;
                }

                var companyFinances = await _context.Set<CompanyFinance>().Where(a => a.CompanyId == id).ToListAsync();
                foreach (var companyFinance in companyFinances)
                {
                    companyFinance.IsDeleted = true;
                    companyFinance.DeletedDate = DateTime.UtcNow;
                }

                var companyPinteches = await _context.Set<CompanyPintech>().Where(a => a.CompanyId == id).ToListAsync();
                foreach (var companyPintech in companyPinteches)
                {
                    companyPintech.IsDeleted = true;
                    companyPintech.DeletedDate = DateTime.UtcNow;
                }

                var companyStages = await _context.Set<CompanyStage>().Where(a => a.CompanyId == id).ToListAsync();
                foreach (var companyStage in companyStages)
                {
                    companyStage.IsDeleted = true;
                    companyStage.DeletedDate = DateTime.UtcNow;
                }

                var companyTeams = await _context.Set<CompanyTeam>().Where(a => a.CompanyId == id).ToListAsync();
                foreach (var companyTeam in companyTeams)
                {
                    companyTeam.IsDeleted = true;
                    companyTeam.DeletedDate = DateTime.UtcNow;
                }

                var comments = await _context.Set<Comment>().Where(a => a.CompanyId == id).ToListAsync();
                foreach (var comment in comments)
                {
                    comment.IsDeleted = true;
                    comment.DeletedDate = DateTime.UtcNow;
                }

                var followerCompanies = await _context.Set<Follow>().Where(a => a.FollowerCompanyId == id).ToListAsync();
                foreach (var follow in followerCompanies)
                {
                    follow.IsDeleted = true;
                    follow.DeletedDate = DateTime.UtcNow;
                }

                var followedCompanies = await _context.Set<Follow>().Where(a => a.FollowedCompanyId == id).ToListAsync();
                foreach (var followedCompany in followedCompanies)
                {
                    followedCompany.IsDeleted = true;
                    followedCompany.DeletedDate = DateTime.UtcNow;
                }

                var hits = await _context.Set<Hit>().Where(a => a.CompanyId == id).ToListAsync();
                foreach (var hit in hits)
                {
                    hit.IsDeleted = true;
                    hit.DeletedDate = DateTime.UtcNow;
                }

                var likes = await _context.Set<Like>().Where(a => a.CompanyId == id).ToListAsync();
                foreach (var like in likes)
                {
                    like.IsDeleted = true;
                    like.DeletedDate = DateTime.UtcNow;
                }

                var pictures = await _context.Set<Picture>().Where(a => a.CompanyId == id).ToListAsync();
                foreach (var picture in pictures)
                {
                    picture.IsDeleted = true;
                    picture.DeletedDate = DateTime.UtcNow;
                }

                var posts = await _context.Set<Post>().Where(a => a.CompanyId == id).ToListAsync();
                foreach (var post in posts)
                {
                    post.IsDeleted = true;
                    post.DeletedDate = DateTime.UtcNow;
                }

                var reports = await _context.Set<Report>().Where(a => a.CompanyId == id).ToListAsync();
                foreach (var report in reports)
                {
                    report.IsDeleted = true;
                    report.DeletedDate = DateTime.UtcNow;
                }

                var savedContents = await _context.Set<SavedContent>().Where(a => a.CompanyId == id).ToListAsync();
                foreach (var savedContent in savedContents)
                {
                    savedContent.IsDeleted = true;
                    savedContent.DeletedDate = DateTime.UtcNow;
                }

                var surveys = await _context.Set<Survey>().Where(a => a.CompanyId == id).ToListAsync();
                foreach (var survey in surveys)
                {
                    survey.IsDeleted = true;
                    survey.DeletedDate = DateTime.UtcNow;
                }

                var userSocialMedias = await _context.Set<UserSocialMedia>().Where(a => a.CompanyId == id).ToListAsync();
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
                var data = await _context.Set<Company>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (data == null)
                {
                    return false;
                }
                data.IsDeleted = false;

                var announcements = await _context.Set<Announcement>().Where(a => a.CompanyId == id).ToListAsync();
                foreach (var announcement in announcements)
                {
                    announcement.IsDeleted = false;
                }

                var companyContacts = await _context.Set<CompanyContact>().Where(a => a.CompanyId == id).ToListAsync();
                foreach (var companyContact in companyContacts)
                {
                    companyContact.IsDeleted = false;
                }

                var companyFinances = await _context.Set<CompanyFinance>().Where(a => a.CompanyId == id).ToListAsync();
                foreach (var companyFinance in companyFinances)
                {
                    companyFinance.IsDeleted = false;
                }

                var companyPinteches = await _context.Set<CompanyPintech>().Where(a => a.CompanyId == id).ToListAsync();
                foreach (var companyPintech in companyPinteches)
                {
                    companyPintech.IsDeleted = false;
                }

                var companyStages = await _context.Set<CompanyStage>().Where(a => a.CompanyId == id).ToListAsync();
                foreach (var companyStage in companyStages)
                {
                    companyStage.IsDeleted = false;
                }

                var companyTeams = await _context.Set<CompanyTeam>().Where(a => a.CompanyId == id).ToListAsync();
                foreach (var companyTeam in companyTeams)
                {
                    companyTeam.IsDeleted = false;
                }

                var comments = await _context.Set<Comment>().Where(a => a.CompanyId == id).ToListAsync();
                foreach (var comment in comments)
                {
                    comment.IsDeleted = false;
                }

                var followerCompanies = await _context.Set<Follow>().Where(a => a.FollowerCompanyId == id).ToListAsync();
                foreach (var follow in followerCompanies)
                {
                    follow.IsDeleted = false;
                }

                var followedCompanies = await _context.Set<Follow>().Where(a => a.FollowedCompanyId == id).ToListAsync();
                foreach (var follow in followedCompanies)
                {
                    follow.IsDeleted = false;
                }

                var hits = await _context.Set<Hit>().Where(a => a.CompanyId == id).ToListAsync();
                foreach (var hit in hits)
                {
                    hit.IsDeleted = false;
                }

                var likes = await _context.Set<Like>().Where(a => a.CompanyId == id).ToListAsync();
                foreach (var like in likes)
                {
                    like.IsDeleted = false;
                }

                var pictures = await _context.Set<Picture>().Where(a => a.CompanyId == id).ToListAsync();
                foreach (var picture in pictures)
                {
                    picture.IsDeleted = false;
                }

                var posts = await _context.Set<Post>().Where(a => a.CompanyId == id).ToListAsync();
                foreach (var post in posts)
                {
                    post.IsDeleted = false;
                }

                var reports = await _context.Set<Report>().Where(a => a.CompanyId == id).ToListAsync();
                foreach (var report in reports)
                {
                    report.IsDeleted = false;
                }

                var savedContents = await _context.Set<SavedContent>().Where(a => a.CompanyId == id).ToListAsync();
                foreach (var savedContent in savedContents)
                {
                    savedContent.IsDeleted = false;
                }

                var surveys = await _context.Set<Survey>().Where(a => a.CompanyId == id).ToListAsync();
                foreach (var survey in surveys)
                {
                    survey.IsDeleted = false;
                }

                var userSocialMedias = await _context.Set<UserSocialMedia>().Where(a => a.CompanyId == id).ToListAsync();
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

        public int CompanyCounter()
        {
            try
            {
                return _context.Companies.Count();
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public IEnumerable<Company> GetAllIncludingMostPopularCompanies()
        {
            //var startDate = DateTime.UtcNow.AddDays(-7);
            //var endDate = DateTime.UtcNow;

            try
            {
                var popularCompanies = _context.Companies
                    .Where(i => i.IsActive == true && i.IsDeleted == false)
                    .Include(i => i.Country)
                    .Include(i => i.AppUser)
                    .Include(i => i.CompanyCategory)
                    .Include(i => i.Sector)
                    .Include(i => i.SubSector)
                    .Include(i => i.Blogs)
                    .Include(i => i.Posts)
                    .Include(i => i.Hits)
                    .Include(i => i.Likes)
                    .Include(i => i.CompanyFinances)
                    .Include(i => i.CompanyPinteches)
                    .Include(i => i.CompanyStages)
                    .Include(i => i.SavedContents).AsSplitQuery()
                    .OrderByDescending(i =>
                        i.Blogs.Count(/*b => b.CreatedDate >= startDate && b.CreatedDate <= endDate*/) * 10.0 +
                        i.Posts.Count(/*p => p.CreatedDate >= startDate && p.CreatedDate <= endDate*/) * 10.0 +
                        i.Hits.Count(/*h => h.CreatedDate >= startDate && h.CreatedDate <= endDate*/) * 15.0 +
                        i.Likes.Count(/*l => l.CreatedDate >= startDate && l.CreatedDate <= endDate*/) * 30.0 +
                        i.CompanyFinances.Count() * 3.0 +
                        i.CompanyPinteches.Count() * 4.0 +
                        i.CompanyStages.Count() * 3.0 +
                        i.SavedContents.Count(/*s => s.CreatedDate >= startDate && s.CreatedDate <= endDate*/) * 25.0)
                    .Take(15).ToList();
                return popularCompanies;

                //    var popularInvestors = _context.Investors
                //.Where(i => i.IsActive == true && i.IsDeleted == false)
                //.Include(i => i.Blogs)
                //.Include(i => i.Posts)
                //.Include(i => i.Hits)
                //.Include(i => i.Likes)
                //.Include(i => i.CompanyFinances)
                //    .Include(i => i.CompanyPinteches)
                //    .Include(i => i.CompanyStages)
                //.Include(i => i.SavedContents)
                //.AsSplitQuery()
                //.OrderByDescending(i =>
                //    i.Blogs.Count() * 10.0 +
                //    i.Posts.Count() * 10.0 +
                //    i.Hits.Count() * 25.0 +
                //    i.Likes.Count() * 30.0 +
                //i.CompanyFinances.Count() * 3.0 +
                //        i.CompanyPinteches.Count() * 4.0 +
                //        i.CompanyStages.Count() * 3.0 +
                //    i.SavedContents.Count() * 25.0)
                //.Take(15)
                //.ToList();
            }
            catch (Exception)
            {
                return new List<Company>();
            }
        }

        public async Task<IEnumerable<Company>> GetAllIncludingMostPopularCompaniesAsync()
        {
            try
            {
                var popularCompanies = await _context.Companies
                    .Where(i => i.IsActive == true && i.IsDeleted == false)
                    .Include(i => i.Country)
                    .Include(i => i.AppUser)
                    .Include(i => i.CompanyCategory)
                    .Include(i => i.Sector)
                    .Include(i => i.SubSector)
                    .Include(i => i.Blogs)
                    .Include(i => i.Posts)
                    .Include(i => i.Hits)
                    .Include(i => i.Likes)
                    .Include(i => i.CompanyFinances)
                    .Include(i => i.CompanyPinteches)
                    .Include(i => i.CompanyStages)
                    .Include(i => i.SavedContents).AsSplitQuery()
                    .OrderByDescending(i =>
                        i.Blogs.Count(/*b => b.CreatedDate >= startDate && b.CreatedDate <= endDate*/) * 10.0 +
                        i.Posts.Count(/*p => p.CreatedDate >= startDate && p.CreatedDate <= endDate*/) * 10.0 +
                        i.Hits.Count(/*h => h.CreatedDate >= startDate && h.CreatedDate <= endDate*/) * 15.0 +
                        i.Likes.Count(/*l => l.CreatedDate >= startDate && l.CreatedDate <= endDate*/) * 30.0 +
                        i.CompanyFinances.Count() * 3.0 +
                        i.CompanyPinteches.Count() * 4.0 +
                        i.CompanyStages.Count() * 3.0 +
                        i.SavedContents.Count(/*s => s.CreatedDate >= startDate && s.CreatedDate <= endDate*/) * 25.0)
                    .Take(120).ToListAsync();
                return popularCompanies;
            }
            catch (Exception)
            {
                return new List<Company>();
            }
        }

        public async Task<IEnumerable<Company>> GetAllIncludingUnPopularCompaniesAsync()
        {
            try
            {
                var popularCompanies = await _context.Companies
                    .Where(i => i.IsActive == true && i.IsDeleted == false)
                    .Include(i => i.Country)
                    .Include(i => i.AppUser)
                    .Include(i => i.CompanyCategory)
                    .Include(i => i.Sector)
                    .Include(i => i.SubSector)
                    .Include(i => i.Blogs)
                    .Include(i => i.Posts)
                    .Include(i => i.Hits)
                    .Include(i => i.Likes)
                    .Include(i => i.CompanyFinances)
                    .Include(i => i.CompanyPinteches)
                    .Include(i => i.CompanyStages)
                    .Include(i => i.SavedContents).AsSplitQuery()
                    .OrderBy(i =>
                        i.Blogs.Count() * 10.0 +
                        i.Posts.Count() * 10.0 +
                        i.Hits.Count() * 15.0 +
                        i.Likes.Count() * 30.0 +
                        i.CompanyFinances.Count() * 3.0 +
                        i.CompanyPinteches.Count() * 4.0 +
                        i.CompanyStages.Count() * 3.0 +
                        i.SavedContents.Count() * 25.0)
                    .Take(120).ToListAsync();
                return popularCompanies;
            }
            catch (Exception)
            {
                return new List<Company>();
            }
        }
    }
}
