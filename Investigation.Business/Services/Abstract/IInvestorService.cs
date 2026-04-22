using Investigation.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Investigation.Business.Services.Abstract
{
    public interface IInvestorService
    {
        IQueryable<Investor> GetAllIncludingAsync();
        IQueryable<Investor> GetAllIncludingByInvestorDateAsync();
        IQueryable<Investor> GetAllIncludingByLookingForCompanyAsync();
        IQueryable<Investor> GetAllIncludingByMostHitAsync();
        IQueryable<Investor> GetAllIncludingByMostLikedAsync();
        IQueryable<Investor> GetAllIncludingByMostInvestedAsync();
        IQueryable<Investor> GetAllIncludingByUserIdAsync(string appUserId);
        IQueryable<Investor> GetAllIncludingByInvesterCategoryIdAsync(int investorCategoryId);
        IQueryable<Investor> GetAllIncludingByCountryIdAsync(int countryId);
        IQueryable<Investor> GetAllIncludingForAdminAsync();
        Task<IEnumerable<Investor>> GetAllIncludingMostPopularInvestorsAsync();
        Task<IEnumerable<Investor>> GetAllIncludingUnPopularInvestorsAsync();
        Task<IEnumerable<Investor>> GetAllIncludingInvestorsForPublicUser();
        IQueryable<Investor> GetAllIncludingMostLikedInvestorsAsync();
        IQueryable<Investor> GetAllIncludingMostSavedInvestorsAsync();
        IQueryable<Investor> GetAllIncludingMostHitInvestorsAsync();
        IQueryable<Investor> GetAllIncludingLessLikedInvestorsAsync();
        IQueryable<Investor> GetAllIncludingLessSavedInvestorsAsync();
        IQueryable<Investor> GetAllIncludingLessHitInvestorsAsync();
        Task<IEnumerable<Investor>> GetAllIncludingInvestorTodayAsync();
        IQueryable<Investor> GetAllIncludingInvestorSearchResult(string investArea, string? sinceWhen, bool isLookingForCompany, int? countryId, int? investorCategoryId);
        Task<IEnumerable<Investor>> GetAllForSignalRAsync();
        Task<Investor> GetByIdAsync(int? id);
        Task<Investor?> GetBySlugAsync(string slug);
        Task<Investor> GetInvestorForProfileByUserIdAsync(string userId);        
        Task<bool> CreateAsync(string bio, string investArea, DateTime sinceWhen, bool isLookingForCompany, string? emailAddress, string? phoneNumber, int investorCategoryId, int countryId, string appUserId, IFormFile image);
        Task<bool> UpdateAsync(string bio, string investArea, DateTime sinceWhen, bool isLookingForCompany, string? emailAddress, string? phoneNumber, int investorCategoryId, int countryId, string appUserId, IFormFile image, int id);
        Task<bool> DeleteAsync(Investor entity, int id);
        Task<bool> SetInvestorLookingForCompanyAsync(int id);
        Task<bool> SetInvestorNotLookingForCompanyAsync(int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        IQueryable<Investor> GetAllForSitemap();
        IEnumerable<Investor> GetAllIncludeLastJoinedInvestorForAdmin();
        IEnumerable<Investor> GetAllIncludingLastInvestor();
        IEnumerable<Investor> GetAllIncludingMostPopularInvestors();
        IEnumerable<Investor> GetAllIncludingInvestorsRandomForInvestorDetail();
        IEnumerable<Investor> GetAllIncludingInvestorForPublicUser();
        Investor GetInvestorIdForInvestorHeader(string userId);        
        int InvestorCounter();
    }
}
