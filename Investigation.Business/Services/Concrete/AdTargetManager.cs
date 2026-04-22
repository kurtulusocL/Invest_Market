using System.Linq.Expressions;
using Investigation.Business.Services.Abstract;
using Investigation.DataAccess.Abstract;
using Investigation.Domain.Entities;

namespace Investigation.Business.Services.Concrete
{
    public class AdTargetManager : IAdTargetService
    {
        readonly IAdTargetRepository _adTargetRepository;
        public AdTargetManager(IAdTargetRepository adTargetRepository)
        {
            _adTargetRepository = adTargetRepository;
        }

        public async Task<bool> CreateAsync(int? minAge, int? maxAge, string targetCountries, string? targetCategoryType, List<int>? targetCategoryIds, int minInteractionCount, int minTotalLikeCount, int minTotalSaveCount, int minTotalViewCount, bool includeBlogInteractions, bool includeInvestorInteractions, bool includeCompanyInteractions, bool includePostInteractions, int adId)
        {
            try
            {
                var entity = new AdTarget
                {
                    MinAge = minAge,
                    MaxAge = maxAge,
                    TargetCountries = targetCountries,
                    TargetCategoryType = targetCategoryType,
                    TargetCategoryIds = targetCategoryIds,
                    MinInteractionCount = minInteractionCount,
                    MinTotalLikeCount = minTotalLikeCount,
                    MinTotalSaveCount = minTotalSaveCount,
                    MinTotalViewCount = minTotalViewCount,
                    IncludeBlogInteractions = includeBlogInteractions,
                    IncludeInvestorInteractions = includeInvestorInteractions,
                    IncludeCompanyInteractions = includeCompanyInteractions,
                    IncludePostInteractions = includePostInteractions,
                    AdId = adId
                };
                if (entity != null)
                {
                    var result = await _adTargetRepository.AddAsync(entity);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> DeleteAllByIdAsync(List<int> ids)
        {
            try
            {
                if (ids == null || !ids.Any())
                    throw new ArgumentNullException(nameof(ids), "id list was null or empty");

                var result = await _adTargetRepository.DeleteByIdsAsync(ids.Cast<object>());
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while bulk deleting entities.", ex);
            }
        }

        public async Task<bool> DeleteAsync(AdTarget entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var data = await _adTargetRepository.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _adTargetRepository.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public async Task<IEnumerable<AdTarget>> GetAllForSignalRAsync()
        {
            try
            {
                var data = await _adTargetRepository.GetAllIncludeAsync(new Expression<Func<AdTarget, bool>>[]
                {

                }, null, y => y.Ad);
                return data.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<AdTarget>();
            }
        }

        public IQueryable<AdTarget> GetAllIncludingAdTargetAsync()
        {
            try
            {
                var data = _adTargetRepository.GetAllInclude(new Expression<Func<AdTarget, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Ad);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<AdTarget>().AsQueryable();
            }
        }

        public IQueryable<AdTarget> GetAllIncludingAdTargetByAdIdAsync(int adId)
        {
            try
            {
                var data = _adTargetRepository.GetAllIncludeById(adId, "AdId", new Expression<Func<AdTarget, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Ad);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<AdTarget>().AsQueryable();
            }
        }

        public IQueryable<AdTarget> GetAllIncludingAdTargetByAgeAsync()
        {
            try
            {
                var data = _adTargetRepository.GetAllInclude(new Expression<Func<AdTarget, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.MinAge!=null||i.MaxAge!=null
                }, null, y => y.Ad);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<AdTarget>().AsQueryable();
            }
        }

        public IQueryable<AdTarget> GetAllIncludingAdTargetByMaxAgeAsync()
        {
            try
            {
                var data = _adTargetRepository.GetAllInclude(new Expression<Func<AdTarget, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.MaxAge!=null
                }, null, y => y.Ad);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<AdTarget>().AsQueryable();
            }
        }

        public IQueryable<AdTarget> GetAllIncludingAdTargetByMinAgeAsync()
        {
            try
            {
                var data = _adTargetRepository.GetAllInclude(new Expression<Func<AdTarget, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.MinAge!=null
                }, null, y => y.Ad);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<AdTarget>().AsQueryable();
            }
        }

        public IQueryable<AdTarget> GetAllIncludingAdTargetByMinInteractionCounAsync()
        {
            try
            {
                var data = _adTargetRepository.GetAllInclude(new Expression<Func<AdTarget, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Ad);
                return data.OrderBy(i => i.MinInteractionCount);
            }
            catch (Exception)
            {
                return Enumerable.Empty<AdTarget>().AsQueryable();
            }
        }

        public IQueryable<AdTarget> GetAllIncludingAdTargetByMinTotalLikeCounAsync()
        {
            try
            {
                var data = _adTargetRepository.GetAllInclude(new Expression<Func<AdTarget, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.MinTotalLikeCount!=null
                }, null, y => y.Ad);
                return data.OrderByDescending(i => i.MinTotalLikeCount);
            }
            catch (Exception)
            {
                return Enumerable.Empty<AdTarget>().AsQueryable();
            }
        }

        public IQueryable<AdTarget> GetAllIncludingAdTargetByMinTotalSaveCounAsync()
        {
            try
            {
                var data = _adTargetRepository.GetAllInclude(new Expression<Func<AdTarget, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.MinTotalSaveCount!=null
                }, null, y => y.Ad);
                return data.OrderByDescending(i => i.MinTotalSaveCount);
            }
            catch (Exception)
            {
                return Enumerable.Empty<AdTarget>().AsQueryable();
            }
        }

        public IQueryable<AdTarget> GetAllIncludingAdTargetByMinTotalViewCounAsync()
        {
            try
            {
                var data = _adTargetRepository.GetAllInclude(new Expression<Func<AdTarget, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.MinTotalViewCount!=null
                }, null, y => y.Ad);
                return data.OrderByDescending(i => i.MinTotalViewCount);
            }
            catch (Exception)
            {
                return Enumerable.Empty<AdTarget>().AsQueryable();
            }
        }

        public IQueryable<AdTarget> GetAllIncludingAdTargetForAdminAsync()
        {
            try
            {
                var data = _adTargetRepository.GetAllInclude(new Expression<Func<AdTarget, bool>>[]
                {

                }, null, y => y.Ad);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<AdTarget>().AsQueryable();
            }
        }

        public async Task<AdTarget> GetByIdAsync(int? id)
        {
            try
            {
                return await _adTargetRepository.GetIncludeAsync(i => i.Id == id, y => y.Ad);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _adTargetRepository.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _adTargetRepository.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _adTargetRepository.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _adTargetRepository.SetNotDeletedAsync(id);
            return result;
        }

        public async Task<bool> UpdateAsync(int? minAge, int? maxAge, string targetCountries, string? targetCategoryType, List<int>? targetCategoryIds, int minInteractionCount, int minTotalLikeCount, int minTotalSaveCount, int minTotalViewCount, bool includeBlogInteractions, bool includeInvestorInteractions, bool includeCompanyInteractions, bool includePostInteractions, int adId, int id)
        {
            try
            {
                var entity = new AdTarget
                {
                    MinAge = minAge,
                    MaxAge = maxAge,
                    TargetCountries = targetCountries,
                    TargetCategoryType = targetCategoryType,
                    TargetCategoryIds = targetCategoryIds,
                    MinInteractionCount = minInteractionCount,
                    MinTotalLikeCount = minTotalLikeCount,
                    MinTotalSaveCount = minTotalSaveCount,
                    MinTotalViewCount = minTotalViewCount,
                    IncludeBlogInteractions = includeBlogInteractions,
                    IncludeInvestorInteractions = includeInvestorInteractions,
                    IncludeCompanyInteractions = includeCompanyInteractions,
                    IncludePostInteractions = includePostInteractions,
                    AdId = adId,
                    Id = id,
                    UpdatedDate = DateTime.UtcNow
                };
                if (entity != null)
                {
                    var result = await _adTargetRepository.UpdateAsync(entity);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while Updating the entity.", ex);
            }
        }
    }
}
