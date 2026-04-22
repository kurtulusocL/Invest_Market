using Investigation.DataAccess.Abstract;
using Investigation.DataAccess.Concrete.EntityFramework.Context.MSSQL;
using Investigation.Domain.Entities;
using Investigation.Shared.DataAccess.EntityFramework;
using Investigation.Shared.Helpers;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Investigation.DataAccess.Concrete.EntityFramework.Repositories
{
    public class SurveyAnalyticsRepository : EntityRepositoryBase<SurveyAnalytics, ApplicationDbContext>, ISurveyAnalyticsRepository
    {
        readonly ApplicationDbContext _context;
        public SurveyAnalyticsRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        
        public async Task<SurveyAnalytics> GetIncludingClosedSurveyDataBySurveyIdAsync(int surveyId)
        {
            try
            {
                var query = from a in _context.SurveyAnalytics
                            join s in _context.Surveys on a.SurveyId equals s.Id
                            join q in _context.SurveyQuestions on s.Id equals q.SurveyId into questionsGroup
                            from q in questionsGroup.DefaultIfEmpty()
                            join o in _context.QuestionOptions on q.Id equals o.SurveyQuestionId into optionsGroup
                            from o in optionsGroup.DefaultIfEmpty()
                            where a.SurveyId == surveyId
                            select new
                            {
                                Analytics = a,
                                Survey = s,
                                Question = q,
                                Option = o
                            };

                var results = await query.AsNoTracking().ToListAsync();

                if (!results.Any())
                    return null;

                var analytics = results.First().Analytics;
                analytics.Survey = results.First().Survey;

                // Soruları ve seçenekleri gruplandır
                analytics.Survey.SurveyQuestions = results
                    .Where(r => r.Question != null)
                    .GroupBy(r => r.Question.Id)
                    .Select(g => new SurveyQuestion
                    {
                        Id = g.Key,
                        QuestionText = g.First().Question.QuestionText,
                        OrderIndex = g.First().Question.OrderIndex,
                        IsRequired = g.First().Question.IsRequired,
                        SurveyId = g.First().Question.SurveyId,
                        QuestionOptions = g.Where(r => r.Option != null)
                            .Select(r => new QuestionOption
                            {
                                Id = r.Option.Id,
                                OptionText = r.Option.OptionText,
                                OrderIndex = r.Option.OrderIndex,
                                SurveyQuestionId = r.Option.SurveyQuestionId
                            }).ToList()
                    }).ToList();

                return analytics;
            }
            catch (Exception ex)
            {
                throw new Exception("Anket analizi yüklenirken bir hata oluştu.", ex);
            }
        }

        public async Task LoadSurveyQuestionsAndOptionsAsync(SurveyAnalytics analytics)
        {
            try
            {
                if (analytics?.Survey == null) return;

                await _context.Entry(analytics.Survey)
                    .Collection(s => s.SurveyQuestions)
                    .Query()
                    .Include(q => q.QuestionOptions)
                    .LoadAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting Include the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            try
            {
                var active = await _context.Set<SurveyAnalytics>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (active != null)
                {
                    active.IsActive = true;
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
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
                var active = await _context.Set<SurveyAnalytics>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (active != null)
                {
                    active.IsActive = false;
                    active.SuspendedDate = DateTime.Now.ToLocalTime();
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
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
                var deleted = await _context.Set<SurveyAnalytics>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (deleted != null)
                {
                    deleted.IsDeleted = true;
                    deleted.DeletedDate = DateTime.Now.ToLocalTime();
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
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
                var deleted = await _context.Set<SurveyAnalytics>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (deleted != null)
                {
                    deleted.IsDeleted = false;
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting Not Deleted the entity.", ex);
            }
        }

        public async Task UpdateSurveyAnalyticsAsync(int surveyId)
        {
            try
            {
                if (surveyId <= 0) return;

                var survey = await _context.Surveys.Include(s => s.SurveyResponses).ThenInclude(r => r.SurveyAnswers).Include(s => s.SurveyQuestions).ThenInclude(q => q.QuestionOptions).AsNoTracking().FirstOrDefaultAsync(s => s.Id == surveyId);
                if (survey == null) return;

                var analytics = await _context.SurveyAnalytics.FirstOrDefaultAsync(a => a.SurveyId == surveyId);
                if (analytics == null)
                {
                    analytics = new SurveyAnalytics { SurveyId = surveyId };
                    _context.SurveyAnalytics.Add(analytics);
                }

                var responses = survey.SurveyResponses?.ToList() ?? new List<SurveyResponse>();
                analytics.TotalResponses = responses.Count;
                analytics.CompletionRate = responses.Any()
                    ? (decimal)responses.Count(r => r.IsCompleted) / responses.Count * 100
                    : 0;
                analytics.AverageCompletionTimeSeconds = responses.Any(r => r.IsCompleted && r.CompletedAt.HasValue)
                    ? (int)responses.Where(r => r.IsCompleted && r.CompletedAt.HasValue)
                        .Average(r => (r.CompletedAt.Value - r.StartedAt).TotalSeconds)
                    : 0;
                analytics.LastUpdated = DateTime.UtcNow;

                var questionAnalytics = new Dictionary<int, object>();

                foreach (var question in survey.SurveyQuestions ?? Enumerable.Empty<SurveyQuestion>())
                {
                    var questionAnswers = responses.Where(r => r.SurveyAnswers != null).SelectMany(r => r.SurveyAnswers).Where(a => a.SurveyQuestionId == question.Id)
                        .ToList();

                    var totalQuestionResponses = questionAnswers.Count;
                    var optionPercentages = new Dictionary<int, decimal>();

                    foreach (var option in question.QuestionOptions ?? Enumerable.Empty<QuestionOption>())
                    {
                        var optionCount = questionAnswers.Count(a => a.QuestionOptionId == option.Id);
                        var percentage = totalQuestionResponses > 0
                            ? Math.Round((decimal)optionCount / totalQuestionResponses * 100, 2)
                            : 0;
                        optionPercentages[option.Id] = percentage;
                    }
                    questionAnalytics[question.Id] = new
                    {
                        CompletionRate = responses.Any()
                            ? Math.Round((decimal)totalQuestionResponses / responses.Count * 100, 2)
                            : 0,
                        Options = optionPercentages
                    };
                }
                analytics.AnalyticsDataJson = JsonConvert.SerializeObject(new
                {
                    Questions = questionAnalytics,
                    AllowMultipleResponses = survey.AllowMultipleResponses,
                    IsAnonymous = survey.IsAnonymous
                }, Formatting.Indented);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting Analytics Datas.", ex);
            }
        }
    }
}
