using System.Linq.Expressions;
using Investigation.Business.Constants.Helpers;
using Investigation.Business.Services.Abstract;
using Investigation.DataAccess.Abstract;
using Investigation.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Investigation.Business.Services.Concrete
{
    public class CompanyTeamManager : ICompanyTeamService
    {
        readonly ICompanyTeamRepository _companyTeamRepository;
        public CompanyTeamManager(ICompanyTeamRepository companyTeamRepository)
        {
            _companyTeamRepository = companyTeamRepository;
        }

        public async Task<bool> CreateAsync(string nameSurname, string email, string title, int totalExperienceDuration, int? companyId, IFormFile image)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                if (image != null && image.Length > 0)
                {
                    ServiceImageHelper.ImageValidation(image);
                    try
                    {
                        string savedFileName = await ServiceImageHelper.CompanyTeamImageResize(image);
                        var entity = new CompanyTeam
                        {
                            NameSurname = nameSurname,
                            Email = email,
                            Title = title,
                            TotalExperienceDuration = totalExperienceDuration,
                            CompanyId = companyId,
                            PhotoUrl = savedFileName
                        };

                        var results = await _companyTeamRepository.AddAsync(entity);
                        if (!results)
                        {
                            return false;
                        }
                        return true;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> DeleteAsync(CompanyTeam entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var data = await _companyTeamRepository.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _companyTeamRepository.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public async Task<IEnumerable<CompanyTeam>> GetAllForSignalRAsync()
        {
            try
            {
                var data = await _companyTeamRepository.GetAllIncludeAsync(new Expression<Func<CompanyTeam, bool>>[]
                {
                   
                }, null, y => y.Company);
                return data.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<CompanyTeam>();
            }
        }

        public IQueryable<CompanyTeam> GetAllIncludingAsync()
        {
            try
            {
                var data = _companyTeamRepository.GetAllInclude(new Expression<Func<CompanyTeam, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Company);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<CompanyTeam>().AsQueryable();
            }
        }

        public IQueryable<CompanyTeam> GetAllIncludingByCompanyIdAsync(int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                var data = _companyTeamRepository.GetAllIncludeById(companyId, "CompanyId", new Expression<Func<CompanyTeam, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Company);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<CompanyTeam>().AsQueryable();
            }
        }

        public IQueryable<CompanyTeam> GetAllIncludingByCompanyNameAsync()
        {
            try
            {
                var data = _companyTeamRepository.GetAllInclude(new Expression<Func<CompanyTeam, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Company);
                return data.OrderBy(i => i.Company.Name);
            }
            catch (Exception)
            {
                return Enumerable.Empty<CompanyTeam>().AsQueryable();
            }
        }

        public IQueryable<CompanyTeam> GetAllIncludingCompanyTeamByCompanyId(int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                return _companyTeamRepository.GetAllIncludeById(companyId, "CompanyId", new Expression<Func<CompanyTeam, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Company.IsActive==true&&i.IsDeleted==false
                }, y => y.Company).OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<CompanyTeam>().AsQueryable();
            }
        }

        public IQueryable<CompanyTeam> GetAllIncludingCompanyTeamByCompanyIdAsync(int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                var data = _companyTeamRepository.GetAllIncludeById(companyId, "CompanyId", new Expression<Func<CompanyTeam, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Company.IsActive==true&&i.IsDeleted==false
                }, y => y.Company);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<CompanyTeam>().AsQueryable();
            }
        }

        public IQueryable<CompanyTeam> GetAllIncludingForAdminAsync()
        {
            try
            {
                var data = _companyTeamRepository.GetAllInclude(new Expression<Func<CompanyTeam, bool>>[]
                {

                }, null, y => y.Company);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<CompanyTeam>().AsQueryable();
            }
        }

        public async Task<CompanyTeam> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                return await _companyTeamRepository.GetIncludeAsync(i => i.Id == id, y => y.Company);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<CompanyTeam> GetCompanyTeamByCompanyIdAsync(int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                return await _companyTeamRepository.GetIncludeAsync(i => i.CompanyId == companyId, y => y.Company);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _companyTeamRepository.SetActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _companyTeamRepository.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _companyTeamRepository.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _companyTeamRepository.SetNotDeletedAsync(id);
            return result;
        }

        public async Task<bool> UpdateAsync(string nameSurname, string email, string title, int totalExperienceDuration, int? companyId, IFormFile image, int id)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                if (image != null && image.Length > 0)
                {
                    ServiceImageHelper.ImageValidation(image);
                    try
                    {
                        string savedFileName = await ServiceImageHelper.CompanyTeamImageResize(image);

                        var entity = new CompanyTeam
                        {
                            NameSurname = nameSurname,
                            Email = email,
                            Title = title,
                            TotalExperienceDuration = totalExperienceDuration,
                            CompanyId = companyId,
                            PhotoUrl = savedFileName,
                            Id = id,
                            UpdatedDate = DateTime.UtcNow
                        };

                        var results = await _companyTeamRepository.UpdateAsync(entity);
                        if (!results)
                        {
                            return false;
                        }
                        return true;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while updating the entity.", ex);
            }
        }
    }
}
