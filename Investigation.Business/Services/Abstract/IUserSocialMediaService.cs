using Investigation.Domain.Entities;

namespace Investigation.Business.Services.Abstract
{
    public interface IUserSocialMediaService
    {
        IQueryable<UserSocialMedia> GetAllIncludingAsync();
        IQueryable<UserSocialMedia> GetAllIncludingByCompanyIdAsync(int? companyId);
        IQueryable<UserSocialMedia> GetAllIncludingByInvestorIdAsync(int? investorId);
        IQueryable<UserSocialMedia> GetAllIncludingForAdminAsync();
        IQueryable<UserSocialMedia> GetAllIncludingSocialMediaForInvestorByInvestorIdAsync(int? investorId);
        IQueryable<UserSocialMedia> GetAllIncludingSocialMediaForCompanyByCompanyIdAsync(int? companyId);
        Task<IEnumerable<UserSocialMedia>> GetAllForSignalRAsync();
        Task<UserSocialMedia> GetByIdAsync(int? id);
        Task<bool> CreateCompanyUserAsync(string name, string url, int? companyId);
        Task<bool> CreateInvestorUserAsync(string name, string url, int? investorId);
        Task<bool> UpdateCompanyUserAsync(string name, string url, int? companyId, int id);
        Task<bool> UpdateInvestorUserAsync(string name, string url, int? investorId, int id);
        Task<bool> DeleteAsync(UserSocialMedia entity, int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        IQueryable<UserSocialMedia> GetAllIncludingSocialMediaForInvestorByInvestorId(int? investorId);
        IQueryable<UserSocialMedia> GetAllIncludingSocialMediaForCompanyByCompanyId(int? companyId);
        IQueryable<UserSocialMedia> GetAllIncludingSocialmediaForInvestorDetail(int? investorId);
    }
}
