using Investigation.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Investigation.Business.Services.Abstract
{
    public interface IAnnouncementService
    {
        IQueryable<Announcement> GetAllIncludingAsync();
        IQueryable<Announcement> GetAllIncludingByAnnouncementCategoryIdAsync(int announcementCategoryId);
        IQueryable<Announcement> GetAllIncludingByCompanyIdAsync(int? companyId);
        IQueryable<Announcement> GetAllIncludingByInvestorIdAsync(int? investorId);
        IQueryable<Announcement> GetAllIncludingForAdminAsync();
        IQueryable<Announcement> GetAllIncludingAnnouncementForInvestorByInvestorIdAsync(int? investorId);
        IQueryable<Announcement> GetAllIncludingAnnouncementForCompanyByCompanyIdAsync(int? companyId);
        IQueryable<Announcement> GetAllIncludingAnnouncementTodayAsync();
        Task<IEnumerable<Announcement>> GetAllForSignalRAsync();
        Task<Announcement> GetByIdAsync(int? id);
        Task<Announcement?> GetBySlugAsync(string slug);
        Task<bool> CreateCompanyAnnouncementAsync(string title, string? subtitle, string content, int announcementCategoryId, int? companyId, IFormFile? image);
        Task<bool> CreateInvestorAnnouncemenetAsync(string title, string? subtitle, string content, int announcementCategoryId, int? investorId, IFormFile? image);
        Task<bool> UpdateCompanyAnnouncementAsync(string title, string? subtitle, string content, int announcementCategoryId, int? companyId, IFormFile? image, int id);
        Task<bool> UpdateInvestorAnnouncementAsync(string title, string? subtitle, string content, int announcementCategoryId, int? investorId, IFormFile? image, int id);
        Task<bool> DeleteAsync(Announcement entity, int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        IQueryable<Announcement> GetAllIncludingLastAnnouncementForIndex();
        IQueryable<Announcement> GetAllIncludingLastAnnouncementForTimeline();
        IQueryable<Announcement> GetAllIncludingAnnouncementForInvestorDetail(int? investorId);
        IQueryable<Announcement> GetAllIncludingAnnouncementByCompanyId(int? companyId);
    }
}
