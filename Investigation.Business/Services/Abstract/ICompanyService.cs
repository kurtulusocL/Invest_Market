using Investigation.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Investigation.Business.Services.Abstract
{
    public interface ICompanyService
    {
        IQueryable<Company> GetAllIncludingAsync();
        IQueryable<Company> GetAllIncludingByLookingForInvestAsync();
        IQueryable<Company> GetAllIncludingByFoundationDateAsync();
        IQueryable<Company> GetAllIncludingByCompanyCategoryIdAsync(int companyCategoryId);
        IQueryable<Company> GetAllIncludingByCountryIdAsync(int countryId);
        IQueryable<Company> GetAllIncludingBySubSectorIdAsync(int? subSectorId);
        IQueryable<Company> GetAllIncludingBySectorIdAsync(int sectorId);
        IQueryable<Company> GetAllIncludingByUserIdAsync(string appUserId);
        IQueryable<Company> GetAllIncludingForAdminAsync();
        Task<IEnumerable<Company>> GetAllIncludingMostPopularCompaniesAsync();
        Task<IEnumerable<Company>> GetAllIncludingUnPopularCompaniesAsync();
        IQueryable<Company> GetAllIncludingCompeniesForCompanyHomeByUserIdAsync(string userId);
        Task<IEnumerable<Company>> GetAllIncludingCompaniesForPublicUser();
        IQueryable<Company> GetAllIncludingMostLikedCompaniesAsync();
        IQueryable<Company> GetAllIncludingMostSavedCompaniesAsync();
        IQueryable<Company> GetAllIncludingMostHitCompaniesAsync();
        IQueryable<Company> GetAllIncludingLessLikedCompaniesAsync();
        IQueryable<Company> GetAllIncludingLessSavedCompaniesAsync();
        IQueryable<Company> GetAllIncludingLessHitCompaniesAsync();
        IQueryable<Company> GetAllIncludingCompanyTodayAsync();
        List<SelectListItem> SectorSelectSystem(int? sectorId, string tip);
        IQueryable<Company> GetAllIncludingCompanyFinderSearchResults(string? companyName = null, string? foundationYear = null, bool isLookingForInvest = true, string? hasGithubAccount = null, int? countryId = null, int? companyCategoryId = null, int? sectorId = null);
        Task<IEnumerable<Company>> GetAllForSignalRAsync();
        Task<Company> GetByIdAsync(int? id);
        Task<Company?> GetBySlugAsync(string slug);
        Task<bool> CreateAsync(string name, string slogan, string shortBio, string desc, DateTime foundationDate, bool isLookingForInvest, string linkedIn, string? gitHub, int companyCategoryId, int countryId, int sectorId, int? subSectorId, string appUserId, IFormFile image);
        Task<bool> UpdateAsync(string name, string slogan, string shortBio, string desc, DateTime foundationDate, bool isLookingForInvest, string linkedIn, string? gitHub, int companyCategoryId, int countryId, int sectorId, int? subSectorId, string appUserId, IFormFile image, int id);
        Task<bool> DeleteAsync(Company entity, int id);
        Task<bool> SetLookingForInvestAsync(int id);
        Task<bool> SetNotLookingForInvestAsync(int id);
        Task<bool> SetFollowableAsync(int id);
        Task<bool> SetNotFollowableAsync(int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        IQueryable<Company> GetAllForSitemap();
        IEnumerable<Company> GetAllIncludeLastJoinedCompaniesForAdminHome();
        IEnumerable<Company> GetAllIncludingLastCompanies();
        IEnumerable<Company> GetAllIncludingMostPopularCompanies();
        IEnumerable<Company> GetAllIncludingRandomCompaniesForCompanyDetail();
        IEnumerable<Company> GetAllIncludingCompanyForPublicUser();       
        Company GetCompanyForCommentFormByCompanyId(int? companyId);
        Company GetCompanyIdForCompanyHeader(string userId);
        Company GetCompanyLogoByCompanyUserId(string userId);
        int CompanyCounter();
    }
}