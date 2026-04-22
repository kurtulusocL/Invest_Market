using Investigation.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Investigation.Business.Services.Abstract
{
    public interface IBannerImageService
    {
        IQueryable<BannerImage> GetAllAsync();
        IQueryable<BannerImage> GetAllForAdminAsync();
        Task<IEnumerable<BannerImage>> GetAllForSignalRAsync();
        Task<BannerImage> GetByIdAsync(int? id);
        Task<bool> CreateAsync(BannerImage entity, IFormFile image);
        Task<bool> UpdateAsync(BannerImage entity, IFormFile image);
        Task<bool> DeleteAsync(BannerImage entity, int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        IQueryable<BannerImage> GetAllBlogBannerImage();
        IQueryable<BannerImage> GetAllSurveyBannerImage();
        IQueryable<BannerImage> GetAllAgreementBannerImage();
        IQueryable<BannerImage> GetAllDataPagesBannerImage();
        IQueryable<BannerImage> GetAllKvkkBannerImage();
        IQueryable<BannerImage> GetAllHowItWorksBannerImage();
        IQueryable<BannerImage> GetAllServicesBannerImage();
        IQueryable<BannerImage> GetAllFAQBannerImage();
        IQueryable<BannerImage> GetAllContactBannerImage();
        IQueryable<BannerImage> GetAllAboutBannerImage();
        IQueryable<BannerImage> GetAllEventBannerImage();
        IQueryable<BannerImage> GetAllEntrepreneurBannerImage();
        IQueryable<BannerImage> GetAllInvestorBannerImage();
        IQueryable<BannerImage> GetAllNewsBannerImage();
        IQueryable<BannerImage> GetAllSectorNewsBannerImage();
        IQueryable<BannerImage> GetAllSector404BannerImage();
        IQueryable<BannerImage> GetAllSector500BannerImage();
        IQueryable<BannerImage> GetAllSector400BannerImage();
    }
}
