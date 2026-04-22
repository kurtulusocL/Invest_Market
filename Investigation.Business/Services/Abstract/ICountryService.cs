using Investigation.Domain.Entities;

namespace Investigation.Business.Services.Abstract
{
    public interface ICountryService
    {
        IQueryable<Country> GetAllIncludingAsync();
        IQueryable<Country> GetAllIncludingByInvestorQuantityAsync();
        IQueryable<Country> GetAllIncludingByCompanyQuantityAsync();
        IQueryable<Country> GetAllIncludingForAddInvestorAsync();
        IQueryable<Country> GetAllIncludingForAddCompanyAsync();
        IQueryable<Country> GetAllIncludingForAdminAsync();
        Task<IEnumerable<Country>> GetAllForSignalRAsync();
        Task<Country> GetByIdAsync(int? id);
        Task<bool> CreateAsync(Country entity);
        Task<bool> UpdateAsync(Country entity);
        Task<bool> DeleteAsync(Country entity, int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        IQueryable<Country> GetAllForSitemap();
        IQueryable<Country> GetAllIncludingForAdminHome();
        IQueryable<Country> GetAllIncludingInvestorCountries();
        IQueryable<Country> GetAllIncludingCompanyCountries();
        IQueryable<Country> GetAllCountriesForCompanySearch();
        IQueryable<Country> GetAllCountriesForInvestorSearch();
    }
}
