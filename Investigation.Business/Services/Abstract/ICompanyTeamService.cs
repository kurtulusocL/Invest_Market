using Investigation.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Investigation.Business.Services.Abstract
{
    public interface ICompanyTeamService
    {
        IQueryable<CompanyTeam> GetAllIncludingAsync();
        IQueryable<CompanyTeam> GetAllIncludingByCompanyIdAsync(int? companyId);
        IQueryable<CompanyTeam> GetAllIncludingByCompanyNameAsync();
        IQueryable<CompanyTeam> GetAllIncludingForAdminAsync();
        IQueryable<CompanyTeam> GetAllIncludingCompanyTeamByCompanyIdAsync(int? companyId);
        Task<IEnumerable<CompanyTeam>> GetAllForSignalRAsync();
        Task<CompanyTeam> GetByIdAsync(int? id);
        Task<CompanyTeam> GetCompanyTeamByCompanyIdAsync(int? companyId);
        Task<bool> CreateAsync(string nameSurname, string email, string title, int totalExperienceDuration, int? companyId, IFormFile image);
        Task<bool> UpdateAsync(string nameSurname, string email, string title, int totalExperienceDuration, int? companyId, IFormFile image, int id);
        Task<bool> DeleteAsync(CompanyTeam entity, int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        IQueryable<CompanyTeam> GetAllIncludingCompanyTeamByCompanyId(int? companyId);
    }
}
