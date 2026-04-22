using System.Linq.Expressions;
using System.Security.Claims;
using Investigation.Business.Constants.Services;
using Investigation.Business.Services.Abstract;
using Investigation.DataAccess.Abstract;
using Investigation.Domain.Entities;
using Investigation.Shared.Dtos.FollowDtos;
using Microsoft.AspNetCore.Http;

namespace Investigation.Business.Services.Concrete
{
    public class FollowManager : IFollowService
    {
        readonly IFollowRepository _followRepository;
        readonly IHttpContextAccessor _httpContextAccessor;
        readonly EncryptionService _encryptionService;
        public FollowManager(IFollowRepository followRepository, IHttpContextAccessor httpContextAccessor, EncryptionService encryptionService)
        {
            _followRepository = followRepository;
            _httpContextAccessor = httpContextAccessor;
            _encryptionService = encryptionService;
        }

        public async Task<bool> CancelFollowerAsync(string? targetFollowerUserId, int? targetFollowerCompanyId)
        {
            try
            {
                var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("UserId")?.Value
                           ?? _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var appUserId = userIdClaim ?? _httpContextAccessor.HttpContext?.Session.GetString("userId");

                if (string.IsNullOrEmpty(appUserId))
                    throw new UnauthorizedAccessException("Oturum bulunamadı.");

                var sessionCompanyIdStr = _httpContextAccessor.HttpContext?.Session.GetString("companyId");

                string? myUserId = null;
                int? myCompanyId = null;

                if (!string.IsNullOrEmpty(sessionCompanyIdStr))
                    myCompanyId = int.Parse(sessionCompanyIdStr);
                else
                    myUserId = appUserId;

                var existingFollow = await _followRepository.GetAsync(x =>
                    ((myCompanyId != null && x.FollowedCompanyId == myCompanyId) || (myUserId != null && x.FollowedUserId == myUserId)) &&
                    ((targetFollowerUserId != null && x.FollowerUserId == targetFollowerUserId) || (targetFollowerCompanyId != null && x.FollowerCompanyId == targetFollowerCompanyId)));

                if (existingFollow != null)
                {
                    if (existingFollow.IsCanceled) return true;

                    existingFollow.IsFollowed = false;
                    existingFollow.IsCanceled = true;
                    existingFollow.CanceledFollowDate = DateTime.Now;
                    existingFollow.UnfollowDate = DateTime.Now;

                    return await _followRepository.UpdateAsync(existingFollow);
                }

                var canceledRecord = new Follow
                {
                    FollowerUserId = targetFollowerUserId,
                    FollowerCompanyId = targetFollowerCompanyId,
                    FollowedUserId = myUserId,
                    FollowedCompanyId = myCompanyId,
                    IsFollowed = false,
                    IsCanceled = true,
                    CanceledFollowDate = DateTime.Now
                };

                var result = await _followRepository.AddAsync(canceledRecord);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while cancelling to follower.", ex);
            }
        }

        public async Task<bool> FollowAsync(bool isFollowed, string? followedUserId, int? followedCompanyId)
        {
            try
            {
                var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("UserId")?.Value
                ?? _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var sessionUserId = _httpContextAccessor.HttpContext?.Session.GetString("userId");

                var appUserId = userIdClaim ?? sessionUserId;

                if (string.IsNullOrEmpty(appUserId))
                    throw new UnauthorizedAccessException("Not Found");

                string? followerUserId = appUserId;
                int? followerCompanyId = null;

                var sessionCompanyIdStr = _httpContextAccessor.HttpContext?.Session.GetString("companyId");
                if (!string.IsNullOrEmpty(sessionCompanyIdStr))
                {
                    followerCompanyId = int.Parse(sessionCompanyIdStr);
                }

                var existingFollow = await _followRepository.GetAsync(x =>
                    (x.FollowerUserId == followerUserId || (followerCompanyId != null && x.FollowerCompanyId == followerCompanyId)) &&
                    ((followedUserId != null && x.FollowedUserId == followedUserId) || (followedCompanyId != null && x.FollowedCompanyId == followedCompanyId)));

                if (existingFollow != null)
                {
                    if (existingFollow.IsCanceled) return false;
                    if (existingFollow.IsFollowed == isFollowed) return true;

                    existingFollow.IsFollowed = isFollowed;
                    if (isFollowed)
                    {
                        existingFollow.FollowDate = DateTime.Now;
                        existingFollow.UnfollowDate = null;
                    }
                    else
                    {
                        existingFollow.UnfollowDate = DateTime.Now;
                    }
                    return await _followRepository.UpdateAsync(existingFollow);
                }
                if (isFollowed)
                {
                    var newFollow = new Follow
                    {
                        FollowerUserId = followerUserId,
                        FollowerCompanyId = followerCompanyId,
                        FollowedUserId = followedUserId,
                        FollowedCompanyId = followedCompanyId,
                        IsFollowed = true,
                        IsCanceled = false,
                        FollowDate = DateTime.Now
                    };
                    var result = await _followRepository.AddAsync(newFollow);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while following.", ex);
            }
        }

        public async Task<bool> UnfollowAsync(bool isFollowed, string? followedUserId, int? followedCompanyId)
        {
            try
            {
                var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("UserId")?.Value
                   ?? _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var sessionUserId = _httpContextAccessor.HttpContext?.Session.GetString("userId");
                var appUserId = userIdClaim ?? sessionUserId;

                if (string.IsNullOrEmpty(appUserId))
                    throw new UnauthorizedAccessException("Oturum bulunamadı.");

                var sessionCompanyIdStr = _httpContextAccessor.HttpContext?.Session.GetString("companyId");

                string? followerUserId = null;
                int? followerCompanyId = null;

                if (!string.IsNullOrEmpty(sessionCompanyIdStr))
                {
                    followerCompanyId = int.Parse(sessionCompanyIdStr);
                }
                else
                {
                    followerUserId = appUserId;
                }

                var existingFollow = await _followRepository.GetAsync(x =>
                    ((followerCompanyId != null && x.FollowerCompanyId == followerCompanyId) || (followerUserId != null && x.FollowerUserId == followerUserId)) &&
                    ((followedCompanyId != null && x.FollowedCompanyId == followedCompanyId) || (followedUserId != null && x.FollowedUserId == followedUserId)));

                if (existingFollow != null)
                {
                    if (!existingFollow.IsFollowed)
                    {
                        return true;
                    }
                    else
                    {
                        existingFollow.IsFollowed = false;
                        existingFollow.UnfollowDate = DateTime.UtcNow;
                        existingFollow.UpdatedDate = DateTime.UtcNow;
                    }
                    var result = await _followRepository.UpdateAsync(existingFollow);
                    return result;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while unfollowing.", ex);
            }
        }

        public async Task<FollowStatusDto> GetFollowStatusAsync(string? targetUserId, int? targetCompanyId)
        {
            try
            {
                var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("UserId")?.Value
                ?? _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var sessionUserId = _httpContextAccessor.HttpContext?.Session.GetString("userId");
                var appUserId = userIdClaim ?? sessionUserId;

                var sessionCompanyIdStr = _httpContextAccessor.HttpContext?.Session.GetString("companyId");
                int? followerCompanyId = !string.IsNullOrEmpty(sessionCompanyIdStr) ? int.Parse(sessionCompanyIdStr) : null;
                string? followerUserId = (followerCompanyId == null) ? appUserId : null;

                var isFollowing = await _followRepository.AnyAsync(x =>
                    x.IsFollowed && !x.IsCanceled &&
                    (
                        ((followerCompanyId != null && x.FollowerCompanyId == followerCompanyId) || (followerUserId != null && x.FollowerUserId == followerUserId))
                        &&
                        ((targetCompanyId != null && x.FollowedCompanyId == targetCompanyId) || (targetUserId != null && x.FollowedUserId == targetUserId))
                    ));

                return new FollowStatusDto
                {
                    TargetUserId = targetUserId,
                    TargetCompanyId = targetCompanyId,
                    IsFollowing = isFollowing,
                    IsFollowable = (appUserId != targetUserId)
                };
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while following status.", ex);
            }
        }

        public async Task<bool> DeleteAllByIdAsync(List<int> ids)
        {
            try
            {
                if (ids == null || !ids.Any())
                    throw new ArgumentNullException(nameof(ids), "id list was null or empty");

                var result = await _followRepository.DeleteByIdsAsync(ids.Cast<object>());
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while bulk deleting entities.", ex);
            }
        }

        public async Task<bool> DeleteAsync(Follow entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var data = await _followRepository.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _followRepository.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public IQueryable<Follow> GetAllIncluding()
        {
            try
            {
                var data = _followRepository.GetAllInclude(new Expression<Func<Follow, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                }, null, y => y.FollowerUser, y => y.FollowedUser, y => y.FollowedCompany, y => y.FollowerCompany);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Follow>().AsQueryable();
            }
        }

        public IQueryable<Follow> GetAllIncludingCancelledOnFollow()
        {
            try
            {
                var data = _followRepository.GetAllInclude(new Expression<Func<Follow, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsCanceled==true
                }, null, y => y.FollowerUser, y => y.FollowedUser, y => y.FollowedCompany, y => y.FollowerCompany);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Follow>().AsQueryable();
            }
        }

        public IQueryable<Follow> GetAllIncludingFollowsByCompanyId(int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                var data = _followRepository.GetAllInclude(new Expression<Func<Follow, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsFollowed==true,
                    i=>i.FollowerCompanyId==companyId||i.FollowedCompanyId==companyId
                }, y => y.FollowerUser, y => y.FollowedUser, y => y.FollowedCompany, y => y.FollowerCompany);
                return data.OrderByDescending(i => i.FollowDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Follow>().AsQueryable();
            }
        }

        public IQueryable<Follow> GetAllIncludingFollowsByUserId(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var data = _followRepository.GetAllInclude(new Expression<Func<Follow, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsFollowed==true,
                    i=>i.FollowerUserId==userId||i.FollowedUserId==userId
                }, y => y.FollowerUser, y => y.FollowedUser, y => y.FollowedCompany, y => y.FollowerCompany);
                return data.OrderByDescending(i => i.FollowDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Follow>().AsQueryable();
            }
        }

        public IQueryable<Follow> GetAllIncludingUnFollows()
        {
            try
            {
                var data = _followRepository.GetAllInclude(new Expression<Func<Follow, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsFollowed==false
                }, null, y => y.FollowerUser, y => y.FollowedUser, y => y.FollowedCompany, y => y.FollowerCompany);
                return data.OrderByDescending(i => i.FollowDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Follow>().AsQueryable();
            }
        }

        public IQueryable<Follow> GetAllIncludingCurrentlyFollowing()
        {
            try
            {
                var data = _followRepository.GetAllInclude(new Expression<Func<Follow, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsFollowed==true
                }, null, y => y.FollowerUser, y => y.FollowedUser, y => y.FollowedCompany, y => y.FollowerCompany);
                return data.OrderByDescending(i => i.FollowDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Follow>().AsQueryable();
            }
        }
        public IQueryable<Follow> GetAllIncludingFolloweds()
        {
            try
            {
                var data = _followRepository.GetAllInclude(new Expression<Func<Follow, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsFollowed==true
                }, null, y => y.FollowerUser, y => y.FollowedUser, y => y.FollowedCompany, y => y.FollowerCompany);
                return data.OrderByDescending(i => i.FollowDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Follow>().AsQueryable();
            }
        }

        public IQueryable<Follow> GetAllIncludingFollowedsByUserId(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var data = _followRepository.GetAllIncludeById(userId, "FollowedUserId", new Expression<Func<Follow, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsFollowed==true
                }, y => y.FollowerUser, y => y.FollowedUser, y => y.FollowedCompany, y => y.FollowerCompany);
                return data.OrderByDescending(i => i.FollowDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Follow>().AsQueryable();
            }
        }

        public IQueryable<Follow> GetAllIncludingFollowedsByCompanyId(int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                var data = _followRepository.GetAllIncludeById(companyId, "FollowedCompanyId", new Expression<Func<Follow, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsFollowed==true
                }, y => y.FollowerUser, y => y.FollowedUser, y => y.FollowedCompany, y => y.FollowerCompany);
                return data.OrderByDescending(i => i.FollowDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Follow>().AsQueryable();
            }
        }
        public IQueryable<Follow> GetAllIncludingCurrentlyUnFollowingByUserId(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var data = _followRepository.GetAllInclude(new Expression<Func<Follow, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsFollowed==false,
                    i=>i.FollowerUserId==userId||i.FollowedUserId==userId
                }, y => y.FollowerUser, y => y.FollowedUser, y => y.FollowedCompany, y => y.FollowerCompany);
                return data.OrderByDescending(i => i.FollowDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Follow>().AsQueryable();
            }
        }

        public IQueryable<Follow> GetAllIncludingUnFollowsByCompanyId(int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                var data = _followRepository.GetAllInclude(new Expression<Func<Follow, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsFollowed==false,
                    i=>i.FollowerCompanyId==companyId||i.FollowedCompanyId==companyId
                }, y => y.FollowerUser, y => y.FollowedUser, y => y.FollowedCompany, y => y.FollowerCompany);
                return data.OrderByDescending(i => i.FollowDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Follow>().AsQueryable();
            }
        }

        public IQueryable<Follow> GetAllIncludingFollowedsForCompanyByCompanyId(int? companyId, int skip = 0, int take = 50)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                var data = _followRepository.GetAllInclude(new Expression<Func<Follow, bool>>[]
                {
                    i => i.IsActive == true,
                    i => i.IsDeleted == false,
                    i => i.IsFollowed == true,
                    i => i.FollowerCompanyId == companyId
                }, y => y.FollowedUser, y => y.FollowedUser.Investors, y => y.FollowedCompany, y => y.FollowedCompany.Country, y => y.FollowerUser, y => y.FollowerCompany);
                return data.OrderByDescending(i => i.FollowDate).Skip(skip).Take(take);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Follow>().AsQueryable();
            }
        }

        public IQueryable<Follow> GetAllIncludingFollowersForCompanyByCompanyId(int? companyId, int skip = 0, int take = 50)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                var data = _followRepository.GetAllInclude(new Expression<Func<Follow, bool>>[]
                {
                    i => i.IsActive == true,
                    i => i.IsDeleted == false,
                    i => i.IsFollowed == true,
                    i => i.FollowedCompanyId == companyId
                }, y => y.FollowerUser, y => y.FollowerUser.Investors, y => y.FollowerCompany, y => y.FollowerCompany.Country);
                return data.OrderByDescending(i => i.FollowDate).Skip(skip).Take(take);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Follow>().AsQueryable();
            }
        }

        public IQueryable<Follow> GetAllIncludingFollowedsForUserByUserId(string userId, int skip = 0, int take = 50)
        {
            try
            {
                if (string.IsNullOrEmpty(userId))
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var data = _followRepository.GetAllInclude(new Expression<Func<Follow, bool>>[]
                {
                    i => i.IsActive == true,
                    i => i.IsDeleted == false,
                    i => i.IsFollowed == true,
                    i => i.FollowerUserId == userId
                }, y => y.FollowedUser, y => y.FollowedUser.Investors, y => y.FollowedCompany, y => y.FollowedCompany.Country);
                return data.OrderByDescending(i => i.FollowDate).Skip(skip).Take(take);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Follow>().AsQueryable();
            }
        }

        public IQueryable<Follow> GetAllIncludingFollowersForUserByUserId(string userId, int skip = 0, int take = 50)
        {
            try
            {
                if (string.IsNullOrEmpty(userId))
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var data = _followRepository.GetAllInclude(new Expression<Func<Follow, bool>>[]
                {
                    i => i.IsActive == true,
                    i => i.IsDeleted == false,
                    i => i.IsFollowed == true,
                    i => i.FollowedUserId == userId
                }, y => y.FollowerUser, y => y.FollowerUser.Investors, y => y.FollowerCompany, y => y.FollowerCompany.Country);
                return data.OrderByDescending(i => i.FollowDate).Skip(skip).Take(take);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Follow>().AsQueryable();
            }
        }

        public IQueryable<Follow> GetAllIncludingForAdmin()
        {
            try
            {
                var data = _followRepository.GetAllInclude(new Expression<Func<Follow, bool>>[]
                {

                }, null, y => y.FollowerUser, y => y.FollowedUser, y => y.FollowedCompany, y => y.FollowerCompany);
                return data.OrderByDescending(i => i.FollowDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Follow>().AsQueryable();
            }
        }

        public async Task<Follow> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                return await _followRepository.GetIncludeAsync(i => i.Id == id, y => y.FollowerUser, y => y.FollowedUser, y => y.FollowedCompany, y => y.FollowerCompany);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _followRepository.SetActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _followRepository.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _followRepository.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _followRepository.SetNotDeletedAsync(id);
            return result;
        }

        public IQueryable<Follow> GetAllIncludingFollowedsSearchResultForUserByUserId(string userId, int skip, int take, string searchTerm)
        {
            try
            {
                if (string.IsNullOrEmpty(userId))
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var sqlFilters = new Expression<Func<Follow, bool>>[]
                {
                    i => i.IsActive == true,
                    i => i.IsDeleted == false,
                    i => i.IsFollowed == true,
                    i => i.FollowerUserId == userId
                };

                var data = _followRepository.GetAllInclude(sqlFilters,
                    y => y.FollowedUser,
                    y => y.FollowedUser.Investors,
                    y => y.FollowedCompany,
                    y => y.FollowerUser);
                var resultList = data.AsEnumerable();

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    searchTerm = searchTerm.ToLower().Trim();
                    resultList = resultList.Where(i =>
                        (i.FollowedUser != null && _encryptionService.Decrypt(i.FollowedUser.NameSurname).ToLower().Contains(searchTerm)) ||
                        (i.FollowedCompany != null && i.FollowedCompany.Name.ToLower().Contains(searchTerm))
                    );
                }
                return resultList.OrderByDescending(i => i.FollowDate).Skip(skip).Take(take).AsQueryable();
            }
            catch (Exception)
            {
                return Enumerable.Empty<Follow>().AsQueryable();
            }
        }

        public IQueryable<Follow> GetAllIncludingFollowersSearchResultForUserByUserId(string userId, int skip, int take, string searchTerm)
        {
            try
            {
                if (string.IsNullOrEmpty(userId))
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var sqlFilters = new Expression<Func<Follow, bool>>[]
                {
                    i => i.IsActive == true,
                    i => i.IsDeleted == false,
                    i => i.IsFollowed == true,
                    i => i.FollowedUserId == userId
                };

                var data = _followRepository.GetAllInclude(sqlFilters,
                    y => y.FollowedUser,
                    y => y.FollowedUser.Investors,
                    y => y.FollowedCompany,
                    y => y.FollowedCompany.Country,
                    y => y.FollowerUser,
                    y => y.FollowerUser.Investors,
                    y => y.FollowerCompany);
                var resultList = data.AsEnumerable();

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    searchTerm = searchTerm.ToLower().Trim();
                    resultList = resultList.Where(i =>
                        (i.FollowerUser != null && _encryptionService.Decrypt(i.FollowerUser.NameSurname).ToLower().Contains(searchTerm)) ||
                        (i.FollowerCompany != null && i.FollowerCompany.Name.ToLower().Contains(searchTerm))
                    );
                }
                return resultList.OrderByDescending(i => i.FollowDate).Skip(skip).Take(take).AsQueryable();
            }
            catch (Exception)
            {
                return Enumerable.Empty<Follow>().AsQueryable();
            }
        }

        public IQueryable<Follow> GetAllIncludingFollowedCompaniesSearchResultByCompanyId(int companyId, int skip, int take, string searchTerm)
        {
            try
            {
                var sqlFilters = new Expression<Func<Follow, bool>>[]
                 {
                    i => i.IsActive == true,
                    i => i.IsDeleted == false,
                    i => i.IsFollowed == true,
                    i => i.FollowerCompanyId == companyId
                 };

                var data = _followRepository.GetAllInclude(sqlFilters,
                    y => y.FollowedUser,
                    y => y.FollowedUser.Investors,
                    y => y.FollowedCompany,
                    y => y.FollowedCompany.Country,
                    y => y.FollowerCompany);
                var resultList = data.AsEnumerable();

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    searchTerm = searchTerm.ToLower().Trim();
                    resultList = resultList.Where(i =>
                        (i.FollowedUser != null && _encryptionService.Decrypt(i.FollowedUser.NameSurname).ToLower().Contains(searchTerm)) ||
                        (i.FollowedCompany != null && i.FollowedCompany.Name.ToLower().Contains(searchTerm))
                    );
                }
                return resultList.OrderByDescending(i => i.FollowDate).Skip(skip).Take(take).AsQueryable();
            }
            catch (Exception)
            {
                return Enumerable.Empty<Follow>().AsQueryable();
            }
        }

        public IQueryable<Follow> GetAllIncludingFollowerCompaniesSearchResultByCompanyId(int companyId, int skip, int take, string searchTerm)
        {
            try
            {
                var sqlFilters = new Expression<Func<Follow, bool>>[]
                {
                    i => i.IsActive == true,
                    i => i.IsDeleted == false,
                    i => i.IsFollowed == true,
                    i => i.FollowedCompanyId == companyId
                };

                var data = _followRepository.GetAllInclude(sqlFilters,
                    y => y.FollowedCompany,
                    y => y.FollowerUser,
                    y => y.FollowerUser.Investors,
                    y => y.FollowerCompany,
                    y => y.FollowerCompany.Country);
                var resultList = data.AsEnumerable();

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    searchTerm = searchTerm.ToLower().Trim();                   
                    resultList = resultList.Where(i =>
                        (i.FollowerUser != null && _encryptionService.Decrypt(i.FollowerUser.NameSurname).ToLower().Contains(searchTerm)) ||
                        (i.FollowerCompany != null && i.FollowerCompany.Name.ToLower().Contains(searchTerm))
                    );
                }
                return resultList.OrderByDescending(i => i.FollowDate).Skip(skip).Take(take).AsQueryable();
            }
            catch (Exception)
            {
                return Enumerable.Empty<Follow>().AsQueryable();
            }
        }
    }
}