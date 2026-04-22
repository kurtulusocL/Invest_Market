using System.Linq.Expressions;
using System.Security.Claims;
using Investigation.Business.Services.Abstract;
using Investigation.DataAccess.Abstract;
using Investigation.Domain.Entities;
using Investigation.Domain.Entities.UserEntities;
using Microsoft.AspNetCore.Http;

namespace Investigation.Business.Services.Concrete
{
    public class SavedContentManager : ISavedContentService
    {
        readonly ISavedContentRepository _savedContentRepository;
        readonly IBlogRepository _blogRepository;
        readonly ISectorNewsRepository _sectorNewsRepository;
        readonly ICompanyRepository _companyRepository;
        readonly IInvestorRepository _investorRepository;
        readonly IPostRepository _postRepository;
        readonly ISurveyRepository _surveyRepository;
        readonly IHttpContextAccessor _httpContextAccessor;
        public SavedContentManager(ISavedContentRepository savedContentRepository, IHttpContextAccessor httpContextAccessor, IBlogRepository blogRepository, ISectorNewsRepository sectorNewsRepository, ICompanyRepository companyRepository, IInvestorRepository investorRepository, IPostRepository postRepository, ISurveyRepository surveyRepository)
        {
            _savedContentRepository = savedContentRepository;
            _httpContextAccessor = httpContextAccessor;
            _blogRepository = blogRepository;
            _sectorNewsRepository = sectorNewsRepository;
            _companyRepository = companyRepository;
            _investorRepository = investorRepository;
            _postRepository = postRepository;
            _surveyRepository = surveyRepository;
        }

        public async Task<bool> DeleteAllByIdAsync(List<int> ids)
        {
            try
            {
                if (ids == null || !ids.Any())
                    throw new ArgumentNullException(nameof(ids), "id list was null or empty");

                var result = await _savedContentRepository.DeleteByIdsAsync(ids.Cast<object>());
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while bulk deleting entities.", ex);
            }
        }

        public async Task<bool> DeleteAsync(SavedContent entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var data = await _savedContentRepository.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _savedContentRepository.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public IQueryable<SavedContent> GetAllIncludingAsync()
        {
            try
            {
                var data = _savedContentRepository.GetAllInclude(new Expression<Func<SavedContent, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Blog, y => y.SectorNews, y => y.Company, y => y.Post, y => y.Investor, y => y.Survey, y => y.AppUser);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<SavedContent>().AsQueryable();
            }
        }

        public IQueryable<SavedContent> GetAllIncludingForAdminAsync()
        {
            try
            {
                var data = _savedContentRepository.GetAllInclude(new Expression<Func<SavedContent, bool>>[]
                {

                }, null, y => y.Blog, y => y.SectorNews, y => y.Company, y => y.Post, y => y.Investor, y => y.Survey, y => y.AppUser);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<SavedContent>().AsQueryable();
            }
        }

        public IQueryable<SavedContent> GetAllIncludingByDisSavedAsync()
        {
            try
            {
                var data = _savedContentRepository.GetAllInclude(new Expression<Func<SavedContent, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsSaved==false &&i.DisSaveDate!=null
                }, null, y => y.Blog, y => y.SectorNews, y => y.Company, y => y.Post, y => y.Investor, y => y.Survey, y => y.AppUser);
                return data.OrderByDescending(i => i.DisSaveDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<SavedContent>().AsQueryable();
            }
        }

        public IQueryable<SavedContent> GetAllIncludingByDisSavedByUserIdAsync(string appUserId)
        {
            try
            {
                if (appUserId == null)
                    throw new ArgumentNullException(nameof(appUserId), "appUserId was null");

                var data = _savedContentRepository.GetAllIncludeById(appUserId, "AppUserId", new Expression<Func<SavedContent, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsSaved==false &&i.DisSaveDate!=null
                }, y => y.Blog, y => y.SectorNews, y => y.Company, y => y.Post, y => y.Investor, y => y.Survey, y => y.AppUser);
                return data.OrderByDescending(i => i.DisSaveDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<SavedContent>().AsQueryable();
            }
        }

        public IQueryable<SavedContent> GetAllIncludingByUserIdAsync(string appUserId)
        {
            try
            {
                if (appUserId == null)
                    throw new ArgumentNullException(nameof(appUserId), "appUserId was null");

                var data = _savedContentRepository.GetAllIncludeById(appUserId, "AppUserId", new Expression<Func<SavedContent, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Blog, y => y.SectorNews, y => y.Company, y => y.Post, y => y.Investor, y => y.Survey, y => y.AppUser);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<SavedContent>().AsQueryable();
            }
        }

        public IQueryable<SavedContent> GetAllIncludingNotSavedByBlogIdAsync(int? blogId)
        {
            try
            {
                if (blogId == null)
                    throw new ArgumentNullException(nameof(blogId), "blogId was null");

                var data = _savedContentRepository.GetAllIncludeById(blogId, "BlogId", new Expression<Func<SavedContent, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsSaved==false &&i.DisSaveDate!=null
                }, y => y.Blog, y => y.SectorNews, y => y.Company, y => y.Post, y => y.Investor, y => y.Survey, y => y.AppUser);
                return data.OrderByDescending(i => i.DisSaveDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<SavedContent>().AsQueryable();
            }
        }

        public IQueryable<SavedContent> GetAllIncludingNotSavedByCompanyIdAsync(int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                var data = _savedContentRepository.GetAllIncludeById(companyId, "CompanyId", new Expression<Func<SavedContent, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsSaved==false &&i.DisSaveDate!=null
                }, y => y.Blog, y => y.SectorNews, y => y.Company, y => y.Post, y => y.Investor, y => y.Survey, y => y.AppUser);
                return data.OrderByDescending(i => i.DisSaveDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<SavedContent>().AsQueryable();
            }
        }

        public IQueryable<SavedContent> GetAllIncludingNotSavedByInvestorIdAsync(int? investorId)
        {
            try
            {
                if (investorId == null)
                    throw new ArgumentNullException(nameof(investorId), "investorId was null");

                var data = _savedContentRepository.GetAllIncludeById(investorId, "InvestorId", new Expression<Func<SavedContent, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsSaved==false &&i.DisSaveDate!=null
                }, y => y.Blog, y => y.SectorNews, y => y.Company, y => y.Post, y => y.Investor, y => y.Survey, y => y.AppUser);
                return data.OrderByDescending(i => i.DisSaveDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<SavedContent>().AsQueryable();
            }
        }

        public IQueryable<SavedContent> GetAllIncludingNotSavedByPostIdAsync(int? postId)
        {
            try
            {
                if (postId == null)
                    throw new ArgumentNullException(nameof(postId), "postId was null");

                var data = _savedContentRepository.GetAllIncludeById(postId, "PostId", new Expression<Func<SavedContent, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsSaved==false &&i.DisSaveDate!=null
                }, y => y.Blog, y => y.SectorNews, y => y.Company, y => y.Post, y => y.Investor, y => y.Survey, y => y.AppUser);
                return data.OrderByDescending(i => i.DisSaveDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<SavedContent>().AsQueryable();
            }
        }

        public IQueryable<SavedContent> GetAllIncludingNotSavedBySectorNewsIdAsync(int? sectorNewsId)
        {
            try
            {
                if (sectorNewsId == null)
                    throw new ArgumentNullException(nameof(sectorNewsId), "sectorNewsId was null");

                var data = _savedContentRepository.GetAllIncludeById(sectorNewsId, "SectorNewsId", new Expression<Func<SavedContent, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsSaved==false &&i.DisSaveDate!=null
                }, y => y.Blog, y => y.SectorNews, y => y.Company, y => y.Post, y => y.Investor, y => y.Survey, y => y.AppUser);
                return data.OrderByDescending(i => i.DisSaveDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<SavedContent>().AsQueryable();
            }
        }

        public IQueryable<SavedContent> GetAllIncludingNotSavedBySurveyIdAsync(int? surveyId)
        {
            try
            {
                if (surveyId == null)
                    throw new ArgumentNullException(nameof(surveyId), "surveyId was null");

                var data = _savedContentRepository.GetAllIncludeById(surveyId, "SurveyId", new Expression<Func<SavedContent, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsSaved==false &&i.DisSaveDate!=null
                }, y => y.Blog, y => y.SectorNews, y => y.Company, y => y.Post, y => y.Investor, y => y.Survey, y => y.AppUser);
                return data.OrderByDescending(i => i.DisSaveDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<SavedContent>().AsQueryable();
            }
        }

        public IQueryable<SavedContent> GetAllIncludingSavedByBlogIdAsync(int? blogId)
        {
            try
            {
                if (blogId == null)
                    throw new ArgumentNullException(nameof(blogId), "blogId was null");

                var data = _savedContentRepository.GetAllIncludeById(blogId, "BlogId", new Expression<Func<SavedContent, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsSaved==true
                }, y => y.Blog, y => y.SectorNews, y => y.Company, y => y.Post, y => y.Investor, y => y.Survey, y => y.AppUser);
                return data.OrderByDescending(i => i.SaveDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<SavedContent>().AsQueryable();
            }
        }

        public IQueryable<SavedContent> GetAllIncludingSavedByCompanyIdAsync(int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                var data = _savedContentRepository.GetAllIncludeById(companyId, "CompanyId", new Expression<Func<SavedContent, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsSaved == true
                }, y => y.Blog, y => y.SectorNews, y => y.Company, y => y.Post, y => y.Investor, y => y.Survey, y => y.AppUser);
                return data.OrderByDescending(i => i.SaveDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<SavedContent>().AsQueryable();
            }
        }

        public IQueryable<SavedContent> GetAllIncludingSavedByInvestorIdAsync(int? investorId)
        {
            try
            {
                if (investorId == null)
                    throw new ArgumentNullException(nameof(investorId), "investorId was null");

                var data = _savedContentRepository.GetAllIncludeById(investorId, "InvestorId", new Expression<Func<SavedContent, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsSaved == true
                }, y => y.Blog, y => y.SectorNews, y => y.Company, y => y.Post, y => y.Investor, y => y.Survey, y => y.AppUser);
                return data.OrderByDescending(i => i.SaveDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<SavedContent>().AsQueryable();
            }
        }

        public IQueryable<SavedContent> GetAllIncludingSavedByPostIdAsync(int? postId)
        {
            try
            {
                if (postId == null)
                    throw new ArgumentNullException(nameof(postId), "postId was null");

                var data = _savedContentRepository.GetAllIncludeById(postId, "PostId", new Expression<Func<SavedContent, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsSaved == true
                }, y => y.Blog, y => y.SectorNews, y => y.Company, y => y.Post, y => y.Investor, y => y.Survey, y => y.AppUser);
                return data.OrderByDescending(i => i.SaveDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<SavedContent>().AsQueryable();
            }
        }

        public IQueryable<SavedContent> GetAllIncludingSavedBySectorNewsIdAsync(int? sectorNewsId)
        {
            try
            {
                if (sectorNewsId == null)
                    throw new ArgumentNullException(nameof(sectorNewsId), "sectorNewsId was null");

                var data = _savedContentRepository.GetAllIncludeById(sectorNewsId, "SectorNewsId", new Expression<Func<SavedContent, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsSaved == true
                }, y => y.Blog, y => y.SectorNews, y => y.Company, y => y.Post, y => y.Investor, y => y.Survey, y => y.AppUser);
                return data.OrderByDescending(i => i.SaveDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<SavedContent>().AsQueryable();
            }
        }

        public IQueryable<SavedContent> GetAllIncludingSavedBySurveyIdAsync(int? surveyId)
        {
            try
            {
                if (surveyId == null)
                    throw new ArgumentNullException(nameof(surveyId), "surveyId was null");

                var data = _savedContentRepository.GetAllIncludeById(surveyId, "SurveyId", new Expression<Func<SavedContent, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsSaved == true
                }, y => y.Blog, y => y.SectorNews, y => y.Company, y => y.Post, y => y.Investor, y => y.Survey, y => y.AppUser);
                return data.OrderByDescending(i => i.SaveDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<SavedContent>().AsQueryable();
            }
        }

        public async Task<SavedContent> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                return await _savedContentRepository.GetIncludeAsync(i => i.Id == id, y => y.Blog, y => y.SectorNews, y => y.Company, y => y.Post, y => y.Investor, y => y.Survey, y => y.AppUser);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SaveBlogAsync(bool isSaved, int? blogId, string appUserId)
        {
            try
            {
                var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("UserId")?.Value
                           ?? _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var sessionUserId = _httpContextAccessor.HttpContext.Session.GetString("userId");
                appUserId = userIdClaim ?? sessionUserId;

                if (string.IsNullOrEmpty(appUserId))
                {
                    throw new UnauthorizedAccessException("User not authenticated. UserId not found in claims or session.");
                }

                if (blogId == null)
                    throw new ArgumentNullException(nameof(blogId), "blogId was null");

                var savedBlog = await _blogRepository.GetAsync(i => i.Id == blogId);
                //_context.Set<Blog>().Where(i => i.Id == blogId).FirstOrDefaultAsync();
                if (savedBlog != null)
                {
                    SavedContent model = new SavedContent
                    {
                        BlogId = savedBlog.Id,
                        AppUserId = appUserId,
                        IsSaved = true,
                        SaveDate = DateTime.Now.ToLocalTime()
                    };
                    await _savedContentRepository.AddAsync(model);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while saving the entity.", ex);
            }
        }

        public async Task<bool> NotSaveBlogAsync(bool isSaved, int? blogId, string appUserId)
        {
            try
            {
                var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("UserId")?.Value
                           ?? _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var sessionUserId = _httpContextAccessor.HttpContext.Session.GetString("userId");
                appUserId = userIdClaim ?? sessionUserId;

                if (string.IsNullOrEmpty(appUserId))
                {
                    throw new UnauthorizedAccessException("User not authenticated. UserId not found in claims or session.");
                }

                if (blogId == null)
                    throw new ArgumentNullException(nameof(blogId), "blogId was null");

                var notSavedBlog = await _blogRepository.GetAsync(i => i.Id == blogId);
                if (notSavedBlog != null)
                {
                    SavedContent model = new SavedContent
                    {
                        BlogId = notSavedBlog.Id,
                        AppUserId = appUserId,
                        IsSaved = false,
                        DisSaveDate = DateTime.Now.ToLocalTime()
                    };
                    model.UpdatedDate = DateTime.UtcNow;
                    await _savedContentRepository.UpdateAsync(model);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while Dissaving the entity.", ex);
            }
        }

        public async Task<bool> SaveCompanyAsync(bool isSaved, int? companyId, string appUserId)
        {
            try
            {
                var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("UserId")?.Value
                           ?? _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var sessionUserId = _httpContextAccessor.HttpContext.Session.GetString("userId");
                appUserId = userIdClaim ?? sessionUserId;

                if (string.IsNullOrEmpty(appUserId))
                {
                    throw new UnauthorizedAccessException("User not authenticated. UserId not found in claims or session.");
                }

                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                var savedCompany = await _companyRepository.GetAsync(i => i.Id == companyId);
                if (savedCompany != null)
                {
                    SavedContent model = new SavedContent
                    {
                        CompanyId = savedCompany.Id,
                        AppUserId = appUserId,
                        IsSaved = true,
                        SaveDate = DateTime.Now.ToLocalTime()
                    };
                    await _savedContentRepository.AddAsync(model);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while Saving the entity.", ex);
            }
        }

        public async Task<bool> NotSaveCompanyAsync(bool isSaved, int? companyId, string appUserId)
        {
            try
            {
                var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("UserId")?.Value
                           ?? _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var sessionUserId = _httpContextAccessor.HttpContext.Session.GetString("userId");
                appUserId = userIdClaim ?? sessionUserId;

                if (string.IsNullOrEmpty(appUserId))
                {
                    throw new UnauthorizedAccessException("User not authenticated. UserId not found in claims or session.");
                }

                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                var notSavedCompany = await _companyRepository.GetAsync(i => i.Id == companyId);
                if (notSavedCompany != null)
                {
                    SavedContent model = new SavedContent
                    {
                        CompanyId = notSavedCompany.Id,
                        AppUserId = appUserId,
                        IsSaved = false,
                        DisSaveDate = DateTime.Now.ToLocalTime()
                    };
                    model.UpdatedDate = DateTime.UtcNow;
                    await _savedContentRepository.UpdateAsync(model);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while Dissaving the entity.", ex);
            }
        }

        public async Task<bool> SaveInvestorAsync(bool isSaved, int? investorId, string appUserId)
        {
            try
            {
                var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("UserId")?.Value
                           ?? _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var sessionUserId = _httpContextAccessor.HttpContext.Session.GetString("userId");
                appUserId = userIdClaim ?? sessionUserId;

                if (string.IsNullOrEmpty(appUserId))
                {
                    throw new UnauthorizedAccessException("User not authenticated. UserId not found in claims or session.");
                }

                if (investorId == null)
                    throw new ArgumentNullException(nameof(investorId), "investorId was null");

                var savedInvestor = await _investorRepository.GetAsync(i => i.Id == investorId);
                if (savedInvestor != null)
                {
                    SavedContent model = new SavedContent
                    {
                        InvestorId = savedInvestor.Id,
                        AppUserId = appUserId,
                        IsSaved = true,
                        SaveDate = DateTime.Now.ToLocalTime()
                    };
                    await _savedContentRepository.AddAsync(model);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while Saving the entity.", ex);
            }
        }

        public async Task<bool> NotSaveInvestorAsync(bool isSaved, int? investorId, string appUserId)
        {
            try
            {
                var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("UserId")?.Value
                           ?? _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var sessionUserId = _httpContextAccessor.HttpContext.Session.GetString("userId");
                appUserId = userIdClaim ?? sessionUserId;

                if (string.IsNullOrEmpty(appUserId))
                {
                    throw new UnauthorizedAccessException("User not authenticated. UserId not found in claims or session.");
                }

                if (investorId == null)
                    throw new ArgumentNullException(nameof(investorId), "investorId was null");

                var notSavedInvestor = await _investorRepository.GetAsync(i => i.Id == investorId);
                if (notSavedInvestor != null)
                {
                    SavedContent model = new SavedContent
                    {
                        InvestorId = notSavedInvestor.Id,
                        AppUserId = appUserId,
                        IsSaved = false,
                        DisSaveDate = DateTime.Now.ToLocalTime()
                    };
                    model.UpdatedDate = DateTime.UtcNow;
                    await _savedContentRepository.UpdateAsync(model);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while Dissaving the entity.", ex);
            }
        }

        public async Task<bool> SavePostAsync(bool isSaved, int? postId, string appUserId)
        {
            try
            {
                var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("UserId")?.Value
                           ?? _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var sessionUserId = _httpContextAccessor.HttpContext.Session.GetString("userId");
                appUserId = userIdClaim ?? sessionUserId;

                if (string.IsNullOrEmpty(appUserId))
                {
                    throw new UnauthorizedAccessException("User not authenticated. UserId not found in claims or session.");
                }

                if (postId == null)
                    throw new ArgumentNullException(nameof(postId), "postId was null");

                var savedPost = await _postRepository.GetAsync(i => i.Id == postId);
                if (savedPost != null)
                {
                    SavedContent model = new SavedContent
                    {
                        PostId = savedPost.Id,
                        AppUserId = appUserId,
                        IsSaved = true,
                        SaveDate = DateTime.Now.ToLocalTime()
                    };
                    await _savedContentRepository.AddAsync(model);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while Saving the entity.", ex);
            }
        }

        public async Task<bool> NotSavePostAsync(bool isSaved, int? postId, string appUserId)
        {
            try
            {
                var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("UserId")?.Value
                           ?? _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var sessionUserId = _httpContextAccessor.HttpContext.Session.GetString("userId");
                appUserId = userIdClaim ?? sessionUserId;

                if (string.IsNullOrEmpty(appUserId))
                {
                    throw new UnauthorizedAccessException("User not authenticated. UserId not found in claims or session.");
                }

                if (postId == null)
                    throw new ArgumentNullException(nameof(postId), "postId was null");

                var notSavedPost = await _postRepository.GetAsync(i => i.Id == postId);
                if (notSavedPost != null)
                {
                    SavedContent model = new SavedContent
                    {
                        PostId = notSavedPost.Id,
                        AppUserId = appUserId,
                        IsSaved = false,
                        DisSaveDate = DateTime.Now.ToLocalTime()
                    };
                    model.UpdatedDate = DateTime.UtcNow;
                    await _savedContentRepository.UpdateAsync(model);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while Dissaving the entity.", ex);
            }
        }

        public async Task<bool> SaveSectorNewsAsync(bool isSaved, int? sectorNewsId, string appUserId)
        {
            try
            {
                var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("UserId")?.Value
                           ?? _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var sessionUserId = _httpContextAccessor.HttpContext.Session.GetString("userId");
                appUserId = userIdClaim ?? sessionUserId;

                if (string.IsNullOrEmpty(appUserId))
                {
                    throw new UnauthorizedAccessException("User not authenticated. UserId not found in claims or session.");
                }

                if (sectorNewsId == null)
                    throw new ArgumentNullException(nameof(sectorNewsId), "sectorNewsId was null");

                var savedSectorNews = await _sectorNewsRepository.GetAsync(i => i.Id == sectorNewsId);
                if (savedSectorNews != null)
                {
                    SavedContent model = new SavedContent
                    {
                        SectorNewsId = savedSectorNews.Id,
                        AppUserId = appUserId,
                        IsSaved = true,
                        SaveDate = DateTime.Now.ToLocalTime()
                    };
                    await _savedContentRepository.AddAsync(model);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while Saving the entity.", ex);
            }
        }

        public async Task<bool> NotSaveSectorNewsAsync(bool isSaved, int? sectorNewsId, string appUserId)
        {
            try
            {
                var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("UserId")?.Value
                           ?? _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var sessionUserId = _httpContextAccessor.HttpContext.Session.GetString("userId");
                appUserId = userIdClaim ?? sessionUserId;

                if (string.IsNullOrEmpty(appUserId))
                {
                    throw new UnauthorizedAccessException("User not authenticated. UserId not found in claims or session.");
                }

                if (sectorNewsId == null)
                    throw new ArgumentNullException(nameof(sectorNewsId), "sectorNewsId was null");

                var notSavedSectorNews = await _sectorNewsRepository.GetAsync(i => i.Id == sectorNewsId);
                if (notSavedSectorNews != null)
                {
                    SavedContent model = new SavedContent
                    {
                        SectorNewsId = notSavedSectorNews.Id,
                        AppUserId = appUserId,
                        IsSaved = false,
                        DisSaveDate = DateTime.Now.ToLocalTime()
                    };
                    model.UpdatedDate = DateTime.UtcNow;
                    await _savedContentRepository.UpdateAsync(model);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while Dissaving the entity.", ex);
            }
        }

        public async Task<bool> SaveSurveyAsync(bool isSaved, int? surveyId, string appUserId)
        {
            try
            {
                var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("UserId")?.Value
                           ?? _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var sessionUserId = _httpContextAccessor.HttpContext.Session.GetString("userId");
                appUserId = userIdClaim ?? sessionUserId;

                if (string.IsNullOrEmpty(appUserId))
                {
                    throw new UnauthorizedAccessException("User not authenticated. UserId not found in claims or session.");
                }

                if (surveyId == null)
                    throw new ArgumentNullException(nameof(surveyId), "surveyId was null");

                var savedSurvey = await _surveyRepository.GetAsync(i => i.Id == surveyId);
                if (savedSurvey != null)
                {
                    SavedContent model = new SavedContent
                    {
                        SurveyId = savedSurvey.Id,
                        AppUserId = appUserId,
                        IsSaved = true,
                        SaveDate = DateTime.Now.ToLocalTime()
                    };
                    await _savedContentRepository.AddAsync(model);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while Saving the entity.", ex);
            }
        }

        public async Task<bool> NotSaveSurveyAsync(bool isSaved, int? surveyId, string appUserId)
        {
            try
            {
                var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("UserId")?.Value
                            ?? _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var sessionUserId = _httpContextAccessor.HttpContext.Session.GetString("userId");
                appUserId = userIdClaim ?? sessionUserId;

                if (string.IsNullOrEmpty(appUserId))
                {
                    throw new UnauthorizedAccessException("User not authenticated. UserId not found in claims or session.");
                }

                if (surveyId == null)
                    throw new ArgumentNullException(nameof(surveyId), "surveyId was null");

                var notSavedSurvey = await _surveyRepository.GetAsync(i => i.Id == surveyId);
                if (notSavedSurvey != null)
                {
                    SavedContent model = new SavedContent
                    {
                        SurveyId = notSavedSurvey.Id,
                        AppUserId = appUserId,
                        IsSaved = false,
                        DisSaveDate = DateTime.Now.ToLocalTime()
                    };
                    model.UpdatedDate = DateTime.UtcNow;
                    await _savedContentRepository.UpdateAsync(model);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while Dissaving the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _savedContentRepository.SetActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _savedContentRepository.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _savedContentRepository.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _savedContentRepository.SetNotDeletedAsync(id);
            return result;
        }

        public int SavedContentCounter()
        {
            return _savedContentRepository.SavedContentCounter();
        }

        public IQueryable<SavedContent> GetAllIncludingSavedContentsForUserByUserIdAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var data = _savedContentRepository.GetAllIncludeById(userId, "AppUserId", new Expression<Func<SavedContent, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsSaved==true
                }, y => y.Blog, y => y.SectorNews, y => y.Company, y => y.Post, y => y.Investor, y => y.Investor.AppUser, y => y.Survey);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<SavedContent>().AsQueryable();
            }
        }

        public IQueryable<SavedContent> GetAllIncludingSavedContentsForSavedContentOwnerByUserIdAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var currentUserId = _httpContextAccessor.HttpContext.Session.GetString("userId");
                if (currentUserId == null)
                    throw new ArgumentNullException(nameof(currentUserId), "currentUserId was null");

                var savedContents = _savedContentRepository.GetAllInclude(new Expression<Func<SavedContent, bool>>[]
                {
                    i => i.IsActive == true,
                    i => i.IsDeleted == false,
                    i => (i.BlogId != null && i.Blog.AppUserId == userId&& i.Blog.AppUserId == currentUserId)
                    || (i.CompanyId != null && i.Company.AppUserId == userId&& i.Company.AppUserId == currentUserId)
                    || (i.InvestorId != null && i.Investor.AppUserId == userId&& i.Investor.AppUserId == currentUserId)
                    || (i.Post != null && i.Post.AppUserId == userId&& i.Post.AppUserId == currentUserId)
                    || (i.SurveyId != null && i.Survey.AppUserId == userId&& i.Survey.AppUserId == currentUserId)
                }, y => y.Company, y => y.Blog, y => y.Post, y => y.AppUser, y => y.Investor, y => y.Investor.AppUser, y => y.Survey);

                if (savedContents == null || !savedContents.Any())
                    return Enumerable.Empty<SavedContent>().AsQueryable();

                var uniqueComments = savedContents.AsEnumerable().GroupBy(c => new { c.BlogId, c.CompanyId, c.InvestorId, c.PostId, c.SurveyId }).Select(g => g.OrderByDescending(c => c.CreatedDate).First()).OrderByDescending(c => c.CreatedDate);
                return uniqueComments.AsEnumerable().AsQueryable();
            }
            catch (Exception)
            {
                return Enumerable.Empty<SavedContent>().AsQueryable();
            }
        }

        public IQueryable<AppUser> GetAllIncludingSavedContentsPeopleForOwnerByContentIdAsync(int? blogId = null, int? postId = null, int? companyId = null, int? investorId = null, int? surveyId = null)
        {
            try
            {
                if (blogId == null && postId == null && companyId == null && investorId == null && surveyId == null)
                    throw new ArgumentException("At least one content ID must be provided.", "contentId");

                var savedContents = _savedContentRepository.GetAllInclude(new Expression<Func<SavedContent, bool>>[]
                {
                    i => i.IsActive == true,
                    i => i.IsDeleted == false,
                    i=>i.IsSaved==true,
                    i => (blogId != null && i.BlogId == blogId)
                    || (postId != null && i.PostId == postId)
                    || (companyId != null && i.CompanyId == companyId)
                    || (investorId != null && i.InvestorId == investorId)
                    || (surveyId != null && i.SurveyId == surveyId)
                }, y => y.AppUser);

                if (savedContents == null || !savedContents.Any())
                    return Enumerable.Empty<AppUser>().AsQueryable();

                var users = savedContents.AsEnumerable().OrderByDescending(i => i.SaveDate).Select(x => x.AppUser).Distinct();
                return users.AsQueryable();
            }
            catch (Exception)
            {
                return Enumerable.Empty<AppUser>().AsQueryable();
            }
        }

        public IQueryable<SavedContent> GetAllIncludingCompanySavedsPeopleByCompanyId(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var hits = _savedContentRepository.GetAllInclude(new Expression<Func<SavedContent, bool>>[] {
                    i => i.IsActive == true,
                    i => i.IsDeleted == false,
                    i => i.IsSaved == true,
                    i => i.CompanyId != null && i.Company.AppUserId == userId
                }, y => y.Company, y => y.Company.AppUser, y => y.AppUser)
                .AsEnumerable().OrderByDescending(i => i.CreatedDate).ToList();

                if (!hits.Any())
                    return Enumerable.Empty<SavedContent>().AsQueryable();

                var uniqueLikes = hits
                    .Where(h => h.AppUserId != null)
                    .GroupBy(c => new { c.CompanyId, c.AppUserId })
                    .Select(g => g.OrderByDescending(c => c.CreatedDate).First())
                    .OrderByDescending(c => c.CreatedDate);

                return uniqueLikes.AsQueryable();
            }
            catch (Exception)
            {
                return Enumerable.Empty<SavedContent>().AsQueryable();
            }
        }

        public async Task<IEnumerable<SavedContent>> GetAllForSignalRAsync()
        {
            try
            {
                var data = await _savedContentRepository.GetAllIncludeAsync(new Expression<Func<SavedContent, bool>>[]
                {

                }, null, y => y.Blog, y => y.SectorNews, y => y.Company, y => y.Post, y => y.Investor, y => y.Survey, y => y.AppUser);
                return data.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<SavedContent>();
            }
        }
    }
}
