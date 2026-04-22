using System.Security.Claims;
using Investigation.DataAccess.Abstract;
using Investigation.DataAccess.Concrete.EntityFramework.Context.MSSQL;
using Investigation.Domain.Entities;
using Investigation.Domain.Entities.UserEntities;
using Investigation.Shared.DataAccess.EntityFramework;
using Investigation.Shared.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Investigation.DataAccess.Concrete.EntityFramework.Repositories
{
    public class SurveyRepository : EntityRepositoryBase<Survey, ApplicationDbContext>, ISurveyRepository
    {
        readonly ApplicationDbContext _context;
        readonly IHttpContextAccessor _httpContextAccessor;
        readonly ISurveyAnalyticsRepository _surveyAnalyticsRepository;
        public SurveyRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, ISurveyAnalyticsRepository surveyAnalyticsRepository) : base(context)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _surveyAnalyticsRepository = surveyAnalyticsRepository;
        }
        public async Task<Survey?> GetBySlugAsync(string slug)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(slug) || slug.Length != 8)
                    return null;

                var candidates = await _context.Surveys.AsNoTracking().Where(n => n.IsActive && !n.IsDeleted)
                    .Select(n => new
                    {
                        n.Id,
                        n.CreatedDate
                    }).ToListAsync();

                var match = candidates.FirstOrDefault(c =>
                {
                    var computedSlug = SecureSlugHelper.Generate(c.Id, c.CreatedDate);
                    return string.Equals(computedSlug, slug, StringComparison.OrdinalIgnoreCase);
                });

                if (match == null)
                {
                    return null;
                }
                return await _context.Surveys.FirstOrDefaultAsync(n => n.Id == match.Id && n.IsActive && !n.IsDeleted);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred.", ex);
            }
        }
        public async Task<bool> SetCurrentlyOnlineSurveyAsync(int id)
        {
            try
            {
                var isOnline = await _context.Set<Survey>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (isOnline != null)
                {
                    isOnline.IsOnline = true;
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting Online the entity.", ex);
            }
        }
        public async Task<bool> SetOfflineSurveyAsync(int id)
        {
            try
            {
                var isOnline = await _context.Set<Survey>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (isOnline != null)
                {
                    isOnline.IsOnline = false;
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting Online the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            try
            {
                var data = await _context.Set<Survey>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (data == null)
                {
                    return false;
                }
                data.IsActive = true;

                var hits = await _context.Set<Hit>().Where(a => a.SurveyId == id).ToListAsync();
                foreach (var hit in hits)
                {
                    hit.IsActive = true;
                }

                var likes = await _context.Set<Like>().Where(a => a.SurveyId == id).ToListAsync();
                foreach (var like in likes)
                {
                    like.IsActive = true;
                }

                var reports = await _context.Set<Report>().Where(a => a.SurveyId == id).ToListAsync();
                foreach (var report in reports)
                {
                    report.IsActive = true;
                }

                var surveyAnalytics = await _context.Set<SurveyAnalytics>().Where(a => a.SurveyId == id).ToListAsync();
                foreach (var surveyAnalytic in surveyAnalytics)
                {
                    surveyAnalytic.IsActive = true;
                }

                var surveyQuestions = await _context.Set<SurveyQuestion>().Where(a => a.SurveyId == id).ToListAsync();
                foreach (var surveyQuestion in surveyQuestions)
                {
                    surveyQuestion.IsActive = true;
                }

                var surveyResponses = await _context.Set<SurveyResponse>().Where(a => a.SurveyId == id).ToListAsync();
                foreach (var surveyResponse in surveyResponses)
                {
                    surveyResponse.IsActive = true;
                }

                var savedContents = await _context.Set<SavedContent>().Where(a => a.SurveyId == id).ToListAsync();
                foreach (var savedContent in savedContents)
                {
                    savedContent.IsActive = true;
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting Active the entity.", ex);
            }
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            try
            {
                var data = await _context.Set<Survey>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (data == null)
                {
                    return false;
                }
                data.IsActive = false;
                data.SuspendedDate = DateTime.UtcNow;

                var hits = await _context.Set<Hit>().Where(a => a.SurveyId == id).ToListAsync();
                foreach (var hit in hits)
                {
                    hit.IsActive = false;
                    hit.SuspendedDate = DateTime.UtcNow;
                }

                var likes = await _context.Set<Like>().Where(a => a.SurveyId == id).ToListAsync();
                foreach (var like in likes)
                {
                    like.IsActive = false;
                    like.SuspendedDate = DateTime.UtcNow;
                }

                var reports = await _context.Set<Report>().Where(a => a.SurveyId == id).ToListAsync();
                foreach (var report in reports)
                {
                    report.IsActive = false;
                    report.SuspendedDate = DateTime.UtcNow;
                }

                var surveyAnalytics = await _context.Set<SurveyAnalytics>().Where(a => a.SurveyId == id).ToListAsync();
                foreach (var surveyAnalytic in surveyAnalytics)
                {
                    surveyAnalytic.IsActive = false;
                    surveyAnalytic.SuspendedDate = DateTime.UtcNow;
                }

                var surveyQuestions = await _context.Set<SurveyQuestion>().Where(a => a.SurveyId == id).ToListAsync();
                foreach (var surveyQuestion in surveyQuestions)
                {
                    surveyQuestion.IsActive = false;
                    surveyQuestion.SuspendedDate = DateTime.UtcNow;
                }

                var surveyResponses = await _context.Set<SurveyResponse>().Where(a => a.SurveyId == id).ToListAsync();
                foreach (var surveyResponse in surveyResponses)
                {
                    surveyResponse.IsActive = false;
                    surveyResponse.SuspendedDate = DateTime.UtcNow;
                }

                var savedContents = await _context.Set<SavedContent>().Where(a => a.SurveyId == id).ToListAsync();
                foreach (var savedContent in savedContents)
                {
                    savedContent.IsActive = false;
                    savedContent.SuspendedDate = DateTime.UtcNow;
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting DeActive the entity.", ex);
            }
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            try
            {
                var data = await _context.Set<Survey>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (data == null)
                {
                    return false;
                }
                data.IsDeleted = true;
                data.DeletedDate = DateTime.UtcNow;

                var hits = await _context.Set<Hit>().Where(a => a.SurveyId == id).ToListAsync();
                foreach (var hit in hits)
                {
                    hit.IsDeleted = true;
                    hit.DeletedDate = DateTime.UtcNow;
                }

                var likes = await _context.Set<Like>().Where(a => a.SurveyId == id).ToListAsync();
                foreach (var like in likes)
                {
                    like.IsDeleted = true;
                    like.DeletedDate = DateTime.UtcNow;
                }

                var reports = await _context.Set<Report>().Where(a => a.SurveyId == id).ToListAsync();
                foreach (var report in reports)
                {
                    report.IsDeleted = true;
                    report.DeletedDate = DateTime.UtcNow;
                }

                var surveyAnalytics = await _context.Set<SurveyAnalytics>().Where(a => a.SurveyId == id).ToListAsync();
                foreach (var surveyAnalytic in surveyAnalytics)
                {
                    surveyAnalytic.IsDeleted = true;
                    surveyAnalytic.DeletedDate = DateTime.UtcNow;
                }

                var surveyQuestions = await _context.Set<SurveyQuestion>().Where(a => a.SurveyId == id).ToListAsync();
                foreach (var surveyQuestion in surveyQuestions)
                {
                    surveyQuestion.IsDeleted = true;
                    surveyQuestion.DeletedDate = DateTime.UtcNow;
                }

                var surveyResponses = await _context.Set<SurveyResponse>().Where(a => a.SurveyId == id).ToListAsync();
                foreach (var surveyResponse in surveyResponses)
                {
                    surveyResponse.IsDeleted = true;
                    surveyResponse.DeletedDate = DateTime.UtcNow;
                }

                var savedContents = await _context.Set<SavedContent>().Where(a => a.SurveyId == id).ToListAsync();
                foreach (var savedContent in savedContents)
                {
                    savedContent.IsDeleted = true;
                    savedContent.DeletedDate = DateTime.UtcNow;
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting Deleted the entity.", ex);
            }
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            try
            {
                var data = await _context.Set<Survey>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (data == null)
                {
                    return false;
                }
                data.IsDeleted = false;

                var hits = await _context.Set<Hit>().Where(a => a.SurveyId == id).ToListAsync();
                foreach (var hit in hits)
                {
                    hit.IsDeleted = false;
                }

                var likes = await _context.Set<Like>().Where(a => a.SurveyId == id).ToListAsync();
                foreach (var like in likes)
                {
                    like.IsDeleted = false;
                }

                var reports = await _context.Set<Report>().Where(a => a.SurveyId == id).ToListAsync();
                foreach (var report in reports)
                {
                    report.IsDeleted = false;
                }

                var surveyAnalytics = await _context.Set<SurveyAnalytics>().Where(a => a.SurveyId == id).ToListAsync();
                foreach (var surveyAnalytic in surveyAnalytics)
                {
                    surveyAnalytic.IsDeleted = false;
                }

                var surveyQuestions = await _context.Set<SurveyQuestion>().Where(a => a.SurveyId == id).ToListAsync();
                foreach (var surveyQuestion in surveyQuestions)
                {
                    surveyQuestion.IsDeleted = false;
                }

                var surveyResponses = await _context.Set<SurveyResponse>().Where(a => a.SurveyId == id).ToListAsync();
                foreach (var surveyResponse in surveyResponses)
                {
                    surveyResponse.IsDeleted = false;
                }

                var savedContents = await _context.Set<SavedContent>().Where(a => a.SurveyId == id).ToListAsync();
                foreach (var savedContent in savedContents)
                {
                    savedContent.IsDeleted = false;
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting NotDeleted the entity.", ex);
            }
        }

        public int SurveyCounter()
        {
            try
            {
                return _context.Surveys.Count();
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public async Task<IEnumerable<Survey>> GetAllIncludingMostPopularSurveysAsync()
        {
            try
            {
                var popularInvestors = await _context.Surveys
                       .Where(i => i.IsActive == true && i.IsDeleted == false)
                       .Include(i=>i.Company)
                       .Include(i => i.Investor)
                       .Include(i => i.Investor.AppUser)
                       .Include(i => i.SurveyResponses)
                       .Include(i => i.SurveyAnalytics)
                       .Include(i => i.AppUser)
                       .Include(i => i.Hits)
                       .Include(i => i.Likes)
                       .Include(i => i.SavedContents).AsSplitQuery()
                       .OrderByDescending(i =>
                           i.SurveyAnalytics.Count() * 10.0 +
                           i.SurveyResponses.Count() * 20.0 +
                           i.Hits.Count() * 15.0 +
                           i.Likes.Count() * 25.0 +
                           i.SavedContents.Count() * 30.0)
                       .Take(120).ToListAsync();
                return popularInvestors;
            }
            catch (Exception)
            {
                return new List<Survey>();
            }
        }

        public async Task<bool> SubmitSurveyAnswersAsync(int surveyId, Dictionary<int, int> answers)
        {
            try
            {
                var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("UserId")?.Value
                           ?? _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var sessionUserId = _httpContextAccessor.HttpContext.Session.GetString("userId");
                var appUserId = userIdClaim ?? sessionUserId;

                if (string.IsNullOrEmpty(appUserId))
                {
                    throw new UnauthorizedAccessException("User not authenticated. UserId not found in claims or session.");
                }

                if (surveyId <= 0)
                    throw new ArgumentException("Geçersiz anket ID'si.", nameof(surveyId));

                if (answers == null || !answers.Any())
                    throw new ArgumentException("Hiçbir cevap seçilmedi.", nameof(answers));

                var hasResponded = await _context.SurveyResponses.AnyAsync(r => r.SurveyId == surveyId && r.AppUserId == appUserId);
                if (hasResponded)
                    throw new InvalidOperationException("Bu ankete daha önce yanıt verdiniz. Bir kullanıcı sadece bir kez katılabilir.");

                var surveyResponse = new SurveyResponse
                {
                    SurveyId = surveyId,
                    AppUserId = appUserId,
                    StartedAt = DateTime.UtcNow,
                    CompletedAt = DateTime.UtcNow,
                    IsCompleted = true
                };
                _context.SurveyResponses.Add(surveyResponse);

                var surveyAnswers = answers.Select(kvp => new SurveyAnswer
                {
                    SurveyResponse = surveyResponse,
                    SurveyQuestionId = kvp.Key,
                    QuestionOptionId = kvp.Value,
                    AppUserId = appUserId,
                    CreatedDate = DateTime.UtcNow
                }).ToList();
                _context.SurveyAnswers.AddRange(surveyAnswers);
                await _context.SaveChangesAsync();
                await _surveyAnalyticsRepository.UpdateSurveyAnalyticsAsync(surveyId);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Anket cevapları kaydedilirken bir hata oluştu: " + ex.Message, ex);
            }
        }

        public async Task<IEnumerable<Survey>> GetAllIncludingLessPopularSurveysAsync()
        {
            try
            {
                var popularSurveys = await _context.Surveys
                       .Where(i => i.IsActive == true && i.IsDeleted == false)
                       .Include(i => i.Company)
                       .Include(i=>i.Investor)
                       .Include(i=>i.Investor.AppUser)
                       .Include(i => i.SurveyResponses)
                       .Include(i => i.SurveyAnalytics)
                       .Include(i => i.AppUser)
                       .Include(i => i.Hits)
                       .Include(i => i.Likes)
                       .Include(i => i.SavedContents).AsSplitQuery()
                       .OrderBy(i =>
                           i.SurveyAnalytics.Count() * 10.0 +
                           i.SurveyResponses.Count() * 20.0 +
                           i.Hits.Count() * 15.0 +
                           i.Likes.Count() * 25.0 +
                           i.SavedContents.Count() * 30.0)
                       .Take(120).ToListAsync();
                return popularSurveys;
            }
            catch (Exception)
            {
                return new List<Survey>();
            }
        }
    }
}
