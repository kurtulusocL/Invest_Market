using System.Security.Claims;
using Investigation.DataAccess.Abstract;
using Investigation.DataAccess.Concrete.EntityFramework.Context.MSSQL;
using Investigation.Domain.Entities;
using Investigation.Domain.Entities.UserEntities;
using Investigation.Shared.DataAccess.EntityFramework;
using Investigation.Shared.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Investigation.DataAccess.Concrete.EntityFramework.Repositories
{
    public class UserRepository : EntityRepositoryBase<AppUser, ApplicationDbContext>, IUserRepository
    {
        readonly ApplicationDbContext _context;
        readonly UserManager<AppUser> _userManager;
        readonly IHttpContextAccessor _httpContextAccessor;
        public UserRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager) : base(context)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task<AppUser?> GetBySlugAsync(string slug)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(slug) || slug.Length != 8)
                    return null;

                var candidates = await _context.Users.AsNoTracking().Where(n => n.IsActive && !n.IsDeleted)
                    .Select(n => new
                    {
                        n.Id,
                        n.CreatedDate
                    }).ToListAsync();

                var match = candidates.FirstOrDefault(c =>
                {
                    var computedSlug = SecureSlugHelper.Generate(c.Id.ToString(), c.CreatedDate);
                    return string.Equals(computedSlug, slug, StringComparison.OrdinalIgnoreCase);
                });

                if (match == null)
                {
                    return null;
                }
                return await _context.Users.FirstOrDefaultAsync(n => n.Id == match.Id && n.IsActive && !n.IsDeleted);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred.", ex);
            }
        }
        public async Task<bool> SetActiveLoginConfirmCodeAsync(string id)
        {
            try
            {
                var active = await _context.Set<AppUser>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (active != null)
                {
                    active.IsLoginConfirmCodeActive = true;
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting Active Login Confirm Code the entity.", ex);
            }
        }

        public async Task<bool> SetDeActiveLoginConfirmCodeAsync(string id)
        {
            try
            {
                var active = await _context.Set<AppUser>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (active != null)
                {
                    active.IsLoginConfirmCodeActive = false;
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting DeActive Login Confirm Code the entity.", ex);
            }
        }

        public async Task<bool> SetActiveRegisterConfirmCodeAsync(string id)
        {
            try
            {
                var active = await _context.Set<AppUser>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (active != null)
                {
                    active.IsRegisterConfirmCodeActive = true;
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting Active Register Confirm Code the entity.", ex);
            }
        }

        public async Task<bool> SetDeActiveRegisterConfirmCodeAsync(string id)
        {
            try
            {
                var active = await _context.Set<AppUser>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (active != null)
                {
                    active.IsRegisterConfirmCodeActive = false;
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting DeActive Register Confirm Code the entity.", ex);
            }
        }

        public async Task<bool> SetFollowableAsync(string id)
        {
            try
            {
                var isFollowable = await _context.Set<AppUser>().Where(i => i.Id == id).FirstOrDefaultAsync();
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
                throw new Exception("An unexpected error occurred while setting Followable the entity.", ex);
            }
        }

        public async Task<bool> SetNotFollowableAsync(string id)
        {
            try
            {
                var isFollowable = await _context.Set<AppUser>().Where(i => i.Id == id).FirstOrDefaultAsync();
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
                throw new Exception("An unexpected error occurred while setting NotFollowable the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(string id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                var data = await _context.Set<AppUser>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (data == null)
                {
                    return false;
                }
                data.IsActive = true;

                var blogs = await _context.Set<Blog>().Where(a => a.AppUserId == id).ToListAsync();
                foreach (var blog in blogs)
                {
                    blog.IsActive = true;
                }

                var cancelMemberships = await _context.Set<CancelMembership>().Where(a => a.AppUserId == id).ToListAsync();
                foreach (var cancelMembership in cancelMemberships)
                {
                    cancelMembership.IsActive = true;
                }

                var comments = await _context.Set<Comment>().Where(a => a.AppUserId == id).ToListAsync();
                foreach (var comment in comments)
                {
                    comment.IsActive = true;
                }

                var commentAnswers = await _context.Set<CommentAnswer>().Where(a => a.AppUserId == id).ToListAsync();
                foreach (var commentAnswer in commentAnswers)
                {
                    commentAnswer.IsActive = true;
                }

                var companies = await _context.Set<Company>().Where(a => a.AppUserId == id).ToListAsync();
                foreach (var company in companies)
                {
                    company.IsActive = true;
                }

                var followeds = await _context.Set<Follow>().Where(a => a.FollowedUserId == id).ToListAsync();
                foreach (var followed in followeds)
                {
                    followed.IsActive = true;
                }

                var followers = await _context.Set<Follow>().Where(a => a.FollowerUserId == id).ToListAsync();
                foreach (var follower in followers)
                {
                    follower.IsActive = true;
                }

                var hits = await _context.Set<Hit>().Where(a => a.AppUserId == id).ToListAsync();
                foreach (var hit in hits)
                {
                    hit.IsActive = true;
                }

                var investors = await _context.Set<Investor>().Where(a => a.AppUserId == id).ToListAsync();
                foreach (var investor in investors)
                {
                    investor.IsActive = true;
                }

                var likes = await _context.Set<Like>().Where(a => a.AppUserId == id).ToListAsync();
                foreach (var like in likes)
                {
                    like.IsActive = true;
                }

                var posts = await _context.Set<Post>().Where(a => a.AppUserId == id).ToListAsync();
                foreach (var post in posts)
                {
                    post.IsActive = true;
                }

                var profileImages = await _context.Set<ProfileImage>().Where(a => a.AppUserId == id).ToListAsync();
                foreach (var profileImage in profileImages)
                {
                    profileImage.IsActive = true;
                }

                var reports = await _context.Set<Report>().Where(a => a.AppUserId == id).ToListAsync();
                foreach (var report in reports)
                {
                    report.IsActive = true;
                }

                var savedContents = await _context.Set<SavedContent>().Where(a => a.AppUserId == id).ToListAsync();
                foreach (var savedContent in savedContents)
                {
                    savedContent.IsActive = true;
                }

                var surveys = await _context.Set<Survey>().Where(a => a.AppUserId == id).ToListAsync();
                foreach (var survey in surveys)
                {
                    survey.IsActive = true;
                }

                var surveyAnswers = await _context.Set<SurveyAnswer>().Where(a => a.AppUserId == id).ToListAsync();
                foreach (var surveyAnswer in surveyAnswers)
                {
                    surveyAnswer.IsActive = true;
                }

                var surveyResponses = await _context.Set<SurveyResponse>().Where(a => a.AppUserId == id).ToListAsync();
                foreach (var surveyResponse in surveyResponses)
                {
                    surveyResponse.IsActive = true;
                }

                var userProfileImages = await _context.Set<UserProfileImage>().Where(a => a.AppUserId == id).ToListAsync();
                foreach (var userProfileImage in userProfileImages)
                {
                    userProfileImage.IsActive = true;
                }

                var userSessions = await _context.Set<UserSession>().Where(a => a.AppUserId == id).ToListAsync();
                foreach (var userSession in userSessions)
                {
                    userSession.IsActive = true;
                }

                var senderMessages = await _context.Set<Message>().Where(a => a.SenderId == id).ToListAsync();
                foreach (var senderMessage in senderMessages)
                {
                    senderMessage.IsActive = true;
                }

                var recieverMessages = await _context.Set<Message>().Where(a => a.ReceiverId == id).ToListAsync();
                foreach (var recieverMessage in recieverMessages)
                {
                    recieverMessage.IsActive = true;
                }

                var messageUserBlockeds = await _context.Set<MessageUserBlockList>().Where(a => a.BlockedId == id).ToListAsync();
                foreach (var messageUserBlocked in messageUserBlockeds)
                {
                    messageUserBlocked.IsActive = true;
                }

                var messageUserByBlockeds = await _context.Set<MessageUserBlockList>().Where(a => a.BlockerId == id).ToListAsync();
                foreach (var messageUserByBlocked in messageUserByBlockeds)
                {
                    messageUserByBlocked.IsActive = true;
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting Active the entity.", ex);
            }
        }

        public async Task<bool> SetDeActiveAsync(string id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                var data = await _context.Set<AppUser>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (data == null)
                {
                    return false;
                }
                data.IsActive = false;
                data.SuspendedDate = DateTime.UtcNow;

                var blogs = await _context.Set<Blog>().Where(a => a.AppUserId == id).ToListAsync();
                foreach (var blog in blogs)
                {
                    blog.IsActive = false;
                    blog.SuspendedDate = DateTime.UtcNow;
                }

                var cancelMemberships = await _context.Set<CancelMembership>().Where(a => a.AppUserId == id).ToListAsync();
                foreach (var cancelMembership in cancelMemberships)
                {
                    cancelMembership.IsActive = false;
                    cancelMembership.SuspendedDate = DateTime.UtcNow;
                }

                var comments = await _context.Set<Comment>().Where(a => a.AppUserId == id).ToListAsync();
                foreach (var comment in comments)
                {
                    comment.IsActive = false;
                    comment.SuspendedDate = DateTime.UtcNow;
                }

                var commentAnswers = await _context.Set<CommentAnswer>().Where(a => a.AppUserId == id).ToListAsync();
                foreach (var commentAnswer in commentAnswers)
                {
                    commentAnswer.IsActive = false;
                    commentAnswer.SuspendedDate = DateTime.UtcNow;
                }

                var companies = await _context.Set<Company>().Where(a => a.AppUserId == id).ToListAsync();
                foreach (var company in companies)
                {
                    company.IsActive = false;
                    company.SuspendedDate = DateTime.UtcNow;
                }

                var followeds = await _context.Set<Follow>().Where(a => a.FollowedUserId == id).ToListAsync();
                foreach (var followed in followeds)
                {
                    followed.IsActive = false;
                    followed.SuspendedDate = DateTime.UtcNow;
                }

                var followers = await _context.Set<Follow>().Where(a => a.FollowerUserId == id).ToListAsync();
                foreach (var follower in followers)
                {
                    follower.IsActive = false;
                    follower.SuspendedDate = DateTime.UtcNow;
                }

                var hits = await _context.Set<Hit>().Where(a => a.AppUserId == id).ToListAsync();
                foreach (var hit in hits)
                {
                    hit.IsActive = false;
                    hit.SuspendedDate = DateTime.UtcNow;
                }

                var investors = await _context.Set<Investor>().Where(a => a.AppUserId == id).ToListAsync();
                foreach (var investor in investors)
                {
                    investor.IsActive = false;
                    investor.SuspendedDate = DateTime.UtcNow;
                }

                var likes = await _context.Set<Like>().Where(a => a.AppUserId == id).ToListAsync();
                foreach (var like in likes)
                {
                    like.IsActive = false;
                    like.SuspendedDate = DateTime.UtcNow;
                }

                var posts = await _context.Set<Post>().Where(a => a.AppUserId == id).ToListAsync();
                foreach (var post in posts)
                {
                    post.IsActive = false;
                    post.SuspendedDate = DateTime.UtcNow;
                }

                var profileImages = await _context.Set<ProfileImage>().Where(a => a.AppUserId == id).ToListAsync();
                foreach (var profileImage in profileImages)
                {
                    profileImage.IsActive = false;
                    profileImage.SuspendedDate = DateTime.UtcNow;
                }

                var reports = await _context.Set<Report>().Where(a => a.AppUserId == id).ToListAsync();
                foreach (var report in reports)
                {
                    report.IsActive = false;
                    report.SuspendedDate = DateTime.UtcNow;
                }

                var savedContents = await _context.Set<SavedContent>().Where(a => a.AppUserId == id).ToListAsync();
                foreach (var savedContent in savedContents)
                {
                    savedContent.IsActive = false;
                    savedContent.SuspendedDate = DateTime.UtcNow;
                }

                var surveys = await _context.Set<Survey>().Where(a => a.AppUserId == id).ToListAsync();
                foreach (var survey in surveys)
                {
                    survey.IsActive = false;
                    survey.SuspendedDate = DateTime.UtcNow;
                }

                var surveyAnswers = await _context.Set<SurveyAnswer>().Where(a => a.AppUserId == id).ToListAsync();
                foreach (var surveyAnswer in surveyAnswers)
                {
                    surveyAnswer.IsActive = false;
                    surveyAnswer.SuspendedDate = DateTime.UtcNow;
                }

                var surveyResponses = await _context.Set<SurveyResponse>().Where(a => a.AppUserId == id).ToListAsync();
                foreach (var surveyResponse in surveyResponses)
                {
                    surveyResponse.IsActive = false;
                    surveyResponse.SuspendedDate = DateTime.UtcNow;
                }

                var userProfileImages = await _context.Set<UserProfileImage>().Where(a => a.AppUserId == id).ToListAsync();
                foreach (var userProfileImage in userProfileImages)
                {
                    userProfileImage.IsActive = false;
                    userProfileImage.SuspendedDate = DateTime.UtcNow;
                }

                var userSessions = await _context.Set<UserSession>().Where(a => a.AppUserId == id).ToListAsync();
                foreach (var userSession in userSessions)
                {
                    userSession.IsActive = false;
                    userSession.SuspendedDate = DateTime.UtcNow;
                }

                var senderMessages = await _context.Set<Message>().Where(a => a.SenderId == id).ToListAsync();
                foreach (var senderMessage in senderMessages)
                {
                    senderMessage.IsActive = false;
                    senderMessage.SuspendedDate = DateTime.UtcNow;
                }

                var recieverMessages = await _context.Set<Message>().Where(a => a.ReceiverId == id).ToListAsync();
                foreach (var recieverMessage in recieverMessages)
                {
                    recieverMessage.IsActive = false;
                    recieverMessage.SuspendedDate = DateTime.UtcNow;
                }

                var messageUserBlockeds = await _context.Set<MessageUserBlockList>().Where(a => a.BlockedId == id).ToListAsync();
                foreach (var messageUserBlocked in messageUserBlockeds)
                {
                    messageUserBlocked.IsActive = false;
                    messageUserBlocked.SuspendedDate = DateTime.UtcNow;
                }

                var messageUserByBlockeds = await _context.Set<MessageUserBlockList>().Where(a => a.BlockerId == id).ToListAsync();
                foreach (var messageUserByBlocked in messageUserByBlockeds)
                {
                    messageUserByBlocked.IsActive = false;
                    messageUserByBlocked.SuspendedDate = DateTime.UtcNow;
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting DeActive the entity.", ex);
            }
        }

        public async Task<bool> SetDeletedAsync(string id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                var data = await _context.Set<AppUser>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (data == null)
                {
                    return false;
                }
                data.IsDeleted = true;
                data.DeletedDate = DateTime.UtcNow;

                var blogs = await _context.Set<Blog>().Where(a => a.AppUserId == id).ToListAsync();
                foreach (var blog in blogs)
                {
                    blog.IsDeleted = true;
                    blog.DeletedDate = DateTime.UtcNow;
                }

                var cancelMemberships = await _context.Set<CancelMembership>().Where(a => a.AppUserId == id).ToListAsync();
                foreach (var cancelMembership in cancelMemberships)
                {
                    cancelMembership.IsDeleted = true;
                    cancelMembership.DeletedDate = DateTime.UtcNow;
                }

                var comments = await _context.Set<Comment>().Where(a => a.AppUserId == id).ToListAsync();
                foreach (var comment in comments)
                {
                    comment.IsDeleted = true;
                    comment.DeletedDate = DateTime.UtcNow;
                }

                var commentAnswers = await _context.Set<CommentAnswer>().Where(a => a.AppUserId == id).ToListAsync();
                foreach (var commentAnswer in commentAnswers)
                {
                    commentAnswer.IsDeleted = true;
                    commentAnswer.DeletedDate = DateTime.UtcNow;
                }

                var companies = await _context.Set<Company>().Where(a => a.AppUserId == id).ToListAsync();
                foreach (var company in companies)
                {
                    company.IsDeleted = true;
                    company.DeletedDate = DateTime.UtcNow;
                }

                var followeds = await _context.Set<Follow>().Where(a => a.FollowedUserId == id).ToListAsync();
                foreach (var followed in followeds)
                {
                    followed.IsDeleted = true;
                    followed.DeletedDate = DateTime.UtcNow;
                }

                var followers = await _context.Set<Follow>().Where(a => a.FollowerUserId == id).ToListAsync();
                foreach (var follower in followers)
                {
                    follower.IsDeleted = true;
                    follower.DeletedDate = DateTime.UtcNow;
                }

                var hits = await _context.Set<Hit>().Where(a => a.AppUserId == id).ToListAsync();
                foreach (var hit in hits)
                {
                    hit.IsDeleted = true;
                    hit.DeletedDate = DateTime.UtcNow;
                }

                var investors = await _context.Set<Investor>().Where(a => a.AppUserId == id).ToListAsync();
                foreach (var investor in investors)
                {
                    investor.IsDeleted = true;
                    investor.DeletedDate = DateTime.UtcNow;
                }

                var likes = await _context.Set<Like>().Where(a => a.AppUserId == id).ToListAsync();
                foreach (var like in likes)
                {
                    like.IsDeleted = true;
                    like.DeletedDate = DateTime.UtcNow;
                }

                var posts = await _context.Set<Post>().Where(a => a.AppUserId == id).ToListAsync();
                foreach (var post in posts)
                {
                    post.IsDeleted = true;
                    post.DeletedDate = DateTime.UtcNow;
                }

                var profileImages = await _context.Set<ProfileImage>().Where(a => a.AppUserId == id).ToListAsync();
                foreach (var profileImage in profileImages)
                {
                    profileImage.IsDeleted = true;
                    profileImage.DeletedDate = DateTime.UtcNow;
                }

                var reports = await _context.Set<Report>().Where(a => a.AppUserId == id).ToListAsync();
                foreach (var report in reports)
                {
                    report.IsDeleted = true;
                    report.DeletedDate = DateTime.UtcNow;
                }

                var savedContents = await _context.Set<SavedContent>().Where(a => a.AppUserId == id).ToListAsync();
                foreach (var savedContent in savedContents)
                {
                    savedContent.IsDeleted = true;
                    savedContent.DeletedDate = DateTime.UtcNow;
                }

                var surveys = await _context.Set<Survey>().Where(a => a.AppUserId == id).ToListAsync();
                foreach (var survey in surveys)
                {
                    survey.IsDeleted = true;
                    survey.DeletedDate = DateTime.UtcNow;
                }

                var surveyAnswers = await _context.Set<SurveyAnswer>().Where(a => a.AppUserId == id).ToListAsync();
                foreach (var surveyAnswer in surveyAnswers)
                {
                    surveyAnswer.IsDeleted = true;
                    surveyAnswer.DeletedDate = DateTime.UtcNow;
                }

                var surveyResponses = await _context.Set<SurveyResponse>().Where(a => a.AppUserId == id).ToListAsync();
                foreach (var surveyResponse in surveyResponses)
                {
                    surveyResponse.IsDeleted = true;
                    surveyResponse.DeletedDate = DateTime.UtcNow;
                }

                var userProfileImages = await _context.Set<UserProfileImage>().Where(a => a.AppUserId == id).ToListAsync();
                foreach (var userProfileImage in userProfileImages)
                {
                    userProfileImage.IsDeleted = true;
                    userProfileImage.DeletedDate = DateTime.UtcNow;
                }

                var userSessions = await _context.Set<UserSession>().Where(a => a.AppUserId == id).ToListAsync();
                foreach (var userSession in userSessions)
                {
                    userSession.IsDeleted = true;
                    userSession.DeletedDate = DateTime.UtcNow;
                }

                var senderMessages = await _context.Set<Message>().Where(a => a.SenderId == id).ToListAsync();
                foreach (var senderMessage in senderMessages)
                {
                    senderMessage.IsDeleted = true;
                    senderMessage.DeletedDate = DateTime.UtcNow;
                }

                var recieverMessages = await _context.Set<Message>().Where(a => a.ReceiverId == id).ToListAsync();
                foreach (var recieverMessage in recieverMessages)
                {
                    recieverMessage.IsDeleted = true;
                    recieverMessage.DeletedDate = DateTime.UtcNow;
                }

                var messageUserBlockeds = await _context.Set<MessageUserBlockList>().Where(a => a.BlockedId == id).ToListAsync();
                foreach (var messageUserBlocked in messageUserBlockeds)
                {
                    messageUserBlocked.IsDeleted = true;
                    messageUserBlocked.DeletedDate = DateTime.UtcNow;
                }

                var messageUserByBlockeds = await _context.Set<MessageUserBlockList>().Where(a => a.BlockerId == id).ToListAsync();
                foreach (var messageUserByBlocked in messageUserByBlockeds)
                {
                    messageUserByBlocked.IsDeleted = true;
                    messageUserByBlocked.DeletedDate = DateTime.UtcNow;
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting Deleted the entity.", ex);
            }
        }

        public async Task<bool> SetNotDeletedAsync(string id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                var data = await _context.Set<AppUser>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (data == null)
                {
                    return false;
                }
                data.IsDeleted = false;

                var blogs = await _context.Set<Blog>().Where(a => a.AppUserId == id).ToListAsync();
                foreach (var blog in blogs)
                {
                    blog.IsDeleted = false;
                }

                var cancelMemberships = await _context.Set<CancelMembership>().Where(a => a.AppUserId == id).ToListAsync();
                foreach (var cancelMembership in cancelMemberships)
                {
                    cancelMembership.IsDeleted = false;
                }

                var comments = await _context.Set<Comment>().Where(a => a.AppUserId == id).ToListAsync();
                foreach (var comment in comments)
                {
                    comment.IsDeleted = false;
                }

                var commentAnswers = await _context.Set<CommentAnswer>().Where(a => a.AppUserId == id).ToListAsync();
                foreach (var commentAnswer in commentAnswers)
                {
                    commentAnswer.IsDeleted = false;
                }

                var companies = await _context.Set<Company>().Where(a => a.AppUserId == id).ToListAsync();
                foreach (var company in companies)
                {
                    company.IsDeleted = false;
                }

                var followeds = await _context.Set<Follow>().Where(a => a.FollowedUserId == id).ToListAsync();
                foreach (var followed in followeds)
                {
                    followed.IsDeleted = false;
                }

                var followers = await _context.Set<Follow>().Where(a => a.FollowerUserId == id).ToListAsync();
                foreach (var follower in followers)
                {
                    follower.IsDeleted = false;
                }

                var hits = await _context.Set<Hit>().Where(a => a.AppUserId == id).ToListAsync();
                foreach (var hit in hits)
                {
                    hit.IsDeleted = false;
                }

                var investors = await _context.Set<Investor>().Where(a => a.AppUserId == id).ToListAsync();
                foreach (var investor in investors)
                {
                    investor.IsDeleted = false;
                }

                var likes = await _context.Set<Like>().Where(a => a.AppUserId == id).ToListAsync();
                foreach (var like in likes)
                {
                    like.IsDeleted = false;
                }

                var posts = await _context.Set<Post>().Where(a => a.AppUserId == id).ToListAsync();
                foreach (var post in posts)
                {
                    post.IsDeleted = false;
                }

                var profileImages = await _context.Set<ProfileImage>().Where(a => a.AppUserId == id).ToListAsync();
                foreach (var profileImage in profileImages)
                {
                    profileImage.IsDeleted = false;
                }

                var reports = await _context.Set<Report>().Where(a => a.AppUserId == id).ToListAsync();
                foreach (var report in reports)
                {
                    report.IsDeleted = false;
                }

                var savedContents = await _context.Set<SavedContent>().Where(a => a.AppUserId == id).ToListAsync();
                foreach (var savedContent in savedContents)
                {
                    savedContent.IsDeleted = false;
                }

                var surveys = await _context.Set<Survey>().Where(a => a.AppUserId == id).ToListAsync();
                foreach (var survey in surveys)
                {
                    survey.IsDeleted = false;
                }

                var surveyAnswers = await _context.Set<SurveyAnswer>().Where(a => a.AppUserId == id).ToListAsync();
                foreach (var surveyAnswer in surveyAnswers)
                {
                    surveyAnswer.IsDeleted = false;
                }

                var surveyResponses = await _context.Set<SurveyResponse>().Where(a => a.AppUserId == id).ToListAsync();
                foreach (var surveyResponse in surveyResponses)
                {
                    surveyResponse.IsDeleted = false;
                }

                var userProfileImages = await _context.Set<UserProfileImage>().Where(a => a.AppUserId == id).ToListAsync();
                foreach (var userProfileImage in userProfileImages)
                {
                    userProfileImage.IsDeleted = false;
                }

                var userSessions = await _context.Set<UserSession>().Where(a => a.AppUserId == id).ToListAsync();
                foreach (var userSession in userSessions)
                {
                    userSession.IsDeleted = false;
                }

                var senderMessages = await _context.Set<Message>().Where(a => a.SenderId == id).ToListAsync();
                foreach (var senderMessage in senderMessages)
                {
                    senderMessage.IsDeleted = false;
                }

                var recieverMessages = await _context.Set<Message>().Where(a => a.ReceiverId == id).ToListAsync();
                foreach (var recieverMessage in recieverMessages)
                {
                    recieverMessage.IsDeleted = false;
                }

                var messageUserBlockeds = await _context.Set<MessageUserBlockList>().Where(a => a.BlockedId == id).ToListAsync();
                foreach (var messageUserBlocked in messageUserBlockeds)
                {
                    messageUserBlocked.IsDeleted = false;
                }

                var messageUserByBlockeds = await _context.Set<MessageUserBlockList>().Where(a => a.BlockerId == id).ToListAsync();
                foreach (var messageUserByBlocked in messageUserByBlockeds)
                {
                    messageUserByBlocked.IsDeleted = false;
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting NotDeleted the entity.", ex);
            }
        }

        public int UserCounter()
        {
            try
            {
                return _context.Users.Count(i => i.IsAdmin == false);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public IEnumerable<AppUser> GetAllIncludingMostPopularEntrepreneurs()
        {
            try
            {
                var popularInvestors = _context.Users
                       .Where(i => i.IsActive == true && i.IsDeleted == false && i.IsCompany == true && i.IsInvestor == false && i.Companies.Count() == 0)
                       .Include(i => i.Companies)
                       .Include(i => i.Blogs)
                       .Include(i => i.Posts)
                       .Include(i => i.Hits)
                       .Include(i => i.Likes)
                       .Include(i => i.Surveys)
                       .Include(i => i.Comments)
                       .Include(i => i.CommentAnswers)
                       .Include(i => i.SavedContents).AsSplitQuery()
                       .OrderByDescending(i =>
                           i.Blogs.Count() * 10.0 +
                           i.Posts.Count() * 10.0 +
                           i.Hits.Count() * 10.0 +
                           i.Likes.Count() * 30.0 +
                           i.Surveys.Count() * 5.0 +
                           i.Comments.Count() * 5.0 +
                           i.CommentAnswers.Count() * 5.0 +
                           i.SavedContents.Count() * 25.0)
                       .Take(15).ToList();
                return popularInvestors;
            }
            catch (Exception)
            {
                return new List<AppUser>();
            }
        }

        public async Task<IEnumerable<AppUser>> GetAllIncludingMostPopularEntrepreneursAsync()
        {
            try
            {
                var popularInvestors = await _context.Users
                       .Where(i => i.IsActive == true && i.IsDeleted == false && i.IsCompany == false && i.Companies.Count() == 0)
                       .Include(i => i.Companies)
                       .Include(i => i.Blogs)
                       .Include(i => i.Posts)
                       .Include(i => i.Hits)
                       .Include(i => i.Likes)
                       .Include(i => i.Surveys)
                       .Include(i => i.Comments)
                       .Include(i => i.CommentAnswers)
                       .Include(i => i.SavedContents).AsSplitQuery()
                       .OrderByDescending(i =>
                           i.Blogs.Count() * 10.0 +
                           i.Posts.Count() * 10.0 +
                           i.Hits.Count() * 10.0 +
                           i.Likes.Count() * 30.0 +
                           i.Surveys.Count() * 5.0 +
                           i.Comments.Count() * 5.0 +
                           i.CommentAnswers.Count() * 5.0 +
                           i.SavedContents.Count() * 25.0)
                       .Take(120).ToListAsync();
                return popularInvestors;
            }
            catch (Exception)
            {
                return new List<AppUser>();
            }
        }

        public async Task<AppUser?> GetCurrentUserAsync()
        {
            try
            {
                var httpContext = _httpContextAccessor.HttpContext;
                if (httpContext?.User?.Identity?.IsAuthenticated != true)
                    return null;

                var userId = _userManager.GetUserId(httpContext.User);
                if (string.IsNullOrEmpty(userId))
                    return null;

                return await _userManager.FindByIdAsync(userId);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred.", ex);
            }
        }

        public Guid? GetCurrentUserId()
        {
            try
            {
                var httpContext = _httpContextAccessor.HttpContext;
                if (httpContext?.User?.Identity?.IsAuthenticated != true)
                    return null;

                var userIdClaim = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                                  ?? httpContext.User.FindFirst("sub")?.Value;

                return Guid.TryParse(userIdClaim, out var guid) ? guid : null;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred.", ex);
            }
        }

        public async Task<IEnumerable<AppUser>> GetAllIncludingUnPopularEntrepreneursAsync()
        {
            try
            {
                var popularInvestors = await _context.Users
                       .Where(i => i.IsActive == true && i.IsDeleted == false && i.IsCompany == false && i.Companies.Count() == 0)
                       .Include(i => i.Companies)
                       .Include(i => i.Blogs)
                       .Include(i => i.Posts)
                       .Include(i => i.Hits)
                       .Include(i => i.Likes)
                       .Include(i => i.Surveys)
                       .Include(i => i.Comments)
                       .Include(i => i.CommentAnswers)
                       .Include(i => i.SavedContents).AsSplitQuery()
                       .OrderBy(i =>
                           i.Blogs.Count() * 10.0 +
                           i.Posts.Count() * 10.0 +
                           i.Hits.Count() * 10.0 +
                           i.Likes.Count() * 30.0 +
                           i.Surveys.Count() * 5.0 +
                           i.Comments.Count() * 5.0 +
                           i.CommentAnswers.Count() * 5.0 +
                           i.SavedContents.Count() * 25.0)
                       .Take(120).ToListAsync();
                return popularInvestors;
            }
            catch (Exception)
            {
                return new List<AppUser>();
            }
        }
    }
}
