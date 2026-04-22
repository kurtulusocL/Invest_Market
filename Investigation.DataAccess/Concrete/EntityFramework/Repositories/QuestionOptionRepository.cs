using Investigation.DataAccess.Abstract;
using Investigation.DataAccess.Concrete.EntityFramework.Context.MSSQL;
using Investigation.Domain.Entities;
using Investigation.Shared.DataAccess.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Investigation.DataAccess.Concrete.EntityFramework.Repositories
{
    public class QuestionOptionRepository : EntityRepositoryBase<QuestionOption, ApplicationDbContext>, IQuestionOptionRepository
    {
        readonly ApplicationDbContext _context;
        public QuestionOptionRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            try
            {
                var data = await _context.Set<QuestionOption>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (data == null)
                {
                    return false;
                }
                data.IsActive = true;

                var surveyAnswers = await _context.Set<SurveyAnswer>().Where(a => a.QuestionOptionId == id).ToListAsync();
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
                var data = await _context.Set<QuestionOption>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (data == null)
                {
                    return false;
                }
                data.IsActive = false;
                data.SuspendedDate= DateTime.UtcNow;

                var surveyAnswers = await _context.Set<SurveyAnswer>().Where(a => a.QuestionOptionId == id).ToListAsync();
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
                var data = await _context.Set<QuestionOption>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (data == null)
                {
                    return false;
                }
                data.IsDeleted = true;
                data.DeletedDate = DateTime.UtcNow;

                var surveyAnswers = await _context.Set<SurveyAnswer>().Where(a => a.QuestionOptionId == id).ToListAsync();
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
                var data = await _context.Set<QuestionOption>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (data == null)
                {
                    return false;
                }
                data.IsDeleted = false;

                var surveyAnswers = await _context.Set<SurveyAnswer>().Where(a => a.QuestionOptionId == id).ToListAsync();
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
