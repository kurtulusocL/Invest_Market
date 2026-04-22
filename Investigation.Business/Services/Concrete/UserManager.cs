using System.Linq.Expressions;
using Investigation.Business.Services.Abstract;
using Investigation.DataAccess.Abstract;
using Investigation.Domain.Entities.UserEntities;

namespace Investigation.Business.Services.Concrete
{
    public class UserManager : IUserService
    {
        readonly IUserRepository _userRepository;
        public UserManager(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> DeleteAllByIdAsync(List<string> ids)
        {
            try
            {
                if (ids == null || !ids.Any())
                    throw new ArgumentNullException(nameof(ids), "id list was null or empty");

                var result = await _userRepository.DeleteByIdsAsync(ids.Cast<object>());
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while bulk deleting entities.", ex);
            }
        }

        public async Task<bool> DeleteAsync(AppUser entity, string id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var data = await _userRepository.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _userRepository.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public IQueryable<AppUser> GetAllIncludeLastJoinedUserForAdminHome()
        {
            try
            {
                return _userRepository.GetAllInclude(new Expression<Func<AppUser, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsAdmin==false
                }, null).OrderByDescending(i => i.CreatedDate).Take(20);
            }
            catch (Exception)
            {
                return Enumerable.Empty<AppUser>().AsQueryable();
            }
        }

        public IQueryable<AppUser> GetAllIncludingAsync()
        {
            try
            {
                var data = _userRepository.GetAllInclude(new Expression<Func<AppUser, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Blogs, y => y.CancelMemberships, y => y.Comments, y => y.CommentAnswers, y => y.Companies, y => y.Hits, y => y.Investors, y => y.Likes, y => y.Posts, y => y.ProfileImages, y => y.Reports, y => y.SavedContents, y => y.Surveys, y => y.SurveyAnswers, y => y.SurveyResponses, y => y.UserProfileImages, y => y.UserSessions, y => y.MyFollowers, y => y.MyFollowings);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<AppUser>().AsQueryable();
            }
        }

        public IQueryable<AppUser> GetAllIncludingByActiveLoginConfirmCodeAdminAsync()
        {
            try
            {
                var data = _userRepository.GetAllInclude(new Expression<Func<AppUser, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsAdmin==true,
                    i=>i.IsLoginConfirmCodeActive==true
                }, null, y => y.Blogs, y => y.CancelMemberships, y => y.Comments, y => y.CommentAnswers, y => y.Companies, y => y.Hits, y => y.Investors, y => y.Likes, y => y.Posts, y => y.ProfileImages, y => y.Reports, y => y.SavedContents, y => y.Surveys, y => y.SurveyAnswers, y => y.SurveyResponses, y => y.UserProfileImages, y => y.UserSessions, y => y.MyFollowers, y => y.MyFollowings);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<AppUser>().AsQueryable();
            }
        }

        public IQueryable<AppUser> GetAllIncludingByActiveLoginConfirmCodeUserAsync()
        {
            try
            {
                var data = _userRepository.GetAllInclude(new Expression<Func<AppUser, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsAdmin==false,
                    i=>i.IsLoginConfirmCodeActive==true
                }, null, y => y.Blogs, y => y.CancelMemberships, y => y.Comments, y => y.CommentAnswers, y => y.Companies, y => y.Hits, y => y.Investors, y => y.Likes, y => y.Posts, y => y.ProfileImages, y => y.Reports, y => y.SavedContents, y => y.Surveys, y => y.SurveyAnswers, y => y.SurveyResponses, y => y.UserProfileImages, y => y.UserSessions, y => y.MyFollowers, y => y.MyFollowings);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<AppUser>().AsQueryable();
            }
        }

        public IQueryable<AppUser> GetAllIncludingByActiveRegisterConfirmCodeAdminAsync()
        {
            try
            {
                var data = _userRepository.GetAllInclude(new Expression<Func<AppUser, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsAdmin==true,
                    i=>i.IsRegisterConfirmCodeActive==true
                }, null, y => y.Blogs, y => y.CancelMemberships, y => y.Comments, y => y.CommentAnswers, y => y.Companies, y => y.Hits, y => y.Investors, y => y.Likes, y => y.Posts, y => y.ProfileImages, y => y.Reports, y => y.SavedContents, y => y.Surveys, y => y.SurveyAnswers, y => y.SurveyResponses, y => y.UserProfileImages, y => y.UserSessions, y => y.MyFollowers, y => y.MyFollowings);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<AppUser>().AsQueryable();
            }
        }

        public IQueryable<AppUser> GetAllIncludingByActiveRegisterConfirmCodeUserAsync()
        {
            try
            {
                var data = _userRepository.GetAllInclude(new Expression<Func<AppUser, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsAdmin==false,
                    i=>i.IsRegisterConfirmCodeActive==true
                }, null, y => y.Blogs, y => y.CancelMemberships, y => y.Comments, y => y.CommentAnswers, y => y.Companies, y => y.Hits, y => y.Investors, y => y.Likes, y => y.Posts, y => y.ProfileImages, y => y.Reports, y => y.SavedContents, y => y.Surveys, y => y.SurveyAnswers, y => y.SurveyResponses, y => y.UserProfileImages, y => y.UserSessions, y => y.MyFollowers, y => y.MyFollowings);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<AppUser>().AsQueryable();
            }
        }

        public IQueryable<AppUser> GetAllIncludingByAdminAsync()
        {
            try
            {
                var data = _userRepository.GetAllInclude(new Expression<Func<AppUser, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsAdmin==true
                }, null, y => y.Blogs, y => y.CancelMemberships, y => y.Comments, y => y.CommentAnswers, y => y.Companies, y => y.Hits, y => y.Investors, y => y.Likes, y => y.Posts, y => y.ProfileImages, y => y.Reports, y => y.SavedContents, y => y.Surveys, y => y.SurveyAnswers, y => y.SurveyResponses, y => y.UserProfileImages, y => y.UserSessions, y => y.MyFollowers, y => y.MyFollowings);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<AppUser>().AsQueryable();
            }
        }

        public IQueryable<AppUser> GetAllIncludingByCompanyAsync()
        {
            try
            {
                var data = _userRepository.GetAllInclude(new Expression<Func<AppUser, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsCompany==true,
                }, null, y => y.Blogs, y => y.CancelMemberships, y => y.Comments, y => y.CommentAnswers, y => y.Companies, y => y.Hits, y => y.Investors, y => y.Likes, y => y.Posts, y => y.ProfileImages, y => y.Reports, y => y.SavedContents, y => y.Surveys, y => y.SurveyAnswers, y => y.SurveyResponses, y => y.UserProfileImages, y => y.UserSessions, y => y.MyFollowers, y => y.MyFollowings);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<AppUser>().AsQueryable();
            }
        }

        public IQueryable<AppUser> GetAllIncludingByDeletedAdminAsync()
        {
            try
            {
                var data = _userRepository.GetAllInclude(new Expression<Func<AppUser, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==true,
                    i=>i.IsAdmin==true,
                    i=>i.DeletedDate!=null
                }, null, y => y.Blogs, y => y.CancelMemberships, y => y.Comments, y => y.CommentAnswers, y => y.Companies, y => y.Hits, y => y.Investors, y => y.Likes, y => y.Posts, y => y.ProfileImages, y => y.Reports, y => y.SavedContents, y => y.Surveys, y => y.SurveyAnswers, y => y.SurveyResponses, y => y.UserProfileImages, y => y.UserSessions, y => y.MyFollowers, y => y.MyFollowings);
                return data.OrderByDescending(i => i.DeletedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<AppUser>().AsQueryable();
            }
        }

        public IQueryable<AppUser> GetAllIncludingByDeletedUserAsync()
        {
            try
            {
                var data = _userRepository.GetAllInclude(new Expression<Func<AppUser, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==true,
                    i=>i.IsAdmin==false,
                    i=>i.DeletedDate!=null
                }, null, y => y.Blogs, y => y.CancelMemberships, y => y.Comments, y => y.CommentAnswers, y => y.Companies, y => y.Hits, y => y.Investors, y => y.Likes, y => y.Posts, y => y.ProfileImages, y => y.Reports, y => y.SavedContents, y => y.Surveys, y => y.SurveyAnswers, y => y.SurveyResponses, y => y.UserProfileImages, y => y.UserSessions, y => y.MyFollowers, y => y.MyFollowings);
                return data.OrderByDescending(i => i.DeletedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<AppUser>().AsQueryable();
            }
        }

        public IQueryable<AppUser> GetAllIncludingByInvestorAsync()
        {
            try
            {
                var data = _userRepository.GetAllInclude(new Expression<Func<AppUser, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsInvestor==true
                }, null, y => y.Blogs, y => y.CancelMemberships, y => y.Comments, y => y.CommentAnswers, y => y.Companies, y => y.Hits, y => y.Investors, y => y.Likes, y => y.Posts, y => y.ProfileImages, y => y.Reports, y => y.SavedContents, y => y.Surveys, y => y.SurveyAnswers, y => y.SurveyResponses, y => y.UserProfileImages, y => y.UserSessions, y => y.MyFollowers, y => y.MyFollowings);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<AppUser>().AsQueryable();
            }
        }

        public IQueryable<AppUser> GetAllIncludingBySuspendedAdminAsync()
        {
            try
            {
                var data = _userRepository.GetAllInclude(new Expression<Func<AppUser, bool>>[]
                {
                    i=>i.IsActive==false,
                    i=>i.IsDeleted==false,
                    i=>i.IsAdmin==true,
                    i=>i.SuspendedDate!=null
                }, null, y => y.Blogs, y => y.CancelMemberships, y => y.Comments, y => y.CommentAnswers, y => y.Companies, y => y.Hits, y => y.Investors, y => y.Likes, y => y.Posts, y => y.ProfileImages, y => y.Reports, y => y.SavedContents, y => y.Surveys, y => y.SurveyAnswers, y => y.SurveyResponses, y => y.UserProfileImages, y => y.UserSessions, y => y.MyFollowers, y => y.MyFollowings);
                return data.OrderByDescending(i => i.SuspendedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<AppUser>().AsQueryable();
            }
        }

        public IQueryable<AppUser> GetAllIncludingBySuspendedUserAsync()
        {
            try
            {
                var data = _userRepository.GetAllInclude(new Expression<Func<AppUser, bool>>[]
                {
                    i=>i.IsActive==false,
                    i=>i.IsDeleted==false,
                    i=>i.IsAdmin==false,
                    i=>i.SuspendedDate!=null
                }, null, y => y.Blogs, y => y.CancelMemberships, y => y.Comments, y => y.CommentAnswers, y => y.Companies, y => y.Hits, y => y.Investors, y => y.Likes, y => y.Posts, y => y.ProfileImages, y => y.Reports, y => y.SavedContents, y => y.Surveys, y => y.SurveyAnswers, y => y.SurveyResponses, y => y.UserProfileImages, y => y.UserSessions, y => y.MyFollowers, y => y.MyFollowings);
                return data.OrderByDescending(i => i.SuspendedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<AppUser>().AsQueryable();
            }
        }

        public IQueryable<AppUser> GetAllIncludingByUserAsync()
        {
            try
            {
                var data = _userRepository.GetAllInclude(new Expression<Func<AppUser, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsAdmin==false
                }, null, y => y.Blogs, y => y.CancelMemberships, y => y.Comments, y => y.CommentAnswers, y => y.Companies, y => y.Hits, y => y.Investors, y => y.Likes, y => y.Posts, y => y.ProfileImages, y => y.Reports, y => y.SavedContents, y => y.Surveys, y => y.SurveyAnswers, y => y.SurveyResponses, y => y.UserProfileImages, y => y.UserSessions, y => y.MyFollowers, y => y.MyFollowings);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<AppUser>().AsQueryable();
            }
        }

        public IQueryable<AppUser> GetAllIncludingForManagementAsync()
        {
            try
            {
                var data = _userRepository.GetAllInclude(new Expression<Func<AppUser, bool>>[]
                {

                }, null, y => y.Blogs, y => y.CancelMemberships, y => y.Comments, y => y.CommentAnswers, y => y.Companies, y => y.Hits, y => y.Investors, y => y.Likes, y => y.Posts, y => y.ProfileImages, y => y.Reports, y => y.SavedContents, y => y.Surveys, y => y.SurveyAnswers, y => y.SurveyResponses, y => y.UserProfileImages, y => y.UserSessions, y => y.MyFollowers, y => y.MyFollowings);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<AppUser>().AsQueryable();
            }
        }

        public IQueryable<AppUser> GetAllIncludingLastEntrepreneur()
        {
            try
            {
                return _userRepository.GetAllInclude(new Expression<Func<AppUser, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsInvestor==true,
                    i=>i.Investors.Count==0
                }, null, y => y.Companies, y => y.Investors).OrderByDescending(i => i.CreatedDate).Take(15);
            }
            catch (Exception)
            {
                return Enumerable.Empty<AppUser>().AsQueryable();
            }
        }

        public IEnumerable<AppUser> GetAllIncludingMostPopularEntrepreneurs()
        {
            return _userRepository.GetAllIncludingMostPopularEntrepreneurs();
        }

        public async Task<IEnumerable<AppUser>> GetAllIncludingMostPopularEntrepreneursAsync()
        {
            return await _userRepository.GetAllIncludingMostPopularEntrepreneursAsync();
        }

        public IQueryable<AppUser> GetAllIncludingTodaysUsersForAdminHeader()
        {
            try
            {
                var today = DateTime.Today;
                return _userRepository.GetAll(i => i.IsActive == true && i.IsDeleted == false && i.CreatedDate >= today && i.CreatedDate < today.AddDays(1)).OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<AppUser>().AsQueryable();
            }
        }

        public IQueryable<AppUser> GetAllIncludingUserByCountryAsync()
        {
            try
            {
                var data = _userRepository.GetAllInclude(new Expression<Func<AppUser, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Blogs, y => y.CancelMemberships, y => y.Comments, y => y.CommentAnswers, y => y.Companies, y => y.Hits, y => y.Investors, y => y.Likes, y => y.Posts, y => y.ProfileImages, y => y.Reports, y => y.SavedContents, y => y.Surveys, y => y.SurveyAnswers, y => y.SurveyResponses, y => y.UserProfileImages, y => y.UserSessions, y => y.MyFollowers, y => y.MyFollowings);
                return data.OrderBy(i => i.Country);
            }
            catch (Exception)
            {
                return Enumerable.Empty<AppUser>().AsQueryable();
            }
        }
        public async Task<AppUser?> GetBySlugAsync(string slug)
        {
            var match = await _userRepository.GetBySlugAsync(slug);
            if (match == null)
            {
                return null;
            }
            return await GetByIdAsync(match.Id);
        }
        public async Task<AppUser> GetByIdAsync(string appUserId)
        {
            try
            {
                if (appUserId == null)
                    throw new ArgumentNullException(nameof(appUserId), "appUserId was null");

                return await _userRepository.GetIncludeAsync(i => i.Id == appUserId, y => y.Blogs, y => y.CancelMemberships, y => y.Comments, y => y.CommentAnswers, y => y.Companies, y => y.Hits, y => y.Investors, y => y.Likes, y => y.Posts, y => y.ProfileImages, y => y.Reports, y => y.SavedContents, y => y.Surveys, y => y.SurveyAnswers, y => y.SurveyResponses, y => y.UserProfileImages, y => y.UserSessions, y => y.MyFollowers, y => y.MyFollowings);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<AppUser> GetCompanyForProfileByUserId(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                return await _userRepository.GetAsync(i => i.Id == userId);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<AppUser> GetInvestorForProfileByUserId(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                return await _userRepository.GetAsync(i => i.Id == userId);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public AppUser GetUserById(string appUserId)
        {
            try
            {
                if (appUserId == null)
                    throw new ArgumentNullException(nameof(appUserId), "appUserId was null");

                return _userRepository.Get(i => i.Id == appUserId);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetFollowableAsync(string id)
        {
            var result = await _userRepository.SetFollowableAsync(id);
            return result;
        }

        public async Task<bool> SetNotFollowableAsync(string id)
        {
            var result = await _userRepository.SetNotFollowableAsync(id);
            return result;
        }

        public async Task<bool> SetActiveAsync(string id)
        {
            var result = await _userRepository.SetActiveAsync(id);
            return result;
        }

        public async Task<bool> SetActiveLoginConfirmCodeAsync(string id)
        {
            var result = await _userRepository.SetActiveLoginConfirmCodeAsync(id);
            return result;
        }

        public async Task<bool> SetActiveRegisterConfirmCodeAsync(string id)
        {
            var result = await _userRepository.SetActiveRegisterConfirmCodeAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(string id)
        {
            var result = await _userRepository.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveLoginConfirmCodeAsync(string id)
        {
            var result = await _userRepository.SetDeActiveLoginConfirmCodeAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveRegisterConfirmCodeAsync(string id)
        {
            var result = await _userRepository.SetDeActiveRegisterConfirmCodeAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(string id)
        {
            var result = await _userRepository.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(string id)
        {
            var result = await _userRepository.SetNotDeletedAsync(id);
            return result;
        }

        public int UserCounter()
        {
            return _userRepository.UserCounter();
        }

        public async Task<AppUser?> GetCurrentUserAsync()
        {
            return await _userRepository.GetCurrentUserAsync();
        }

        public Guid? GetCurrentUserId()
        {
            return _userRepository.GetCurrentUserId();
        }

        public async Task<AppUser> GetUserProfileByIdAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                return await _userRepository.GetIncludeAsync(i => i.Id == userId, y => y.Blogs, y => y.Comments, y => y.CommentAnswers, y => y.Companies, y => y.Hits, y => y.Investors, y => y.Likes, y => y.Posts, y => y.SavedContents, y => y.Surveys, y => y.MyFollowers, y => y.MyFollowings);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public IQueryable<AppUser> GetAllIncludingEntrepreneursAsync()
        {
            try
            {
                var data = _userRepository.GetAllInclude(new Expression<Func<AppUser, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsInvestor==true,
                    i=>i.Investors.Count()==0,
                    i=>i.Companies.Count()==0
                }, null, y => y.Comments, y => y.CommentAnswers, y => y.Hits, y => y.Investors, y => y.Companies, y => y.Likes, y => y.SavedContents, y => y.UserProfileImages, y => y.MyFollowers, y => y.MyFollowings);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<AppUser>().AsQueryable();
            }
        }

        public async Task<IEnumerable<AppUser>> GetAllIncludingUnPopularEntrepreneursAsync()
        {
            var data = await _userRepository.GetAllIncludingUnPopularEntrepreneursAsync();
            return data;
        }

        public IQueryable<AppUser> GetAllIncludingEntrepreneurTodayAsync()
        {
            try
            {
                var today = DateTime.Now.Date;
                var data = _userRepository.GetAllInclude(new Expression<Func<AppUser, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i => i.CreatedDate >= today && i.CreatedDate < today.AddDays(1)
                }, null, y => y.Comments, y => y.CommentAnswers, y => y.Hits, y => y.Likes, y => y.SavedContents);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<AppUser>().AsQueryable();
            }
        }

        public IQueryable<AppUser> GetAllIncludingSearchResult(string key)
        {
            try
            {
                if (key == null)
                    throw new ArgumentNullException(nameof(key), "key was null");

                var data = _userRepository.GetAllInclude(new Expression<Func<AppUser, bool>>[]
                {
                    i => i.IsActive == true,
                    i => i.IsDeleted == false
                }, null, y => y.Hits, y => y.Likes, y => y.SavedContents, y => y.Companies, y => y.Investors);

                var filtered = data.Where(i => (i.NameSurname.Contains(key, StringComparison.OrdinalIgnoreCase) || i.UserName.Contains(key, StringComparison.OrdinalIgnoreCase)) && (i.IsCompany == true || i.IsInvestor == true));
                return filtered.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<AppUser>().AsQueryable();
            }
        }

        public async Task<IEnumerable<AppUser>> GetAllForSignalRAsync()
        {
            try
            {
                var data = await _userRepository.GetAllIncludeAsync(new Expression<Func<AppUser, bool>>[]
                {

                }, null, y => y.Blogs, y => y.CancelMemberships, y => y.Comments, y => y.CommentAnswers, y => y.Companies, y => y.Hits, y => y.Investors, y => y.Likes, y => y.Posts, y => y.ProfileImages, y => y.Reports, y => y.SavedContents, y => y.Surveys, y => y.SurveyAnswers, y => y.SurveyResponses, y => y.UserProfileImages, y => y.UserSessions, y => y.MyFollowers, y => y.MyFollowings);
                return data.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<AppUser>();
            }
        }
    }
}
