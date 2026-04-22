using System.Linq.Expressions;
using Ganss.Xss;
using Investigation.Business.Services.Abstract;
using Investigation.DataAccess.Abstract;
using Investigation.Domain.Entities;

namespace Investigation.Business.Services.Concrete
{
    public class CompanyContactManager : ICompanyContactService
    {
        readonly ICompanyContactRepository _companyContactRepository;
        private readonly IHtmlSanitizer _htmlSanitizer;
        public CompanyContactManager(ICompanyContactRepository companyContactRepository, IHtmlSanitizer htmlSanitizer)
        {
            _companyContactRepository = companyContactRepository;
            _htmlSanitizer = htmlSanitizer;
        }

        public async Task<bool> CreateAsync(string website, string? phoneNumber, string email, string location, int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                ArgumentNullException.ThrowIfNull(_htmlSanitizer, nameof(_htmlSanitizer));
                string safeLocation = _htmlSanitizer.Sanitize(location ?? string.Empty);
                var entity = new CompanyContact
                {
                    Website = website,
                    PhoneNumber = phoneNumber,
                    Email = email,
                    Location = safeLocation,
                    CompanyId = companyId
                };
                var result = await _companyContactRepository.AddAsync(entity);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> DeleteAsync(CompanyContact entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var data = await _companyContactRepository.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _companyContactRepository.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public async Task<IEnumerable<CompanyContact>> GetAllForSignalRAsync()
        {
            try
            {
                var data = await _companyContactRepository.GetAllIncludeAsync(new Expression<Func<CompanyContact, bool>>[]
                {
                   
                }, null, y => y.Company);
                return data.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
               return new List<CompanyContact>();
            }
        }

        public IQueryable<CompanyContact> GetAllIncludingAsync()
        {
            try
            {
                var data =  _companyContactRepository.GetAllInclude(new Expression<Func<CompanyContact, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Company);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<CompanyContact>().AsQueryable();
            }
        }

        public IQueryable<CompanyContact> GetAllIncludingByCompanyIdAsync(int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                var data =  _companyContactRepository.GetAllIncludeById(companyId, "CompanyId", new Expression<Func<CompanyContact, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Company);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<CompanyContact>().AsQueryable();
            }
        }

        public IQueryable<CompanyContact> GetAllIncludingCompanyContactByCompanyIdAsync(int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                var data =  _companyContactRepository.GetAllIncludeById(companyId, "CompanyId", new Expression<Func<CompanyContact, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Company.IsActive==true&&i.IsDeleted==false
                }, y => y.Company);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<CompanyContact>().AsQueryable();
            }
        }

        public IQueryable<CompanyContact> GetAllIncludingForAdminAsync()
        {
            try
            {
                var data =  _companyContactRepository.GetAllInclude(new Expression<Func<CompanyContact, bool>>[]
                {

                }, null, y => y.Company);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<CompanyContact>().AsQueryable();
            }
        }

        public async Task<CompanyContact> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                return await _companyContactRepository.GetIncludeAsync(i => i.Id == id, y => y.Company);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public CompanyContact GetCompanyContactByCompanyId(int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                return _companyContactRepository.GetInclude(i => i.CompanyId == companyId);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<CompanyContact> GetCompanyContactByCompanyIdAsync(int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                return await _companyContactRepository.GetIncludeAsync(i => i.CompanyId == companyId, y => y.Company);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _companyContactRepository.SetActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _companyContactRepository.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _companyContactRepository.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _companyContactRepository.SetNotDeletedAsync(id);
            return result;
        }

        public async Task<bool> UpdateAsync(string website, string? phoneNumber, string email, string location, int? companyId, int id)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                ArgumentNullException.ThrowIfNull(_htmlSanitizer, nameof(_htmlSanitizer));
                string safeLocation = _htmlSanitizer.Sanitize(location ?? string.Empty);
                var entity = new CompanyContact
                {
                    Website = website,
                    PhoneNumber = phoneNumber,
                    Email = email,
                    Location = safeLocation,
                    CompanyId = companyId,
                    Id = id,
                    UpdatedDate = DateTime.UtcNow
                };
                var result = await _companyContactRepository.UpdateAsync(entity);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while updating the entity.", ex);
            }
        }
    }
}
