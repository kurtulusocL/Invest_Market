using Investigation.DataAccess.Abstract;
using Investigation.DataAccess.Concrete.EntityFramework.Context.MSSQL;
using Investigation.Domain.Entities;
using Investigation.Shared.DataAccess.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace Investigation.DataAccess.Concrete.EntityFramework.Repositories
{
    public class SurveyResponseRepository : EntityRepositoryBase<SurveyResponse, ApplicationDbContext>, ISurveyResponseRepository
    {
        readonly ApplicationDbContext _context;
        public SurveyResponseRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task CalculateAndSaveAnalyticsAsync(int surveyId)
        {
            try
            {
                var responses = await _context.SurveyResponses.Where(r => r.SurveyId == surveyId).Include(r => r.SurveyAnswers).AsNoTracking().ToListAsync();

                var totalResponses = responses.Count;
                if (totalResponses == 0)
                {
                    var emptyAnalytics = await _context.SurveyAnalytics.FirstOrDefaultAsync(a => a.SurveyId == surveyId);
                    if (emptyAnalytics == null)
                    {
                        emptyAnalytics = new SurveyAnalytics
                        {
                            SurveyId = surveyId,
                            AnalyticsDataJson = "{\"Questions\":{}}",
                            TotalResponses = 0,
                            CompletionRate = 0,
                            LastUpdated = DateTime.UtcNow
                        };
                        _context.SurveyAnalytics.Add(emptyAnalytics);
                    }
                    else
                    {
                        emptyAnalytics.AnalyticsDataJson = "{\"Questions\":{}}";
                        emptyAnalytics.TotalResponses = 0;
                        emptyAnalytics.LastUpdated = DateTime.UtcNow;
                    }
                    await _context.SaveChangesAsync();
                    return;
                }
                var questions = await _context.SurveyQuestions.Where(q => q.SurveyId == surveyId).Include(q => q.QuestionOptions).AsNoTracking().ToListAsync();

                var questionsAnalytics = new JObject();
                foreach (var question in questions)
                {
                    var questionAnswers = responses.SelectMany(r => r.SurveyAnswers).Where(a => a.QuestionOptionId == question.Id).ToList();

                    var answeredCount = questionAnswers.Count;
                    decimal completionRate = totalResponses > 0
                        ? (decimal)answeredCount / totalResponses * 100
                        : 0;

                    var optionsData = new JObject();
                    foreach (var option in question.QuestionOptions)
                    {
                        var optionCount = questionAnswers.Count(a => a.QuestionOptionId == option.Id);
                        decimal percentage = answeredCount > 0
                            ? (decimal)optionCount / answeredCount * 100
                            : 0;
                        optionsData[option.Id.ToString()] = Math.Round(percentage, 2);
                    }
                    questionsAnalytics[question.Id.ToString()] = new JObject
                    {
                        ["CompletionRate"] = Math.Round(completionRate, 2),
                        ["Options"] = optionsData
                    };
                }
                var analyticsJson = new JObject
                {
                    ["Questions"] = questionsAnalytics
                }.ToString();

                var analytics = await _context.SurveyAnalytics.FirstOrDefaultAsync(a => a.SurveyId == surveyId);
                if (analytics == null)
                {
                    analytics = new SurveyAnalytics
                    {
                        SurveyId = surveyId,
                        AnalyticsDataJson = analyticsJson,
                        TotalResponses = totalResponses,
                        CompletionRate = 100, 
                        LastUpdated = DateTime.UtcNow
                    };
                    _context.SurveyAnalytics.Add(analytics);
                }
                else
                {
                    analytics.AnalyticsDataJson = analyticsJson;
                    analytics.TotalResponses = totalResponses;
                    analytics.LastUpdated = DateTime.UtcNow;
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Anket analizi hesaplanırken hata oluştu.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            try
            {
                var data = await _context.Set<SurveyResponse>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (data == null)
                {
                    return false;
                }
                data.IsActive = true;

                var surveyAnswers = await _context.Set<SurveyAnswer>().Where(a => a.SurveyResponseId == id).ToListAsync();
                foreach (var surveyAnswer in surveyAnswers)
                {
                    surveyAnswer.IsActive = true;
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
                var data = await _context.Set<SurveyResponse>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (data == null)
                {
                    return false;
                }
                data.IsActive = false;
                data.SuspendedDate = DateTime.UtcNow;

                var surveyAnswers = await _context.Set<SurveyAnswer>().Where(a => a.SurveyResponseId == id).ToListAsync();
                foreach (var surveyAnswer in surveyAnswers)
                {
                    surveyAnswer.IsActive = false;
                    surveyAnswer.SuspendedDate = DateTime.UtcNow;
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
                var data = await _context.Set<SurveyResponse>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (data == null)
                {
                    return false;
                }
                data.IsDeleted = true;
                data.DeletedDate = DateTime.UtcNow;

                var surveyAnswers = await _context.Set<SurveyAnswer>().Where(a => a.SurveyResponseId == id).ToListAsync();
                foreach (var surveyAnswer in surveyAnswers)
                {
                    surveyAnswer.IsDeleted = true;
                    surveyAnswer.DeletedDate = DateTime.UtcNow;
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
                var data = await _context.Set<SurveyResponse>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (data == null)
                {
                    return false;
                }
                data.IsDeleted = false;

                var surveyAnswers = await _context.Set<SurveyAnswer>().Where(a => a.SurveyResponseId == id).ToListAsync();
                foreach (var surveyAnswer in surveyAnswers)
                {
                    surveyAnswer.IsDeleted = false;
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting NotDeleted the entity.", ex);
            }
        }

        public int SurveyResponseCounter()
        {
            try
            {
                return _context.SurveyResponses.Count();
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
