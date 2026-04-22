using System.Linq.Expressions;
using Investigation.Business.Services.Abstract;
using Investigation.DataAccess.Abstract;
using Investigation.Domain.Entities;

namespace Investigation.Business.Services.Concrete
{
    public class UserSessionManager : IUserSessionService
    {
        readonly IUserSessionRepository _userSessionRepository;
        public UserSessionManager(IUserSessionRepository userSessionRepository)
        {
            _userSessionRepository = userSessionRepository;
        }

        public async Task<bool> DeleteAllByIdAsync(List<int> ids)
        {
            try
            {
                if (ids == null || !ids.Any())
                    throw new ArgumentNullException(nameof(ids), "id list was null or empty");

                var result = await _userSessionRepository.DeleteByIdsAsync(ids.Cast<object>());
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while bulk deleting entities.", ex);
            }
        }

        public async Task<bool> DeleteAsync(UserSession entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var data = await _userSessionRepository.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _userSessionRepository.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public async Task<IEnumerable<UserSession>> GetAllForSignalRAsync()
        {
            try
            {
                var data = await _userSessionRepository.GetAllIncludeAsync(new Expression<Func<UserSession, bool>>[]
                {
                    
                }, null, y => y.AppUser);
                return data.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<UserSession>();
            }
        }

        public IQueryable<UserSession> GetAllIncludingAsync()
        {
            try
            {
                var data = _userSessionRepository.GetAllInclude(new Expression<Func<UserSession, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.AppUser);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<UserSession>().AsQueryable();
            }
        }

        public IQueryable<UserSession> GetAllIncludingByCurrentlyOnlineAsync()
        {
            try
            {
                var data = _userSessionRepository.GetAllInclude(new Expression<Func<UserSession, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsOnline==true ||i.LogoutDate==null
                }, null, y => y.AppUser);
                return data.OrderByDescending(i => i.LoginDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<UserSession>().AsQueryable();
            }
        }

        public IQueryable<UserSession> GetAllIncludingByLogoutDateAsync()
        {
            try
            {
                var data = _userSessionRepository.GetAllInclude(new Expression<Func<UserSession, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.LogoutDate!=null
                }, null, y => y.AppUser);
                return data.OrderByDescending(i => i.LogoutDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<UserSession>().AsQueryable();
            }
        }

        public IQueryable<UserSession> GetAllIncludingByUserIdAsync(string appuserId)
        {
            try
            {
                if (appuserId == null)
                    throw new ArgumentNullException(nameof(appuserId), "appuserId was null");

                var data = _userSessionRepository.GetAllIncludeById(appuserId, "AppUserId", new Expression<Func<UserSession, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.AppUser);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<UserSession>().AsQueryable();
            }
        }

        public IQueryable<UserSession> GetAllIncludingForAdminAsync()
        {
            try
            {
                var data = _userSessionRepository.GetAllInclude(new Expression<Func<UserSession, bool>>[]
                {

                }, null, y => y.AppUser);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<UserSession>().AsQueryable();
            }
        }

        public IQueryable<UserSession> GetAllIncludingForAdminHome()
        {
            try
            {
                return _userSessionRepository.GetAllInclude(new Expression<Func<UserSession, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.AppUser).OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<UserSession>().AsQueryable();
            }
        }

        public async Task<UserSession> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                return await _userSessionRepository.GetIncludeAsync(i => i.Id == id, y => y.AppUser);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _userSessionRepository.SetActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _userSessionRepository.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _userSessionRepository.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _userSessionRepository.SetNotDeletedAsync(id);
            return result;
        }
    }
}
