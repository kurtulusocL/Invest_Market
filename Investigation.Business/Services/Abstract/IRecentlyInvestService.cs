using Investigation.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Investigation.Business.Services.Abstract
{
    public interface IRecentlyInvestService
    {
        IQueryable<RecentlyInvest> GetAllIncludingAsync();
        IQueryable<RecentlyInvest> GetAllIncludingByExitsAsync();
        IQueryable<RecentlyInvest> GetAllIncludingByNotExitsAsync();
        IQueryable<RecentlyInvest> GetAllIncludingByInvestDateAsync();
        IQueryable<RecentlyInvest> GetAllIncludingBySectorIdAsync(int sectorId);
        IQueryable<RecentlyInvest> GetAllIncludingBySubSectorIdAsync(int? subSectorId);
        IQueryable<RecentlyInvest> GetAllIncludingByInvestorIdAsync(int? investorId);
        IQueryable<RecentlyInvest> GetAllIncludingForAdminAsync();
        IQueryable<RecentlyInvest> GetAllIncludingRecentlyInvestForInvestorByInvestorIdAsync(int? investorId);
        Task<IEnumerable<RecentlyInvest>> GetAllForSignalRAsync();
        Task<RecentlyInvest> GetByIdAsync(int? id);
        List<SelectListItem> SectorSelectSystem(int? sectorId, string tip);
        Task<bool> CreateAsync(string title, string? desc, DateTime investDate, bool isExit, DateTime? exitDate, string? webUrl, int sectorId, int? subSectorId, int? investorId, IFormFile? image);
        Task<bool> UpdateAsync(string title, string? desc, DateTime investDate, bool isExit, DateTime? exitDate, string? webUrl, int sectorId, int? subSectorId, int? investorId, IFormFile? image, int id);
        Task<bool> DeleteAsync(RecentlyInvest entity, int id);
        Task<bool> SetHasExitInvestAsync(int id);
        Task<bool> SetHasNotExitInvestAsync(int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        IQueryable<RecentlyInvest> GetAllIncludingRecentlyInvestByExitByInvestorId(int? investorId);
        IQueryable<RecentlyInvest> GetAllIncludingRecentlyInvestByNotExitByInvestorId(int? investorId);
        IQueryable<RecentlyInvest> GetAllIncludingLastRecentlyInvestForIndex();
        IQueryable<RecentlyInvest> GetAllIncludingLastRecentlyInvestForTimeline();
        IQueryable<RecentlyInvest> GetAllIncludingRecentlyInvestForInvestorDetail(int? investorId);
    }
}
