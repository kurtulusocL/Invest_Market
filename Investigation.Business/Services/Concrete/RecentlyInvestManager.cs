using System.Linq.Expressions;
using Ganss.Xss;
using Investigation.Business.Constants.Helpers;
using Investigation.Business.Services.Abstract;
using Investigation.DataAccess.Abstract;
using Investigation.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Investigation.Business.Services.Concrete
{
    public class RecentlyInvestManager : IRecentlyInvestService
    {
        readonly IRecentlyInvestRepository _recentlyInvestRepository;
        readonly ISectorService _sectorService;
        readonly ISubSectorService _subSectorService;
        private readonly IHtmlSanitizer _htmlSanitizer;
        public RecentlyInvestManager(IRecentlyInvestRepository recentlyInvestRepository, ISectorService sectorService, ISubSectorService subSectorService, IHtmlSanitizer htmlSanitizer)
        {
            _recentlyInvestRepository = recentlyInvestRepository;
            _sectorService = sectorService;
            _subSectorService = subSectorService;
            _htmlSanitizer = htmlSanitizer;
        }

        public List<SelectListItem> SectorSelectSystem(int? sectorId, string tip)
        {
            try
            {
                var result = new List<SelectListItem>();

                switch (tip)
                {
                    case "getSectors":
                        var sectors =  _sectorService.GetAllIncludingForAddRecentlyInvestAsync();
                        result = sectors.Select(sector => new SelectListItem
                        {
                            Text = sector.Name,
                            Value = sector.Id.ToString()
                        }).ToList();
                        break;

                    case "getSubSectors":
                        if (sectorId == null)
                        {
                            throw new ArgumentNullException(nameof(sectorId), "Sector ID can not be empty.");
                        }

                        var subSectors =  _subSectorService.GetAllIncludingForAddRecentlyInvestBySectorIdAsync(sectorId.Value);
                        result = subSectors.Select(subsector => new SelectListItem
                        {
                            Text = subsector.Name,
                            Value = subsector.Id.ToString()
                        }).ToList();
                        break;

                    default:
                        throw new ArgumentException($"Unsupported type: {tip}");
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("There was an error", ex);
            }
        }

        public async Task<bool> CreateAsync(string title, string? desc, DateTime investDate, bool isExit, DateTime? exitDate, string? webUrl, int sectorId, int? subSectorId, int? investorId, IFormFile? image)
        {
            try
            {
                if (investorId == null)
                    throw new ArgumentNullException(nameof(investorId), "investorId was null");

                ArgumentNullException.ThrowIfNull(_htmlSanitizer, nameof(_htmlSanitizer));

                if (image != null && image.Length > 0)
                {
                    ServiceImageHelper.ImageValidation(image);
                    try
                    {
                        string savedFileName = await ServiceImageHelper.RecentlyInvestImageResize(image);


                        string safeDesc = _htmlSanitizer.Sanitize(desc ?? string.Empty);
                        var entity = new RecentlyInvest
                        {
                            Title = title,
                            Desc = safeDesc,
                            InvestDate = investDate,
                            IsExit = isExit,
                            ExitDate = exitDate,
                            WebUrl = webUrl,
                            SectorId = sectorId,
                            SubSectorId = subSectorId,
                            InvestorId = investorId,
                            ImageUrl = savedFileName
                        };
                        var results = await _recentlyInvestRepository.AddAsync(entity);
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
                else
                {
                    string safeDesc = _htmlSanitizer.Sanitize(desc ?? string.Empty);
                    var entity = new RecentlyInvest
                    {
                        Title = title,
                        Desc = safeDesc,
                        InvestDate = investDate,
                        IsExit = isExit,
                        ExitDate = exitDate,
                        WebUrl = webUrl,
                        SectorId = sectorId,
                        SubSectorId = subSectorId,
                        InvestorId = investorId
                    };
                    if (entity != null)
                    {
                        var result = await _recentlyInvestRepository.AddAsync(entity);
                        return result;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> DeleteAsync(RecentlyInvest entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var data = await _recentlyInvestRepository.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _recentlyInvestRepository.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public IQueryable<RecentlyInvest> GetAllIncludingAsync()
        {
            try
            {
                var data = _recentlyInvestRepository.GetAllInclude(new Expression<Func<RecentlyInvest, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Sector, y => y.SubSector, y => y.Investor);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<RecentlyInvest>().AsQueryable();
            }
        }

        public IQueryable<RecentlyInvest> GetAllIncludingByExitsAsync()
        {
            try
            {
                var data = _recentlyInvestRepository.GetAllInclude(new Expression<Func<RecentlyInvest, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsExit==true,
                    i=>i.ExitDate!=null
                }, null, y => y.Sector, y => y.SubSector, y => y.Investor);
                return data.OrderByDescending(i => i.ExitDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<RecentlyInvest>().AsQueryable();
            }
        }

        public IQueryable<RecentlyInvest> GetAllIncludingByInvestDateAsync()
        {
            try
            {
                var data = _recentlyInvestRepository.GetAllInclude(new Expression<Func<RecentlyInvest, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Sector, y => y.SubSector, y => y.Investor);
                return data.OrderByDescending(i => i.InvestDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<RecentlyInvest>().AsQueryable();
            }
        }

        public IQueryable<RecentlyInvest> GetAllIncludingByInvestorIdAsync(int? investorId)
        {
            try
            {
                if (investorId == null)
                    throw new ArgumentNullException(nameof(investorId), "investorId was null");

                var data = _recentlyInvestRepository.GetAllIncludeById(investorId, "InvestorId", new Expression<Func<RecentlyInvest, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Sector, y => y.SubSector, y => y.Investor);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<RecentlyInvest>().AsQueryable();
            }
        }

        public IQueryable<RecentlyInvest> GetAllIncludingByNotExitsAsync()
        {
            try
            {
                var data = _recentlyInvestRepository.GetAllInclude(new Expression<Func<RecentlyInvest, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsExit==false
                }, null, y => y.Sector, y => y.SubSector, y => y.Investor);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<RecentlyInvest>().AsQueryable();
            }
        }

        public IQueryable<RecentlyInvest> GetAllIncludingBySectorIdAsync(int sectorId)
        {
            try
            {
                var data = _recentlyInvestRepository.GetAllIncludeById(sectorId, "SectorId", new Expression<Func<RecentlyInvest, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Sector, y => y.SubSector, y => y.Investor);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<RecentlyInvest>().AsQueryable();
            }
        }

        public IQueryable<RecentlyInvest> GetAllIncludingBySubSectorIdAsync(int? subSectorId)
        {
            try
            {
                if (subSectorId == null)
                    throw new ArgumentNullException(nameof(subSectorId), "subSectorId was null");

                var data = _recentlyInvestRepository.GetAllIncludeById(subSectorId, "SubSectorId", new Expression<Func<RecentlyInvest, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Sector, y => y.SubSector, y => y.Investor);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<RecentlyInvest>().AsQueryable();
            }
        }

        public IQueryable<RecentlyInvest> GetAllIncludingForAdminAsync()
        {
            try
            {
                var data = _recentlyInvestRepository.GetAllInclude(new Expression<Func<RecentlyInvest, bool>>[]
                {

                }, null, y => y.Sector, y => y.SubSector, y => y.Investor);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<RecentlyInvest>().AsQueryable();
            }
        }

        public async Task<RecentlyInvest> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                return await _recentlyInvestRepository.GetIncludeAsync(i => i.Id == id, y => y.Sector, y => y.SubSector, y => y.Investor);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetHasExitInvestAsync(int id)
        {
            var result = await _recentlyInvestRepository.SetHasExitInvestAsync(id);
            return result;
        }

        public async Task<bool> SetHasNotExitInvestAsync(int id)
        {
            var result = await _recentlyInvestRepository.SetHasNotExitInvestAsync(id);
            return result;
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _recentlyInvestRepository.SetActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _recentlyInvestRepository.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _recentlyInvestRepository.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _recentlyInvestRepository.SetNotDeletedAsync(id);
            return result;
        }

        public async Task<bool> UpdateAsync(string title, string? desc, DateTime investDate, bool isExit, DateTime? exitDate, string? webUrl, int sectorId, int? subSectorId, int? investorId, IFormFile? image, int id)
        {
            try
            {
                if (investorId == null)
                    throw new ArgumentNullException(nameof(investorId), "investorId was null");

                ArgumentNullException.ThrowIfNull(_htmlSanitizer, nameof(_htmlSanitizer));

                if (image != null && image.Length > 0)
                {
                    ServiceImageHelper.ImageValidation(image);
                    try
                    {
                        string savedFileName = await ServiceImageHelper.RecentlyInvestImageResize(image);

                        string safeDesc = _htmlSanitizer.Sanitize(desc ?? string.Empty);
                        var entity = new RecentlyInvest
                        {
                            Title = title,
                            Desc = safeDesc,
                            InvestDate = investDate,
                            IsExit = isExit,
                            ExitDate = exitDate,
                            WebUrl = webUrl,
                            SectorId = sectorId,
                            SubSectorId = subSectorId,
                            InvestorId = investorId,
                            ImageUrl = savedFileName,
                            Id = id,
                            UpdatedDate = DateTime.UtcNow
                        };
                        var results = await _recentlyInvestRepository.UpdateAsync(entity);
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
                else
                {
                    string safeDesc = _htmlSanitizer.Sanitize(desc ?? string.Empty);
                    var entity = new RecentlyInvest
                    {
                        Title = title,
                        Desc = safeDesc,
                        InvestDate = investDate,
                        IsExit = isExit,
                        ExitDate = exitDate,
                        WebUrl = webUrl,
                        SectorId = sectorId,
                        SubSectorId = subSectorId,
                        InvestorId = investorId,
                        Id = id,
                        UpdatedDate = DateTime.UtcNow
                    };
                    if (entity != null)
                    {
                        var result = await _recentlyInvestRepository.UpdateAsync(entity);
                        return result;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while updating the entity.", ex);
            }
        }

        public IQueryable<RecentlyInvest> GetAllIncludingRecentlyInvestForInvestorByInvestorIdAsync(int? investorId)
        {
            try
            {
                if (investorId == null)
                    throw new ArgumentNullException(nameof(investorId), "investorId was null");

                var data = _recentlyInvestRepository.GetAllIncludeById(investorId, "InvestorId", new Expression<Func<RecentlyInvest, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Sector, y => y.SubSector, y => y.Investor);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<RecentlyInvest>().AsQueryable();
            }
        }

        public IQueryable<RecentlyInvest> GetAllIncludingRecentlyInvestByExitByInvestorId(int? investorId)
        {
            try
            {
                if (investorId == null)
                    throw new ArgumentNullException(nameof(investorId), "investorId was null");

                return _recentlyInvestRepository.GetAllIncludeById(investorId, "InvestorId", new Expression<Func<RecentlyInvest, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsExit==true
                }, y => y.Sector, y => y.SubSector, y => y.Investor).OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<RecentlyInvest>().AsQueryable();
            }
        }

        public IQueryable<RecentlyInvest> GetAllIncludingRecentlyInvestByNotExitByInvestorId(int? investorId)
        {
            try
            {
                if (investorId == null)
                    throw new ArgumentNullException(nameof(investorId), "investorId was null");

                return _recentlyInvestRepository.GetAllIncludeById(investorId, "InvestorId", new Expression<Func<RecentlyInvest, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsExit==false
                }, y => y.Sector, y => y.SubSector, y => y.Investor).OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<RecentlyInvest>().AsQueryable();
            }
        }

        public IQueryable<RecentlyInvest> GetAllIncludingLastRecentlyInvestForIndex()
        {
            try
            {
                //var today = DateTime.Today;
                //var tomorrow = today.AddDays(1);

                return _recentlyInvestRepository.GetAllInclude(new Expression<Func<RecentlyInvest, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                    //i => i.CreatedDate >= today && i.CreatedDate < tomorrow
                }, null, y => y.Sector, y => y.SubSector, y => y.Investor, y => y.Investor.AppUser).OrderByDescending(i => Guid.NewGuid()).Take(50);
            }
            catch (Exception)
            {
                return Enumerable.Empty<RecentlyInvest>().AsQueryable();
            }
        }

        public IQueryable<RecentlyInvest> GetAllIncludingLastRecentlyInvestForTimeline()
        {
            try
            {
                //var today = DateTime.Today;
                //var tomorrow = today.AddDays(1);

                return _recentlyInvestRepository.GetAllInclude(new Expression<Func<RecentlyInvest, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                    //i => i.CreatedDate >= today && i.CreatedDate < tomorrow
                }, null, y => y.Sector, y => y.SubSector, y => y.Investor.AppUser).OrderByDescending(i => Guid.NewGuid()).Take(40);
            }
            catch (Exception)
            {
                return Enumerable.Empty<RecentlyInvest>().AsQueryable();
            }
        }

        public IQueryable<RecentlyInvest> GetAllIncludingRecentlyInvestForInvestorDetail(int? investorId)
        {
            try
            {
                if (investorId == null)
                    throw new ArgumentNullException(nameof(investorId), "investorId was null");

                return _recentlyInvestRepository.GetAllIncludeById(investorId, "InvestorId", new Expression<Func<RecentlyInvest, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Investor, y => y.Sector, y => y.SubSector).OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<RecentlyInvest>().AsQueryable();
            }
        }

        public async Task<IEnumerable<RecentlyInvest>> GetAllForSignalRAsync()
        {
            try
            {
                var data = await _recentlyInvestRepository.GetAllIncludeAsync(new Expression<Func<RecentlyInvest, bool>>[]
                {

                }, null, y => y.Sector, y => y.SubSector, y => y.Investor);
                return data.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<RecentlyInvest>();
            }
        }
    }
}
