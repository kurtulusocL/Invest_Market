using System.Linq.Expressions;
using Ganss.Xss;
using Investigation.Business.Services.Abstract;
using Investigation.DataAccess.Abstract;
using Investigation.Domain.Entities;

namespace Investigation.Business.Services.Concrete
{
    public class CompanyPintechManager : ICompanyPintechService
    {
        readonly ICompanyPintechRepository _companyPintechRepository;
        private readonly IHtmlSanitizer _htmlSanitizer;
        public CompanyPintechManager(ICompanyPintechRepository companyPintechRepository, IHtmlSanitizer htmlSanitizer)
        {
            _companyPintechRepository = companyPintechRepository;
            _htmlSanitizer = htmlSanitizer;
        }

        public async Task<bool> CreateAsync(string workPlan, string serviceProduct, string description, string marketingStrategy, string growingPotantial, int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                ArgumentNullException.ThrowIfNull(_htmlSanitizer, nameof(_htmlSanitizer));
                string safeWorkPlan = _htmlSanitizer.Sanitize(workPlan ?? string.Empty);
                string safeDescription = _htmlSanitizer.Sanitize(description ?? string.Empty);
                string safeMarketingStrategy = _htmlSanitizer.Sanitize(marketingStrategy ?? string.Empty);
                string safeGrowingPotantial = _htmlSanitizer.Sanitize(growingPotantial ?? string.Empty);

                var entity = new CompanyPintech
                {
                    WorkPlan = safeWorkPlan,
                    ServiceProduct = serviceProduct,
                    Description = safeDescription,
                    MarketingStrategy = safeMarketingStrategy,
                    GrowingPotantial = safeGrowingPotantial,
                    CompanyId = companyId
                };
                var result = await _companyPintechRepository.AddAsync(entity);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> DeleteAsync(CompanyPintech entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var data = await _companyPintechRepository.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _companyPintechRepository.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public async Task<IEnumerable<CompanyPintech>> GetAllForSignalRAsync()
        {
            try
            {
                var data = await _companyPintechRepository.GetAllIncludeAsync(new Expression<Func<CompanyPintech, bool>>[]
                {
                    
                }, null, y => y.Company, y => y.Hits);
                return data.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<CompanyPintech>();
            }
        }

        public IQueryable<CompanyPintech> GetAllIncludingAsync()
        {
            try
            {
                var data = _companyPintechRepository.GetAllInclude(new Expression<Func<CompanyPintech, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Company, y => y.Hits);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<CompanyPintech>().AsQueryable();
            }
        }

        public IQueryable<CompanyPintech> GetAllIncludingByCompanyIdAsync(int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                var data = _companyPintechRepository.GetAllIncludeById(companyId, "CompanyId", new Expression<Func<CompanyPintech, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Company, y => y.Hits);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<CompanyPintech>().AsQueryable();
            }
        }

        public IQueryable<CompanyPintech> GetAllIncludingByVisibilitySettingIdAsync(int? visibilitySettingId)
        {
            try
            {
                if (visibilitySettingId == null)
                    throw new ArgumentNullException(nameof(visibilitySettingId), "visibilitySettingId was null");

                var data = _companyPintechRepository.GetAllIncludeById(visibilitySettingId, "VisibilitySettingId", new Expression<Func<CompanyPintech, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Company, y => y.Hits);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<CompanyPintech>().AsQueryable();
            }
        }

        public IQueryable<CompanyPintech> GetAllIncludingCompanyPintechForCompanyByCompanyIdAsync(int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                var data = _companyPintechRepository.GetAllIncludeById(companyId, "CompanyId", new Expression<Func<CompanyPintech, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Company.IsActive==true&&i.IsDeleted==false
                }, y => y.Company, y => y.Hits);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<CompanyPintech>().AsQueryable();
            }
        }

        public IQueryable<CompanyPintech> GetAllIncludingForAdminAsync()
        {
            try
            {
                var data = _companyPintechRepository.GetAllInclude(new Expression<Func<CompanyPintech, bool>>[]
                {

                }, null, y => y.Company, y => y.Hits);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<CompanyPintech>().AsQueryable();
            }
        }

        public async Task<CompanyPintech> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                return await _companyPintechRepository.GetIncludeAsync(i => i.Id == id, y => y.Company, y => y.Hits);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<CompanyPintech> GetCompanyPintechByCompanyIdAsync(int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                return await _companyPintechRepository.GetIncludeAsync(i => i.CompanyId == companyId, y => y.Company, y => y.Hits);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public CompanyPintech GetCompanyPintechForCompanyDetailByCompanyId(int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                return _companyPintechRepository.GetInclude(i => i.CompanyId == companyId, y => y.VisibilitySetting, y => y.Hits);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _companyPintechRepository.SetActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _companyPintechRepository.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _companyPintechRepository.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _companyPintechRepository.SetNotDeletedAsync(id);
            return result;
        }

        public async Task<bool> UpdateAsync(string workPlan, string serviceProduct, string description, string marketingStrategy, string growingPotantial, int? companyId, int id)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                ArgumentNullException.ThrowIfNull(_htmlSanitizer, nameof(_htmlSanitizer));
                string safeWorkPlan = _htmlSanitizer.Sanitize(workPlan ?? string.Empty);
                string safeDescription = _htmlSanitizer.Sanitize(description ?? string.Empty);
                string safeMarketingStrategy = _htmlSanitizer.Sanitize(marketingStrategy ?? string.Empty);
                string safeGrowingPotantial = _htmlSanitizer.Sanitize(growingPotantial ?? string.Empty);

                var entity = new CompanyPintech
                {
                    WorkPlan = safeWorkPlan,
                    ServiceProduct = serviceProduct,
                    Description = safeDescription,
                    MarketingStrategy = safeMarketingStrategy,
                    GrowingPotantial = safeGrowingPotantial,
                    CompanyId = companyId,
                    Id = id,
                    UpdatedDate = DateTime.UtcNow,
                };
                var result = await _companyPintechRepository.UpdateAsync(entity);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while updating the entity.", ex);
            }
        }
    }
}
