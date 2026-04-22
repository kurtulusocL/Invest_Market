using System.Linq.Expressions;
using Investigation.Business.Services.Abstract;
using Investigation.DataAccess.Abstract;
using Investigation.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Investigation.Business.Services.Concrete
{
    public class NewsManager : INewsService
    {
        readonly INewsRepository _newsRepository;
        public NewsManager(INewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }

        public async Task<bool> CreateAsync(News entity, IFormFile image)
        {
            try
            {
                var errors = new List<string>();
                if (image != null)
                {
                    var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/img/news/");
                    if (!Directory.Exists(directoryPath))
                    {
                        Console.WriteLine($"Path is preparing: {directoryPath}");
                        Directory.CreateDirectory(directoryPath);
                    }
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                    var filePath = Path.Combine(directoryPath, fileName);
                    try
                    {
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await image.CopyToAsync(stream);
                        }
                        entity.ImageUrl = fileName;
                        var result = await _newsRepository.AddAsync(entity);
                        if (!result)
                        {
                            errors.Add($"Error {fileName}.");
                        }
                        return true;
                    }
                    catch (Exception ex)
                    {
                        errors.Add($"Error {fileName} : {ex.Message}");
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> DeleteAsync(News entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var data = await _newsRepository.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _newsRepository.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public IQueryable<News> GetAllForSitemap()
        {
            try
            {
                return _newsRepository.GetAll(i => i.IsActive == true && i.IsDeleted == false).OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<News>().AsQueryable();
            }
        }

        public IQueryable<News> GetAllIncludingAsync()
        {
            try
            {
                var data = _newsRepository.GetAllInclude(new Expression<Func<News, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Reports);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<News>().AsQueryable();
            }
        }

        public IQueryable<News> GetAllIncludingByMostHitAsync()
        {
            try
            {
                var data = _newsRepository.GetAllInclude(new Expression<Func<News, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Reports);
                return data.OrderByDescending(i => i.Hit);
            }
            catch (Exception)
            {
                return Enumerable.Empty<News>().AsQueryable();
            }
        }

        public IQueryable<News> GetAllIncludingByMostLikedAsync()
        {
            try
            {
                var data = _newsRepository.GetAllInclude(new Expression<Func<News, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Reports);
                return data.OrderByDescending(i => i.Like);
            }
            catch (Exception)
            {
                return Enumerable.Empty<News>().AsQueryable();
            }
        }

        public IQueryable<News> GetAllIncludingForAdminAsync()
        {
            try
            {
                var data = _newsRepository.GetAllInclude(new Expression<Func<News, bool>>[]
                {

                }, null, y => y.Reports);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<News>().AsQueryable();
            }
        }

        public IQueryable<News> GetAllNewsForPublicUser()
        {
            try
            {
                return _newsRepository.GetAll(i => i.IsActive == true && i.IsDeleted == false).OrderByDescending(i => i.CreatedDate).Take(5);
            }
            catch (Exception)
            {
                return Enumerable.Empty<News>().AsQueryable();
            }
        }
        public async Task<News?> GetBySlugAsync(string slug)
        {
            var match = await _newsRepository.GetBySlugAsync(slug);
            if (match == null)
            {
                return null;
            }
            return await GetByIdAsync(match.Id);
        }
        public async Task<News> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                return await _newsRepository.GetIncludeAsync(i => i.Id == id, y => y.Reports);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public News HitRead(int id)
        {
            return _newsRepository.HitRead(id);
        }

        public async Task<bool> LikeAsync(int id)
        {
            var result = await _newsRepository.LikeAsync(id);
            return result;
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _newsRepository.SetActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _newsRepository.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _newsRepository.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _newsRepository.SetNotDeletedAsync(id);
            return result;
        }

        public async Task<bool> UpdateAsync(News entity, IFormFile image)
        {
            try
            {
                var errors = new List<string>();
                if (image != null)
                {
                    var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/img/news/");
                    if (!Directory.Exists(directoryPath))
                    {
                        Console.WriteLine($"Path is preparing: {directoryPath}");
                        Directory.CreateDirectory(directoryPath);
                    }
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                    var filePath = Path.Combine(directoryPath, fileName);
                    try
                    {
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await image.CopyToAsync(stream);
                        }
                        entity.ImageUrl = fileName;
                        entity.UpdatedDate = DateTime.UtcNow;
                        var result = await _newsRepository.UpdateAsync(entity);
                        if (!result)
                        {
                            errors.Add($"Error {fileName}.");
                        }
                        return true;
                    }
                    catch (Exception ex)
                    {
                        errors.Add($"Error {fileName} : {ex.Message}");
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while updating the entity.", ex);
            }
        }

        public async Task<IEnumerable<News>> GetAllForSignalRAsync()
        {
            try
            {
                var data = await _newsRepository.GetAllIncludeAsync(new Expression<Func<News, bool>>[]
                {
                   
                }, null, y => y.Reports);
                return data.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<News>();
            }
        }
    }
}
