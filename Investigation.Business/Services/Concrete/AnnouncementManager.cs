using System.Linq.Expressions;
using Ganss.Xss;
using Investigation.Business.Constants.Helpers;
using Investigation.Business.Services.Abstract;
using Investigation.DataAccess.Abstract;
using Investigation.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Investigation.Business.Services.Concrete
{
    public class AnnouncementManager : IAnnouncementService
    {
        readonly IAnnouncementRepository _announcementRepository;
        private readonly IHtmlSanitizer _htmlSanitizer;
        public AnnouncementManager(IAnnouncementRepository announcementRepository, IHtmlSanitizer htmlSanitizer)
        {
            _announcementRepository = announcementRepository;
            _htmlSanitizer = htmlSanitizer;
        }

        public async Task<bool> CreateCompanyAnnouncementAsync(string title, string? subtitle, string content, int announcementCategoryId, int? companyId, IFormFile? image)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                if (image != null && image.Length > 0)
                {
                    ArgumentNullException.ThrowIfNull(_htmlSanitizer, nameof(_htmlSanitizer));

                    ServiceImageHelper.ImageValidation(image);
                    try
                    {
                        string savedFileName = await ServiceImageHelper.AnnouncememntImageResize(image);

                        string safeContent = _htmlSanitizer.Sanitize(content ?? string.Empty);
                        var entity = new Announcement
                        {
                            Title = title,
                            Subtitle = subtitle,
                            Content = safeContent,
                            AnnouncementCategoryId = announcementCategoryId,
                            CompanyId = companyId,
                            ImageUrl = savedFileName
                        };

                        var results = await _announcementRepository.AddAsync(entity);
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
                    string safeContent = _htmlSanitizer.Sanitize(content ?? string.Empty);
                    var entity = new Announcement
                    {
                        Title = title,
                        Subtitle = subtitle,
                        Content = safeContent,
                        AnnouncementCategoryId = announcementCategoryId,
                        CompanyId = companyId
                    };
                    if (entity != null)
                    {
                        var result = await _announcementRepository.AddAsync(entity);
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

        public async Task<bool> CreateInvestorAnnouncemenetAsync(string title, string? subtitle, string content, int announcementCategoryId, int? investorId, IFormFile? image)
        {
            try
            {
                if (investorId == null)
                    throw new ArgumentNullException(nameof(investorId), "investorId was null");

                if (image != null && image.Length > 0)
                {
                    ArgumentNullException.ThrowIfNull(_htmlSanitizer, nameof(_htmlSanitizer));

                    ServiceImageHelper.ImageValidation(image);
                    try
                    {
                        string savedFileName = await ServiceImageHelper.AnnouncememntImageResize(image);
                        string safeContent = _htmlSanitizer.Sanitize(content ?? string.Empty);
                        var entity = new Announcement
                        {
                            Title = title,
                            Subtitle = subtitle,
                            Content = safeContent,
                            AnnouncementCategoryId = announcementCategoryId,
                            InvestorId = investorId,
                            ImageUrl = savedFileName
                        };

                        var results = await _announcementRepository.AddAsync(entity);
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
                    string safeContent = _htmlSanitizer.Sanitize(content ?? string.Empty);
                    var entity = new Announcement
                    {
                        Title = title,
                        Subtitle = subtitle,
                        Content = safeContent,
                        AnnouncementCategoryId = announcementCategoryId,
                        InvestorId = investorId
                    };
                    if (entity != null)
                    {
                        var result = await _announcementRepository.AddAsync(entity);
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

        public async Task<bool> DeleteAsync(Announcement entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var data = await _announcementRepository.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _announcementRepository.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public IQueryable<Announcement> GetAllIncludingAnnouncementByCompanyId(int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                return _announcementRepository.GetAllIncludeById(companyId, "CompanyId", new Expression<Func<Announcement, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.AnnouncementCategory, y => y.Hits).OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Announcement>().AsQueryable();
            }
        }

        public IQueryable<Announcement> GetAllIncludingAnnouncementForCompanyByCompanyIdAsync(int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                var data = _announcementRepository.GetAllIncludeById(companyId, "CompanyId", new Expression<Func<Announcement, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Company.IsActive==true&&i.IsDeleted==false
                }, y => y.Company, y => y.AnnouncementCategory, y => y.Hits, y => y.Reports);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Announcement>().AsQueryable();
            }
        }

        public IQueryable<Announcement> GetAllIncludingAnnouncementForInvestorByInvestorIdAsync(int? investorId)
        {
            try
            {
                if (investorId == null)
                    throw new ArgumentNullException(nameof(investorId), "investorId was null");

                var data = _announcementRepository.GetAllIncludeById(investorId, "InvestorId", new Expression<Func<Announcement, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Investor.IsActive==true&&i.IsDeleted==false
                }, y => y.Investor, y => y.AnnouncementCategory, y => y.Hits, y => y.Reports);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Announcement>().AsQueryable();
            }
        }

        public IQueryable<Announcement> GetAllIncludingAnnouncementForInvestorDetail(int? investorId)
        {
            try
            {
                if (investorId == null)
                    throw new ArgumentNullException(nameof(investorId), "investorId was null");

                //var today = DateTime.Today;
                //var twoWeeksAgo = today.AddDays(-14);

                return _announcementRepository.GetAllIncludeById(investorId, "InvestorId", new Expression<Func<Announcement, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                   //i => i.CreatedDate >= twoWeeksAgo && i.CreatedDate < today.AddDays(1)
                }, y => y.AnnouncementCategory, y => y.Hits).OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Announcement>().AsQueryable();
            }
        }

        public IQueryable<Announcement> GetAllIncludingAnnouncementTodayAsync()
        {
            try
            {
                var today = DateTime.Now.Date;
                var data = _announcementRepository.GetAllInclude(new Expression<Func<Announcement, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i => i.CreatedDate >= today && i.CreatedDate < today.AddDays(1)
                }, null, y => y.Company, y => y.Investor, y => y.Investor.AppUser, y => y.AnnouncementCategory, y => y.Hits);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Announcement>().AsQueryable();
            }
        }

        public IQueryable<Announcement> GetAllIncludingAsync()
        {
            try
            {
                var data = _announcementRepository.GetAllInclude(new Expression<Func<Announcement, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Company, y => y.Investor, y => y.Investor.AppUser, y => y.AnnouncementCategory, y => y.Hits, y => y.Reports);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Announcement>().AsQueryable();
            }
        }

        public IQueryable<Announcement> GetAllIncludingByAnnouncementCategoryIdAsync(int announcementCategoryId)
        {
            try
            {
                var data = _announcementRepository.GetAllIncludeById(announcementCategoryId, "AnnouncementCategoryId", new Expression<Func<Announcement, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Company, y => y.Investor, y => y.Investor.AppUser, y => y.AnnouncementCategory, y => y.Hits, y => y.Reports);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Announcement>().AsQueryable();
            }
        }

        public IQueryable<Announcement> GetAllIncludingByCompanyIdAsync(int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                var data = _announcementRepository.GetAllIncludeById(companyId, "CompanyId", new Expression<Func<Announcement, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Company, y => y.Investor, y => y.Investor.AppUser, y => y.AnnouncementCategory, y => y.Hits, y => y.Reports);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Announcement>().AsQueryable();
            }
        }

        public IQueryable<Announcement> GetAllIncludingByInvestorIdAsync(int? investorId)
        {
            try
            {
                if (investorId == null)
                    throw new ArgumentNullException(nameof(investorId), "investorId was null");

                var data = _announcementRepository.GetAllIncludeById(investorId, "InvestorId", new Expression<Func<Announcement, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Company, y => y.Investor, y => y.Investor.AppUser, y => y.AnnouncementCategory, y => y.Hits, y => y.Reports);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Announcement>().AsQueryable();
            }
        }

        public IQueryable<Announcement> GetAllIncludingForAdminAsync()
        {
            try
            {
                var data = _announcementRepository.GetAllInclude(new Expression<Func<Announcement, bool>>[]
                {

                }, null, y => y.Company, y => y.Investor, y => y.Investor.AppUser, y => y.AnnouncementCategory, y => y.Hits, y => y.Reports);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Announcement>().AsQueryable();
            }
        }

        public IQueryable<Announcement> GetAllIncludingLastAnnouncementForIndex()
        {
            try
            {
                //var today = DateTime.Today;
                //var tomorrow = today.AddDays(1);

                return _announcementRepository.GetAllInclude(new Expression<Func<Announcement, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                    //i => i.CreatedDate >= today && i.CreatedDate < tomorrow
                }, null, y => y.Company, y => y.Investor, y => y.Investor.AppUser, y => y.AnnouncementCategory, y => y.Hits).OrderByDescending(i => Guid.NewGuid()).Take(25);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Announcement>().AsQueryable();
            }
        }

        public IQueryable<Announcement> GetAllIncludingLastAnnouncementForTimeline()
        {
            try
            {
                //var today = DateTime.Today;
                //var tomorrow = today.AddDays(1);

                return _announcementRepository.GetAllInclude(new Expression<Func<Announcement, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                    //i => i.CreatedDate >= today && i.CreatedDate < tomorrow
                }, null, y => y.Company, y => y.Investor, y => y.Investor.AppUser, y => y.AnnouncementCategory, y => y.Hits).OrderByDescending(i => Guid.NewGuid()).Take(15);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Announcement>().AsQueryable();
            }
        }
        public async Task<Announcement?> GetBySlugAsync(string slug)
        {
            try
            {
                var match = await _announcementRepository.GetBySlugAsync(slug);
                if (match == null)
                {
                    return null;
                }
                return await GetByIdAsync(match.Id);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }
        public async Task<Announcement> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                return await _announcementRepository.GetIncludeAsync(i => i.Id == id, y => y.Company, y => y.Investor, y => y.Investor.AppUser, y => y.AnnouncementCategory, y => y.Hits, y => y.Reports);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _announcementRepository.SetActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _announcementRepository.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _announcementRepository.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _announcementRepository.SetNotDeletedAsync(id);
            return result;
        }

        public async Task<bool> UpdateCompanyAnnouncementAsync(string title, string? subtitle, string content, int announcementCategoryId, int? companyId, IFormFile? image, int id)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                if (image != null && image.Length > 0)
                {
                    ArgumentNullException.ThrowIfNull(_htmlSanitizer, nameof(_htmlSanitizer));

                    ServiceImageHelper.ImageValidation(image);
                    try
                    {
                        string savedFileName = await ServiceImageHelper.AnnouncememntImageResize(image);

                        string safeContent = _htmlSanitizer.Sanitize(content ?? string.Empty);
                        var entity = new Announcement
                        {
                            Title = title,
                            Subtitle = subtitle,
                            Content = safeContent,
                            ImageUrl = savedFileName,
                            AnnouncementCategoryId = announcementCategoryId,
                            CompanyId = companyId,
                            Id = id,
                            UpdatedDate = DateTime.UtcNow
                        };

                        var results = await _announcementRepository.UpdateAsync(entity);
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
                    ArgumentNullException.ThrowIfNull(_htmlSanitizer, nameof(_htmlSanitizer));
                    string safeContent = _htmlSanitizer.Sanitize(content ?? string.Empty);
                    var entity = new Announcement
                    {
                        Title = title,
                        Subtitle = subtitle,
                        Content = safeContent,
                        AnnouncementCategoryId = announcementCategoryId,
                        CompanyId = companyId,
                        Id = id,
                        UpdatedDate = DateTime.UtcNow
                    };
                    if (entity != null)
                    {
                        var result = await _announcementRepository.UpdateAsync(entity);
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

        public async Task<bool> UpdateInvestorAnnouncementAsync(string title, string? subtitle, string content, int announcementCategoryId, int? investorId, IFormFile? image, int id)
        {
            try
            {
                if (investorId == null)
                    throw new ArgumentNullException(nameof(investorId), "investorId was null");

                if (image != null && image.Length > 0)
                {
                    ArgumentNullException.ThrowIfNull(_htmlSanitizer, nameof(_htmlSanitizer));

                    ServiceImageHelper.ImageValidation(image);
                    try
                    {
                        string savedFileName = await ServiceImageHelper.AnnouncememntImageResize(image);

                        string safeContent = _htmlSanitizer.Sanitize(content ?? string.Empty);
                        var entity = new Announcement
                        {
                            Title = title,
                            Subtitle = subtitle,
                            Content = safeContent,
                            AnnouncementCategoryId = announcementCategoryId,
                            InvestorId = investorId,
                            ImageUrl = savedFileName,
                            Id = id,
                            UpdatedDate = DateTime.UtcNow
                        };

                        var results = await _announcementRepository.UpdateAsync(entity);
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
                    string safeContent = _htmlSanitizer.Sanitize(content ?? string.Empty);
                    var entity = new Announcement
                    {
                        Title = title,
                        Subtitle = subtitle,
                        Content = safeContent,
                        AnnouncementCategoryId = announcementCategoryId,
                        InvestorId = investorId,
                        Id = id,
                        UpdatedDate = DateTime.UtcNow
                    };
                    if (entity != null)
                    {
                        var result = await _announcementRepository.UpdateAsync(entity);
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

        public async Task<IEnumerable<Announcement>> GetAllForSignalRAsync()
        {
            try
            {
                var data = await _announcementRepository.GetAllIncludeAsync(new Expression<Func<Announcement, bool>>[]
                {

                }, null, y => y.Company, y => y.Investor, y => y.Investor.AppUser, y => y.AnnouncementCategory, y => y.Hits, y => y.Reports);
                return data.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Announcement>();
            }
        }
    }
}