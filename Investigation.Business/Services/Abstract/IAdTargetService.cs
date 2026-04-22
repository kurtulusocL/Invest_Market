using Investigation.Domain.Entities;

namespace Investigation.Business.Services.Abstract
{
    public interface IAdTargetService
    {
        IQueryable<AdTarget> GetAllIncludingAdTargetAsync();
        IQueryable<AdTarget> GetAllIncludingAdTargetByAgeAsync();
        IQueryable<AdTarget> GetAllIncludingAdTargetByMinAgeAsync();
        IQueryable<AdTarget> GetAllIncludingAdTargetByMaxAgeAsync();
        IQueryable<AdTarget> GetAllIncludingAdTargetByMinTotalViewCounAsync();
        IQueryable<AdTarget> GetAllIncludingAdTargetByMinTotalSaveCounAsync();
        IQueryable<AdTarget> GetAllIncludingAdTargetByMinTotalLikeCounAsync();
        IQueryable<AdTarget> GetAllIncludingAdTargetByMinInteractionCounAsync();
        IQueryable<AdTarget> GetAllIncludingAdTargetByAdIdAsync(int adId);
        IQueryable<AdTarget> GetAllIncludingAdTargetForAdminAsync();
        Task<IEnumerable<AdTarget>> GetAllForSignalRAsync();
        Task<AdTarget> GetByIdAsync(int? id);
        Task<bool> CreateAsync(int? minAge, int? maxAge, string targetCountries, string? targetCategoryType, List<int>? targetCategoryIds, int minInteractionCount, int minTotalLikeCount, int minTotalSaveCount, int minTotalViewCount, bool includeBlogInteractions, bool includeInvestorInteractions, bool includeCompanyInteractions, bool includePostInteractions, int adId);
        Task<bool> UpdateAsync(int? minAge, int? maxAge, string targetCountries, string? targetCategoryType, List<int>? targetCategoryIds, int minInteractionCount, int minTotalLikeCount, int minTotalSaveCount, int minTotalViewCount, bool includeBlogInteractions, bool includeInvestorInteractions, bool includeCompanyInteractions, bool includePostInteractions, int adId, int id);
        Task<bool> DeleteAsync(AdTarget entity, int id);
        Task<bool> DeleteAllByIdAsync(List<int> ids);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
    }
}
