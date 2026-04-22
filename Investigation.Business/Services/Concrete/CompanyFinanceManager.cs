using System.Linq.Expressions;
using Investigation.Business.Services.Abstract;
using Investigation.DataAccess.Abstract;
using Investigation.Domain.Entities;

namespace Investigation.Business.Services.Concrete
{
    public class CompanyFinanceManager : ICompanyFinanceService
    {
        readonly ICompanyFinanceRepository _companyFinanceRepository;
        public CompanyFinanceManager(ICompanyFinanceRepository companyFinanceRepository)
        {
            _companyFinanceRepository = companyFinanceRepository;
        }

        public async Task<bool> CreateAsync(decimal? marketvalue, decimal? arrIncome, decimal totalIncome, int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                var entity = new CompanyFinance
                {
                    MarketValue = marketvalue,
                    ARRIncome = arrIncome,
                    TotalIncome = totalIncome,
                    CompanyId = companyId
                };
                var result = await _companyFinanceRepository.AddAsync(entity);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> DeleteAsync(CompanyFinance entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var data = await _companyFinanceRepository.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _companyFinanceRepository.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public async Task<IEnumerable<CompanyFinance>> GetAllForSignalRAsync()
        {
            try
            {
                var data = await _companyFinanceRepository.GetAllIncludeAsync(new Expression<Func<CompanyFinance, bool>>[]
                {
                    
                }, null, y => y.Company, y => y.Hits);
                return data.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<CompanyFinance>();
            }
        }

        public IQueryable<CompanyFinance> GetAllIncludingAsync()
        {
            try
            {
                var data =  _companyFinanceRepository.GetAllInclude(new Expression<Func<CompanyFinance, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Company, y => y.Hits);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<CompanyFinance>().AsQueryable();
            }
        }

        public IQueryable<CompanyFinance> GetAllIncludingByCompanyIdAsync(int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                var data =  _companyFinanceRepository.GetAllIncludeById(companyId, "CompanyId", new Expression<Func<CompanyFinance, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Company, y => y.Hits);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<CompanyFinance>().AsQueryable();
            }
        }

        public IQueryable<CompanyFinance> GetAllIncludingByTotalIncomeAsync()
        {
            try
            {
                var data =  _companyFinanceRepository.GetAllInclude(new Expression<Func<CompanyFinance, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Company, y => y.Hits);
                return data.OrderByDescending(i => i.TotalIncome);
            }
            catch (Exception)
            {
                return Enumerable.Empty<CompanyFinance>().AsQueryable();
            }
        }

        public IQueryable<CompanyFinance> GetAllIncludingByVisilibilitySettingIdAsync(int? visibilitySettingId)
        {
            try
            {
                if (visibilitySettingId == null)
                    throw new ArgumentNullException(nameof(visibilitySettingId), "visibilitySettingId was null");

                var data =  _companyFinanceRepository.GetAllIncludeById(visibilitySettingId, "VisibilitySettingId", new Expression<Func<CompanyFinance, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Company, y => y.Hits);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<CompanyFinance>().AsQueryable();
            }
        }

        public IQueryable<CompanyFinance> GetAllIncludingCompanyFinanceForCompanyByCompanyIdAsync(int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                var data =  _companyFinanceRepository.GetAllIncludeById(companyId, "CompanyId", new Expression<Func<CompanyFinance, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Company.IsActive==true&&i.IsDeleted==false
                }, y => y.Company, y => y.Hits);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<CompanyFinance>().AsQueryable();
            }
        }

        public IQueryable<CompanyFinance> GetAllIncludingForAdminAsync()
        {
            try
            {
                var data = _companyFinanceRepository.GetAllInclude(new Expression<Func<CompanyFinance, bool>>[]
                {

                }, null, y => y.Company, y => y.Hits);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<CompanyFinance>().AsQueryable();
            }
        }

        public async Task<CompanyFinance> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                return await _companyFinanceRepository.GetIncludeAsync(i => i.Id == id, y => y.Company, y => y.Hits);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public CompanyFinance GetCompanyFinanceForCompanyDetailByCompanyId(int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                return _companyFinanceRepository.GetInclude(i => i.CompanyId == companyId, y => y.VisibilitySetting, y => y.Hits);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<CompanyFinance> GetCopanyFinanceByCompanyIdAsync(int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                return await _companyFinanceRepository.GetIncludeAsync(i => i.CompanyId == companyId, y => y.Company, y => y.Hits);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _companyFinanceRepository.SetActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _companyFinanceRepository.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _companyFinanceRepository.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _companyFinanceRepository.SetNotDeletedAsync(id);
            return result;
        }

        public async Task<bool> UpdateAsync(decimal? marketvalue, decimal? arrIncome, decimal totalIncome, int? companyId, int id)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                var entity = new CompanyFinance
                {
                    MarketValue = marketvalue,
                    ARRIncome = arrIncome,
                    TotalIncome = totalIncome,
                    CompanyId = companyId,
                    Id = id,
                    UpdatedDate = DateTime.UtcNow
                };
                var result = await _companyFinanceRepository.UpdateAsync(entity);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while updating the entity.", ex);
            }
        }
    }
}
