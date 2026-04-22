using Investigation.DataAccess.Abstract;
using Investigation.DataAccess.Concrete.EntityFramework.Context.MSSQL;
using Investigation.Domain.Entities;
using Investigation.Shared.DataAccess.EntityFramework;
using Investigation.Shared.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Investigation.DataAccess.Concrete.EntityFramework.Repositories
{
    public class SurveyQuestionRepository : EntityRepositoryBase<SurveyQuestion, ApplicationDbContext>, ISurveyQuestionRepository
    {
        readonly ApplicationDbContext _context;
        public SurveyQuestionRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<SurveyQuestion?> GetBySlugAsync(string slug)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(slug) || slug.Length != 8)
                    return null;

                var candidates = await _context.SurveyQuestions.AsNoTracking().Where(n => n.IsActive && !n.IsDeleted)
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
                return await _context.SurveyQuestions.FirstOrDefaultAsync(n => n.Id == match.Id && n.IsActive && !n.IsDeleted);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred.", ex);
            }
        }
        public async Task<bool> SetActiveAsync(int id)
        {
            try
            {
                var data = await _context.Set<SurveyQuestion>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (data == null)
                {
                    return false;
                }
                data.IsActive = true;

                var questionOptions = await _context.Set<QuestionOption>().Where(a => a.SurveyQuestionId == id).ToListAsync();
                foreach (var questionOption in questionOptions)
                {
                    questionOption.IsActive = true;
                }

                var surveyAnswers = await _context.Set<SurveyAnswer>().Where(a => a.SurveyQuestionId == id).ToListAsync();
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
                var data = await _context.Set<SurveyQuestion>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (data == null)
                {
                    return false;
                }
                data.IsActive = true;
                data.SuspendedDate = DateTime.UtcNow;

                var questionOptions = await _context.Set<QuestionOption>().Where(a => a.SurveyQuestionId == id).ToListAsync();
                foreach (var questionOption in questionOptions)
                {
                    questionOption.IsActive = false;
                    questionOption.SuspendedDate = DateTime.UtcNow;
                }

                var surveyAnswers = await _context.Set<SurveyAnswer>().Where(a => a.SurveyQuestionId == id).ToListAsync();
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
                var data = await _context.Set<SurveyQuestion>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (data == null)
                {
                    return false;
                }
                data.IsDeleted = true;
                data.DeletedDate = DateTime.UtcNow;

                var questionOptions = await _context.Set<QuestionOption>().Where(a => a.SurveyQuestionId == id).ToListAsync();
                foreach (var questionOption in questionOptions)
                {
                    questionOption.IsDeleted = true;
                    questionOption.DeletedDate = DateTime.UtcNow;
                }

                var surveyAnswers = await _context.Set<SurveyAnswer>().Where(a => a.SurveyQuestionId == id).ToListAsync();
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
                var data = await _context.Set<SurveyQuestion>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (data == null)
                {
                    return false;
                }
                data.IsDeleted = false;

                var questionOptions = await _context.Set<QuestionOption>().Where(a => a.SurveyQuestionId == id).ToListAsync();
                foreach (var questionOption in questionOptions)
                {
                    questionOption.IsDeleted = false;
                }

                var surveyAnswers = await _context.Set<SurveyAnswer>().Where(a => a.SurveyQuestionId == id).ToListAsync();
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
    }
}
