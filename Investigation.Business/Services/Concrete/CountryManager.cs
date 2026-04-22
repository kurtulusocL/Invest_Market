using System.Linq.Expressions;
using Investigation.Business.Services.Abstract;
using Investigation.DataAccess.Abstract;
using Investigation.Domain.Entities;

namespace Investigation.Business.Services.Concrete
{
    public class CountryManager : ICountryService
    {
        readonly ICountryRepository _countryRepository;
        public CountryManager(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public async Task<bool> CreateAsync(Country entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var result = await _countryRepository.AddAsync(entity);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> DeleteAsync(Country entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var data = await _countryRepository.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _countryRepository.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public IQueryable<Country> GetAllCountriesForCompanySearch()
        {
            try
            {
                return _countryRepository.GetAll(i => i.IsActive == true && i.IsDeleted == false && i.Companies.Count() > 0).OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Country>().AsQueryable();
            }
        }

        public IQueryable<Country> GetAllCountriesForInvestorSearch()
        {
            try
            {
                return _countryRepository.GetAll(i => i.IsActive == true && i.IsDeleted == false && i.Investors.Count() > 0).OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Country>().AsQueryable();
            }
        }

        public async Task<IEnumerable<Country>> GetAllForSignalRAsync()
        {
            try
            {
                var data = await _countryRepository.GetAllIncludeAsync(new Expression<Func<Country, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Companies, y => y.Investors);
                return data.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Country>();
            }
        }

        public IQueryable<Country> GetAllForSitemap()
        {
            try
            {
                return _countryRepository.GetAll(i => i.IsActive == true && i.IsDeleted == false).OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Country>().AsQueryable();
            }
        }

        public IQueryable<Country> GetAllIncludingAsync()
        {
            try
            {
                var data = _countryRepository.GetAllInclude(new Expression<Func<Country, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Companies, y => y.Investors);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Country>().AsQueryable();
            }
        }

        public IQueryable<Country> GetAllIncludingByCompanyQuantityAsync()
        {
            try
            {
                var data = _countryRepository.GetAllInclude(new Expression<Func<Country, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Companies, y => y.Investors);
                return data.OrderByDescending(i => i.Companies.Count());
            }
            catch (Exception)
            {
                return Enumerable.Empty<Country>().AsQueryable();
            }
        }

        public IQueryable<Country> GetAllIncludingByInvestorQuantityAsync()
        {
            try
            {
                var data = _countryRepository.GetAllInclude(new Expression<Func<Country, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Companies, y => y.Investors);
                return data.OrderByDescending(i => i.Investors.Count());
            }
            catch (Exception)
            {
                return Enumerable.Empty<Country>().AsQueryable();
            }
        }

        public IQueryable<Country> GetAllIncludingCompanyCountries()
        {
            try
            {
                return _countryRepository.GetAllInclude(new Expression<Func<Country, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Companies.Count()>0
                }, null, y => y.Companies).OrderBy(i => i.Name);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Country>().AsQueryable();
            }
        }

        public IQueryable<Country> GetAllIncludingForAddCompanyAsync()
        {
            try
            {
                var data = _countryRepository.GetAllInclude(new Expression<Func<Country, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Companies);
                return data.OrderBy(i => i.Name);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Country>().AsQueryable();
            }
        }

        public IQueryable<Country> GetAllIncludingForAddInvestorAsync()
        {
            try
            {
                var data = _countryRepository.GetAllInclude(new Expression<Func<Country, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Investors);
                return data.OrderBy(i => i.Name);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Country>().AsQueryable();
            }
        }

        public IQueryable<Country> GetAllIncludingForAdminAsync()
        {
            try
            {
                var data = _countryRepository.GetAllInclude(new Expression<Func<Country, bool>>[]
                {

                }, null, y => y.Companies, y => y.Investors);
                return data.OrderByDescending(i => i.Investors.Count());
            }
            catch (Exception)
            {
                return Enumerable.Empty<Country>().AsQueryable();
            }
        }

        public IQueryable<Country> GetAllIncludingForAdminHome()
        {
            try
            {
                return _countryRepository.GetAllInclude(new Expression<Func<Country, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Companies, y => y.Investors).OrderByDescending(i => i.Companies.Count());
            }
            catch (Exception)
            {
                return Enumerable.Empty<Country>().AsQueryable();
            }
        }

        public IQueryable<Country> GetAllIncludingInvestorCountries()
        {
            try
            {
                return _countryRepository.GetAllInclude(new Expression<Func<Country, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Investors.Count()>0
                }, null, y => y.Investors).OrderBy(i => i.Name);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Country>().AsQueryable();
            }
        }

        public async Task<Country> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                return await _countryRepository.GetIncludeAsync(i => i.Id == id, y => y.Companies, y => y.Investors);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _countryRepository.SetActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _countryRepository.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _countryRepository.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _countryRepository.SetNotDeletedAsync(id);
            return result;
        }

        public async Task<bool> UpdateAsync(Country entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                entity.UpdatedDate = DateTime.UtcNow;
                var result = await _countryRepository.UpdateAsync(entity);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while updating the entity.", ex);
            }
        }
    }
}
