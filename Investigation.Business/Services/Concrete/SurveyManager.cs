using System.Linq.Expressions;
using System.Security.Claims;
using Ganss.Xss;
using Investigation.Business.Services.Abstract;
using Investigation.DataAccess.Abstract;
using Investigation.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Investigation.Business.Services.Concrete
{
    public class SurveyManager : ISurveyService
    {
        readonly ISurveyRepository _surveyRepository;
        readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHtmlSanitizer _htmlSanitizer;
        public SurveyManager(ISurveyRepository surveyRepository, IHttpContextAccessor httpContextAccessor, IHtmlSanitizer htmlSanitizer)
        {
            _surveyRepository = surveyRepository;
            _httpContextAccessor = httpContextAccessor;
            _htmlSanitizer = htmlSanitizer;
        }

        public async Task<bool> CreateCompanySurveyAsync(string title, string desc, DateTime startDate, DateTime closedDate, int? companyId, string appUserId)
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

                ArgumentNullException.ThrowIfNull(_htmlSanitizer, nameof(_htmlSanitizer));
                string safeDesc = _htmlSanitizer.Sanitize(desc ?? string.Empty);
                var entity = new Survey
                {
                    Title = title,
                    Desc = safeDesc,
                    StartDate = startDate,
                    ClosedDate = closedDate,
                    CompanyId = companyId,
                    AppUserId = appUserId
                };
                if (entity != null)
                {
                    var result = await _surveyRepository.AddAsync(entity);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> CreateInvestorSurveyAsync(string title, string desc, DateTime startDate, DateTime closedDate, int? investorId, string appUserId)
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

                ArgumentNullException.ThrowIfNull(_htmlSanitizer, nameof(_htmlSanitizer));
                string safeDesc = _htmlSanitizer.Sanitize(desc ?? string.Empty);
                var entity = new Survey
                {
                    Title = title,
                    Desc = safeDesc,
                    StartDate = startDate,
                    ClosedDate = closedDate,
                    InvestorId = investorId,
                    AppUserId = appUserId
                };
                if (entity != null)
                {
                    var result = await _surveyRepository.AddAsync(entity);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> DeleteAllByIdAsync(List<int> ids)
        {
            try
            {
                if (ids == null || !ids.Any())
                    throw new ArgumentNullException(nameof(ids), "id list was null or empty");

                var result = await _surveyRepository.DeleteByIdsAsync(ids.Cast<object>());
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while bulk deleting entities.", ex);
            }
        }

        public async Task<bool> DeleteAsync(Survey entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var data = await _surveyRepository.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _surveyRepository.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public IQueryable<Survey> GetAllForSitemap()
        {
            try
            {
                return _surveyRepository.GetAll(i => i.IsActive == true && i.IsDeleted == false).OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Survey>().AsQueryable();
            }
        }

        public IQueryable<Survey> GetAllIncludingAsync()
        {
            try
            {
                var data = _surveyRepository.GetAllInclude(new Expression<Func<Survey, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.AppUser, y => y.Company, y => y.Investor, y => y.Hits, y => y.Likes, y => y.Reports, y => y.SurveyAnalytics, y => y.SurveyQuestions, y => y.SurveyResponses, y => y.SavedContents);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Survey>().AsQueryable();
            }
        }

        public IQueryable<Survey> GetAllIncludingByCloseDateAsync()
        {
            try
            {
                var data = _surveyRepository.GetAllInclude(new Expression<Func<Survey, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.AppUser, y => y.Company, y => y.Investor, y => y.Hits, y => y.Likes, y => y.Reports, y => y.SurveyAnalytics, y => y.SurveyQuestions, y => y.SurveyResponses, y => y.SavedContents);
                return data.OrderByDescending(i => i.ClosedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Survey>().AsQueryable();
            }
        }

        public IQueryable<Survey> GetAllIncludingByCompanyIdAsync(int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                var data = _surveyRepository.GetAllIncludeById(companyId, "CompanyId", new Expression<Func<Survey, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.AppUser, y => y.Company, y => y.Investor, y => y.Hits, y => y.Likes, y => y.Reports, y => y.SurveyAnalytics, y => y.SurveyQuestions, y => y.SurveyResponses, y => y.SavedContents);
                return data.OrderByDescending(i => i.StartDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Survey>().AsQueryable();
            }
        }

        public IQueryable<Survey> GetAllIncludingByInvestorIdAsync(int? investorId)
        {
            try
            {
                if (investorId == null)
                    throw new ArgumentNullException(nameof(investorId), "investorId was null");

                var data = _surveyRepository.GetAllIncludeById(investorId, "InvestorId", new Expression<Func<Survey, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.AppUser, y => y.Company, y => y.Investor, y => y.Hits, y => y.Likes, y => y.Reports, y => y.SurveyAnalytics, y => y.SurveyQuestions, y => y.SurveyResponses, y => y.SavedContents);
                return data.OrderByDescending(i => i.StartDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Survey>().AsQueryable();
            }
        }

        public IQueryable<Survey> GetAllIncludingByOfflineAsync()
        {
            try
            {
                var data = _surveyRepository.GetAllInclude(new Expression<Func<Survey, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsOnline==false
                }, null, y => y.AppUser, y => y.Company, y => y.Investor, y => y.Hits, y => y.Likes, y => y.Reports, y => y.SurveyAnalytics, y => y.SurveyQuestions, y => y.SurveyResponses, y => y.SavedContents);
                return data.OrderByDescending(i => i.ClosedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Survey>().AsQueryable();
            }
        }

        public IQueryable<Survey> GetAllIncludingByOnlineAsync()
        {
            try
            {
                var data = _surveyRepository.GetAllInclude(new Expression<Func<Survey, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsOnline==true
                }, null, y => y.AppUser, y => y.Company, y => y.Investor, y => y.Investor.AppUser, y => y.Hits, y => y.Likes, y => y.Reports, y => y.SurveyAnalytics, y => y.SurveyQuestions, y => y.SurveyResponses, y => y.SavedContents);
                return data.OrderByDescending(i => i.StartDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Survey>().AsQueryable();
            }
        }

        public IQueryable<Survey> GetAllIncludingByStartDateAsync()
        {
            try
            {
                var data = _surveyRepository.GetAllInclude(new Expression<Func<Survey, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.AppUser, y => y.Company, y => y.Investor, y => y.Hits, y => y.Likes, y => y.Reports, y => y.SurveyAnalytics, y => y.SurveyQuestions, y => y.SurveyResponses, y => y.SavedContents);
                return data.OrderByDescending(i => i.StartDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Survey>().AsQueryable();
            }
        }

        public IQueryable<Survey> GetAllIncludingByUserIdAsync(string appUserId)
        {
            try
            {
                if (appUserId == null)
                    throw new ArgumentNullException(nameof(appUserId), "appUserId was null");

                var data = _surveyRepository.GetAllIncludeById(appUserId, "AppUserId", new Expression<Func<Survey, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.AppUser, y => y.Company, y => y.Investor, y => y.Investor.AppUser, y => y.Hits, y => y.Likes, y => y.Reports, y => y.SurveyAnalytics, y => y.SurveyQuestions, y => y.SurveyResponses, y => y.SavedContents);
                return data.OrderByDescending(i => i.StartDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Survey>().AsQueryable();
            }
        }

        public IQueryable<Survey> GetAllIncludingForAdminAsync()
        {
            try
            {
                var data = _surveyRepository.GetAllInclude(new Expression<Func<Survey, bool>>[]
                {

                }, null, y => y.AppUser, y => y.Company, y => y.Investor, y => y.Hits, y => y.Likes, y => y.Reports, y => y.SurveyAnalytics, y => y.SurveyQuestions, y => y.SurveyResponses, y => y.SavedContents);
                return data.OrderByDescending(i => i.ClosedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Survey>().AsQueryable();
            }
        }

        public IQueryable<Survey> GetAllIncludingLastSurveyForIndex()
        {
            try
            {
                //var today = DateTime.Today;
                //var tomorrow = today.AddDays(1);

                return _surveyRepository.GetAllInclude(new Expression<Func<Survey, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsOnline==true
                    //i => i.CreatedDate >= today && i.CreatedDate < tomorrow
                }, null, y => y.AppUser, y => y.Company, y => y.Investor, y => y.Hits, y => y.Likes, y => y.SurveyResponses, y => y.SavedContents).OrderByDescending(i => Guid.NewGuid()).Take(35);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Survey>().AsQueryable();
            }
        }

        public IQueryable<Survey> GetAllIncludingLastSurveyForTimeline()
        {
            try
            {
                //var today = DateTime.Today;
                //var tomorrow = today.AddDays(1);

                return _surveyRepository.GetAllInclude(new Expression<Func<Survey, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsOnline==true
                    //i => i.CreatedDate >= today && i.CreatedDate < tomorrow
                }, null, y => y.AppUser, y => y.Company, y => y.Investor, y => y.Hits, y => y.Likes, y => y.SurveyResponses, y => y.SavedContents).OrderByDescending(i => Guid.NewGuid()).Take(25);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Survey>().AsQueryable();
            }
        }

        public async Task<IEnumerable<Survey>> GetAllIncludingLessPopularSurveysAsync()
        {
            var data = await _surveyRepository.GetAllIncludingLessPopularSurveysAsync();
            return data;
        }

        public IQueryable<Survey> GetAllIncludingLessResponsedSurveyAsync()
        {
            try
            {
                var data = _surveyRepository.GetAllInclude(new Expression<Func<Survey, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.SurveyResponses.Count()>=0
                }, null, y => y.AppUser, y => y.Company, y => y.Investor, y => y.Investor.AppUser, y => y.Hits, y => y.Likes, y => y.SurveyAnalytics, y => y.SurveyQuestions, y => y.SurveyResponses, y => y.SavedContents);
                return data.OrderBy(i => i.SurveyResponses.Count());
            }
            catch (Exception)
            {
                return Enumerable.Empty<Survey>().AsQueryable();
            }
        }

        public IQueryable<Survey> GetAllIncludingMostHitSurveyAsync()
        {
            try
            {
                var data = _surveyRepository.GetAllInclude(new Expression<Func<Survey, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Hits.Count()>0
                }, null, y => y.AppUser, y => y.Company, y => y.Investor, y => y.Investor.AppUser, y => y.Hits, y => y.Likes, y => y.SurveyAnalytics, y => y.SurveyQuestions, y => y.SurveyResponses, y => y.SavedContents);
                return data.OrderByDescending(i => i.Hits.Count());
            }
            catch (Exception)
            {
                return Enumerable.Empty<Survey>().AsQueryable();
            }
        }

        public IQueryable<Survey> GetAllIncludingMostLikedSurveyAsync()
        {
            try
            {
                var data = _surveyRepository.GetAllInclude(new Expression<Func<Survey, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Likes.Count()>0
                }, null, y => y.AppUser, y => y.Company, y => y.Investor, y => y.Investor.AppUser, y => y.Hits, y => y.Likes, y => y.SurveyAnalytics, y => y.SurveyQuestions, y => y.SurveyResponses, y => y.SavedContents);
                return data.OrderByDescending(i => i.Likes.Count());
            }
            catch (Exception)
            {
                return Enumerable.Empty<Survey>().AsQueryable();
            }
        }

        public async Task<IEnumerable<Survey>> GetAllIncludingMostPopularSurveysAsync()
        {
            return await _surveyRepository.GetAllIncludingMostPopularSurveysAsync();
        }

        public IQueryable<Survey> GetAllIncludingMostResponsedSurveyAsync()
        {
            try
            {
                var data = _surveyRepository.GetAllInclude(new Expression<Func<Survey, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.SurveyResponses.Count()>0
                }, null, y => y.AppUser, y => y.Company, y => y.Investor, y => y.Investor.AppUser, y => y.Hits, y => y.Likes, y => y.SurveyAnalytics, y => y.SurveyQuestions, y => y.SurveyResponses, y => y.SavedContents);
                return data.OrderByDescending(i => i.SurveyResponses.Count());
            }
            catch (Exception)
            {
                return Enumerable.Empty<Survey>().AsQueryable();
            }
        }

        public IQueryable<Survey> GetAllIncludingMostSavedSurveyAsync()
        {
            try
            {
                var data = _surveyRepository.GetAllInclude(new Expression<Func<Survey, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.SavedContents.Count()>0
                }, null, y => y.AppUser, y => y.Company, y => y.Investor, y => y.Investor.AppUser, y => y.Hits, y => y.Likes, y => y.SurveyAnalytics, y => y.SurveyQuestions, y => y.SurveyResponses, y => y.SavedContents);
                return data.OrderByDescending(i => i.SavedContents.Count());
            }
            catch (Exception)
            {
                return Enumerable.Empty<Survey>().AsQueryable();
            }
        }

        public IQueryable<Survey> GetAllIncludingOpenSurveyByCompanyIdAsync(int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                var data = _surveyRepository.GetAllIncludeById(companyId, "CompanyId", new Expression<Func<Survey, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsOnline==true
                }, y => y.AppUser, y => y.Company, y => y.Investor, y => y.Investor.AppUser, y => y.Hits, y => y.Likes, y => y.Reports, y => y.SurveyAnalytics, y => y.SurveyQuestions, y => y.SurveyResponses, y => y.SavedContents);
                return data.OrderByDescending(i => i.StartDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Survey>().AsQueryable();
            }
        }

        public IQueryable<Survey> GetAllIncludingOpenSurveyByInvestorIdAsync(int? investorId)
        {
            try
            {
                if (investorId == null)
                    throw new ArgumentNullException(nameof(investorId), "investorId was null");

                var data = _surveyRepository.GetAllIncludeById(investorId, "InvestorId", new Expression<Func<Survey, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsOnline==true
                }, y => y.AppUser, y => y.Company, y => y.Investor, y => y.Investor.AppUser, y => y.Hits, y => y.Likes, y => y.Reports, y => y.SurveyAnalytics, y => y.SurveyQuestions, y => y.SurveyResponses, y => y.SavedContents);
                return data.OrderByDescending(i => i.StartDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Survey>().AsQueryable();
            }
        }

        public IQueryable<Survey> GetAllIncludingPopularSurveys()
        {
            try
            {
                return _surveyRepository.GetAllInclude(new Expression<Func<Survey, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    (i=>i.Hits.Count()>30&&i.Likes.Count()>45&&i.Reports.Count()<20&&i.SurveyResponses.Count()>50)
                }, null, y => y.AppUser, y => y.Company, y => y.Investor, y => y.Hits, y => y.Likes, y => y.Reports, y => y.SurveyAnalytics, y => y.SurveyResponses, y => y.SavedContents).OrderByDescending(i => i.SurveyResponses.Count()).Take(8);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Survey>().AsQueryable();
            }
        }

        public IQueryable<Survey> GetAllIncludingSurveyByCompanyId(int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                return _surveyRepository.GetAllIncludeById(companyId, "CompanyId", new Expression<Func<Survey, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Hits, y => y.Likes, y => y.SurveyResponses, y => y.SavedContents).OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Survey>().AsQueryable();
            }
        }

        public IQueryable<Survey> GetAllIncludingSurveyForCompanyByCompanyIdAsync(int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                var data = _surveyRepository.GetAllIncludeById(companyId, "CompanyId", new Expression<Func<Survey, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Company.IsActive==true&&i.IsDeleted==false
                }, y => y.Company, y => y.Hits, y => y.Likes, y => y.Reports, y => y.SurveyAnalytics, y => y.SurveyQuestions, y => y.SurveyResponses, y => y.SavedContents);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Survey>().AsQueryable();
            }
        }

        public IQueryable<Survey> GetAllIncludingSurveyForInvestorByInvestorIdAsync(int? investorId)
        {
            try
            {
                if (investorId == null)
                    throw new ArgumentNullException(nameof(investorId), "investorId was null");

                var data = _surveyRepository.GetAllIncludeById(investorId, "InvestorId", new Expression<Func<Survey, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Investor, y => y.Hits, y => y.Likes, y => y.Reports, y => y.SurveyAnalytics, y => y.SurveyQuestions, y => y.SurveyResponses, y => y.SavedContents);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Survey>().AsQueryable();
            }
        }

        public IQueryable<Survey> GetAllIncludingSurveyForInvestorDetail(int? investorId)
        {
            try
            {
                if (investorId == null)
                    throw new ArgumentNullException(nameof(investorId), "investorId was null");

                //var today = DateTime.Today;
                //var twoWeeksAgo = today.AddDays(-14);

                return _surveyRepository.GetAllIncludeById(investorId, "InvestorId", new Expression<Func<Survey, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                    //i => i.CreatedDate >= twoWeeksAgo && i.CreatedDate < today.AddDays(1)
                }, y => y.Hits, y => y.Likes, y => y.SurveyResponses, y => y.SavedContents).OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Survey>().AsQueryable();
            }
        }

        public IQueryable<Survey> GetAllIncludingSurveysForPublicUser()
        {
            try
            {
                var data = _surveyRepository.GetAllInclude(new Expression<Func<Survey, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.AppUser, y => y.Company, y => y.Investor, y => y.Investor.AppUser, y => y.SurveyAnalytics, y => y.SurveyResponses);
                return data.Take(140).OrderByDescending(i => i.CreatedDate).OrderBy(i => Guid.NewGuid());
            }
            catch (Exception)
            {
                return Enumerable.Empty<Survey>().AsQueryable();
            }
        }

        public IQueryable<Survey> GetAllIncludingSurveyTodayAsync()
        {
            try
            {
                var today = DateTime.Now.Date;
                var data = _surveyRepository.GetAllInclude(new Expression<Func<Survey, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i => i.CreatedDate >= today && i.CreatedDate < today.AddDays(1)
                }, null, y => y.AppUser, y => y.Company, y => y.Investor, y => y.Investor.AppUser, y => y.Hits, y => y.Likes, y => y.SavedContents, y => y.SurveyResponses, y => y.SurveyAnalytics);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Survey>().AsQueryable();
            }
        }
        public async Task<Survey?> GetBySlugAsync(string slug)
        {
            var match = await _surveyRepository.GetBySlugAsync(slug);
            if (match == null)
            {
                return null;
            }
            return await GetByIdAsync(match.Id);
        }
        public async Task<Survey> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                return await _surveyRepository.GetIncludeAsync(i => i.Id == id, y => y.AppUser, y => y.Company, y => y.Investor, y => y.Hits, y => y.Likes, y => y.Reports, y => y.SurveyAnalytics, y => y.SurveyQuestions, y => y.SurveyResponses, y => y.SavedContents);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public Survey GetSurveyById(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                return _surveyRepository.GetInclude(i => i.Id == id, y => y.SurveyAnalytics, y => y.SurveyQuestions, y => y.SurveyResponses, y => y.SavedContents, y => y.Hits, y => y.Likes);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _surveyRepository.SetActiveAsync(id);
            return result;
        }

        public async Task<bool> SetCurrentlyOnlineSurveyAsync(int id)
        {
            var result = await _surveyRepository.SetCurrentlyOnlineSurveyAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _surveyRepository.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _surveyRepository.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _surveyRepository.SetNotDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetOfflineSurveyAsync(int id)
        {
            var result = await _surveyRepository.SetOfflineSurveyAsync(id);
            return result;
        }

        public async Task<bool> SubmitSurveyAnswersAsync(int surveyId, Dictionary<int, int> answers)
        {
            var result = await _surveyRepository.SubmitSurveyAnswersAsync(surveyId, answers);
            //await _surveyAnalyticsService.UpdateSurveyAnalyticsAsync(surveyId);
            return result;
        }

        public int SurveyCounter()
        {
            return _surveyRepository.SurveyCounter();
        }

        public async Task<bool> UpdateCompanySurveyAsync(string title, string desc, DateTime startDate, DateTime closedDate, int? companyId, string appUserId, int id)
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

                ArgumentNullException.ThrowIfNull(_htmlSanitizer, nameof(_htmlSanitizer));
                string safeDesc = _htmlSanitizer.Sanitize(desc ?? string.Empty);
                var entity = new Survey
                {
                    Title = title,
                    Desc = safeDesc,
                    StartDate = startDate,
                    ClosedDate = closedDate,
                    CompanyId = companyId,
                    AppUserId = appUserId,
                    Id = id,
                    UpdatedDate = DateTime.UtcNow
                };
                if (entity != null)
                {
                    var result = await _surveyRepository.UpdateAsync(entity);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while updating the entity.", ex);
            }
        }

        public async Task<bool> UpdateInvestorSurveyAsync(string title, string desc, DateTime startDate, DateTime closedDate, int? investorId, string appUserId, int id)
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

                ArgumentNullException.ThrowIfNull(_htmlSanitizer, nameof(_htmlSanitizer));
                string safeDesc = _htmlSanitizer.Sanitize(desc ?? string.Empty);
                var entity = new Survey
                {
                    Title = title,
                    Desc = safeDesc,
                    StartDate = startDate,
                    ClosedDate = closedDate,
                    InvestorId = investorId,
                    AppUserId = appUserId,
                    Id = id,
                    UpdatedDate = DateTime.UtcNow
                };
                if (entity != null)
                {
                    var result = await _surveyRepository.UpdateAsync(entity);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while updating the entity.", ex);
            }
        }

        public async Task<IEnumerable<Survey>> GetAllForSignalRAsync()
        {
            try
            {
                var data = await _surveyRepository.GetAllIncludeAsync(new Expression<Func<Survey, bool>>[]
                {
                    
                }, null, y => y.AppUser, y => y.Company, y => y.Investor, y => y.Hits, y => y.Likes, y => y.Reports, y => y.SurveyAnalytics, y => y.SurveyQuestions, y => y.SurveyResponses, y => y.SavedContents);
                return data.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Survey>();
            }
        }
    }
}
