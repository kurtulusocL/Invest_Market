using Investigation.Business.Services.Abstract;
using Investigation.DataAccess.Abstract;
using Investigation.Domain.Entities;

namespace Investigation.Business.Services.Concrete
{
    public class ExceptionLoggerManager : IExceptionLoggerService
    {
        readonly IExceptionLoggerRepository _exceptionLoggerRepository;
        public ExceptionLoggerManager(IExceptionLoggerRepository exceptionLoggerRepository)
        {
            _exceptionLoggerRepository = exceptionLoggerRepository;
        }

        public async Task<bool> DeleteAllByIdAsync(List<int> ids)
        {
            try
            {
                if (ids == null || !ids.Any())
                    throw new ArgumentNullException(nameof(ids), "id list was null or empty");

                var result = await _exceptionLoggerRepository.DeleteByIdsAsync(ids.Cast<object>());
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while bulk deleting entities.", ex);
            }
        }

        public async Task<bool> DeleteAsync(ExceptionLogger entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var data = await _exceptionLoggerRepository.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _exceptionLoggerRepository.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public IQueryable<ExceptionLogger> GetAllAsync()
        {
            try
            {
                var data = _exceptionLoggerRepository.GetAll(i => i.IsActive == true && i.IsDeleted == false);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<ExceptionLogger>().AsQueryable();
            }
        }

        public IQueryable<ExceptionLogger> GetAllForAdminAsync()
        {
            try
            {
                var data = _exceptionLoggerRepository.GetAll();
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<ExceptionLogger>().AsQueryable();
            }
        }

        public async Task<IEnumerable<ExceptionLogger>> GetAllForSignalRAsync()
        {
            try
            {
                var data = await _exceptionLoggerRepository.GetAllAsync();
                return data.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<ExceptionLogger>();
            }
        }

        public async Task<ExceptionLogger> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                return await _exceptionLoggerRepository.GetAsync(i => i.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _exceptionLoggerRepository.SetActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _exceptionLoggerRepository.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _exceptionLoggerRepository.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _exceptionLoggerRepository.SetNotDeletedAsync(id);
            return result;
        }
    }
}
