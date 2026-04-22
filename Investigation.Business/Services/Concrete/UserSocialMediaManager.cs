using System.Linq.Expressions;
using Investigation.Business.Services.Abstract;
using Investigation.DataAccess.Abstract;
using Investigation.Domain.Entities;

namespace Investigation.Business.Services.Concrete
{
    public class UserSocialMediaManager : IUserSocialMediaService
    {
        readonly IUserSocialMediaRepository _userSocialMediaRepository;
        public UserSocialMediaManager(IUserSocialMediaRepository userSocialMediaRepository)
        {
            _userSocialMediaRepository = userSocialMediaRepository;
        }

        public async Task<bool> CreateCompanyUserAsync(string name, string url, int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                var entity = new UserSocialMedia
                {
                    Name = name,
                    Url = url,
                    CompanyId = companyId
                };
                if (entity != null)
                {
                    var result = await _userSocialMediaRepository.AddAsync(entity);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> CreateInvestorUserAsync(string name, string url, int? investorId)
        {
            try
            {
                if (investorId == null)
                    throw new ArgumentNullException(nameof(investorId), "investorId was null");

                var entity = new UserSocialMedia
                {
                    Name = name,
                    Url = url,
                    InvestorId = investorId
                };
                if (entity != null)
                {
                    var result = await _userSocialMediaRepository.AddAsync(entity);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> DeleteAsync(UserSocialMedia entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var data = await _userSocialMediaRepository.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _userSocialMediaRepository.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public async Task<IEnumerable<UserSocialMedia>> GetAllForSignalRAsync()
        {
            try
            {
                var data = await _userSocialMediaRepository.GetAllIncludeAsync(new Expression<Func<UserSocialMedia, bool>>[]
                {
                   
                }, null, y => y.Company, y => y.Investor);
                return data.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<UserSocialMedia>();
            }
        }

        public IQueryable<UserSocialMedia> GetAllIncludingAsync()
        {
            try
            {
                var data =  _userSocialMediaRepository.GetAllInclude(new Expression<Func<UserSocialMedia, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Company, y => y.Investor);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<UserSocialMedia>().AsQueryable();
            }
        }

        public IQueryable<UserSocialMedia> GetAllIncludingByCompanyIdAsync(int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                var data =  _userSocialMediaRepository.GetAllIncludeById(companyId, "CompanyId", new Expression<Func<UserSocialMedia, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Company, y => y.Investor);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<UserSocialMedia>().AsQueryable();
            }
        }

        public IQueryable<UserSocialMedia> GetAllIncludingByInvestorIdAsync(int? investorId)
        {
            try
            {
                if (investorId == null)
                    throw new ArgumentNullException(nameof(investorId), "investorId was null");

                var data =  _userSocialMediaRepository.GetAllIncludeById(investorId, "InvestorId", new Expression<Func<UserSocialMedia, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Company, y => y.Investor);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<UserSocialMedia>().AsQueryable();
            }
        }

        public IQueryable<UserSocialMedia> GetAllIncludingForAdminAsync()
        {
            try
            {
                var data =  _userSocialMediaRepository.GetAllInclude(new Expression<Func<UserSocialMedia, bool>>[]
                {

                }, null, y => y.Company, y => y.Investor);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<UserSocialMedia>().AsQueryable();
            }
        }

        public IQueryable<UserSocialMedia> GetAllIncludingSocialMediaForCompanyByCompanyId(int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                return _userSocialMediaRepository.GetAllIncludeById(companyId, "CompanyId", new Expression<Func<UserSocialMedia, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Company.IsActive==true&&i.IsDeleted==false
                }, y => y.Company).OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<UserSocialMedia>().AsQueryable();
            }
        }

        public IQueryable<UserSocialMedia> GetAllIncludingSocialMediaForCompanyByCompanyIdAsync(int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                var data =  _userSocialMediaRepository.GetAllIncludeById(companyId, "CompanyId", new Expression<Func<UserSocialMedia, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Company.IsActive==true&&i.IsDeleted==false
                }, y => y.Company);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<UserSocialMedia>().AsQueryable();
            }
        }

        public IQueryable<UserSocialMedia> GetAllIncludingSocialMediaForInvestorByInvestorId(int? investorId)
        {
            try
            {
                if (investorId == null)
                    throw new ArgumentNullException(nameof(investorId), "investorId was null");

                return _userSocialMediaRepository.GetAllIncludeById(investorId, "InvestorId", new Expression<Func<UserSocialMedia, bool>>[]
                 {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Investor.IsActive==true&&i.IsDeleted==false
                 }, y => y.Investor).OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<UserSocialMedia>().AsQueryable();
            }
        }

        public IQueryable<UserSocialMedia> GetAllIncludingSocialMediaForInvestorByInvestorIdAsync(int? investorId)
        {
            try
            {
                if (investorId == null)
                    throw new ArgumentNullException(nameof(investorId), "investorId was null");

                var data =  _userSocialMediaRepository.GetAllIncludeById(investorId, "InvestorId", new Expression<Func<UserSocialMedia, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Investor.IsActive==true&&i.IsDeleted==false
                }, y => y.Investor);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<UserSocialMedia>().AsQueryable();
            }
        }

        public IQueryable<UserSocialMedia> GetAllIncludingSocialmediaForInvestorDetail(int? investorId)
        {
            try
            {
                if (investorId == null)
                    throw new ArgumentNullException(nameof(investorId), "investorId was null");

                return _userSocialMediaRepository.GetAllIncludeById(investorId, "InvestorId", new Expression<Func<UserSocialMedia, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Investor).OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<UserSocialMedia>().AsQueryable();
            }
        }

        public async Task<UserSocialMedia> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                return await _userSocialMediaRepository.GetIncludeAsync(i => i.Id == id, y => y.Company, y => y.Investor);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _userSocialMediaRepository.SetActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _userSocialMediaRepository.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _userSocialMediaRepository.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _userSocialMediaRepository.SetNotDeletedAsync(id);
            return result;
        }

        public async Task<bool> UpdateCompanyUserAsync(string name, string url, int? companyId, int id)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                var entity = new UserSocialMedia
                {
                    Name = name,
                    Url = url,
                    CompanyId = companyId,
                    Id = id,
                    UpdatedDate = DateTime.UtcNow
                };
                if (entity != null)
                {
                    var result = await _userSocialMediaRepository.UpdateAsync(entity);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while updating the entity.", ex);
            }
        }

        public async Task<bool> UpdateInvestorUserAsync(string name, string url, int? investorId, int id)
        {
            try
            {
                if (investorId == null)
                    throw new ArgumentNullException(nameof(investorId), "investorId was null");

                var entity = new UserSocialMedia
                {
                    Name = name,
                    Url = url,
                    InvestorId = investorId,
                    Id = id,
                    UpdatedDate = DateTime.UtcNow
                };
                if (entity != null)
                {
                    var result = await _userSocialMediaRepository.UpdateAsync(entity);
                    return result;
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
