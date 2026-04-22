using System.Linq.Expressions;
using Investigation.Business.Services.Abstract;
using Investigation.DataAccess.Abstract;
using Investigation.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Investigation.Business.Services.Concrete
{
    public class SectorNewsManager : ISectorNewsService
    {
        readonly ISectorNewsRepository _sectorNewsRepository;
        public SectorNewsManager(ISectorNewsRepository sectorNewsRepository)
        {
            _sectorNewsRepository = sectorNewsRepository;
        }

        public async Task<bool> CreateAsync(SectorNews entity, IFormFile image)
        {
            try
            {
                var errors = new List<string>();
                if (image != null)
                {
                    var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/img/sectorNews/");
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
                        var result = await _sectorNewsRepository.AddAsync(entity);
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

        public async Task<bool> DeleteAsync(SectorNews entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var data = await _sectorNewsRepository.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _sectorNewsRepository.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public IQueryable<SectorNews> GetAllForSitemap()
        {
            try
            {
                return _sectorNewsRepository.GetAll(i => i.IsActive == true && i.IsDeleted == false).OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<SectorNews>().AsQueryable();
            }
        }

        public IQueryable<SectorNews> GetAllIncludingAsync()
        {
            try
            {
                var data = _sectorNewsRepository.GetAllInclude(new Expression<Func<SectorNews, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Reports, y => y.SavedContents);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<SectorNews>().AsQueryable();
            }
        }

        public IQueryable<SectorNews> GetAllIncludingByMostHitAsync()
        {
            try
            {
                var data = _sectorNewsRepository.GetAllInclude(new Expression<Func<SectorNews, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Reports, y => y.SavedContents);
                return data.OrderByDescending(i => i.Hit);
            }
            catch (Exception)
            {
                return Enumerable.Empty<SectorNews>().AsQueryable();
            }
        }

        public IQueryable<SectorNews> GetAllIncludingByMostLikeAsync()
        {
            try
            {
                var data = _sectorNewsRepository.GetAllInclude(new Expression<Func<SectorNews, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Reports, y => y.SavedContents);
                return data.OrderByDescending(i => i.Like);
            }
            catch (Exception)
            {
                return Enumerable.Empty<SectorNews>().AsQueryable();
            }
        }

        public IQueryable<SectorNews> GetAllIncludingForAdminAsync()
        {
            try
            {
                var data = _sectorNewsRepository.GetAllInclude(new Expression<Func<SectorNews, bool>>[]
                {

                }, null, y => y.Reports, y => y.SavedContents);
                return data.OrderByDescending(i => i.Hit);
            }
            catch (Exception)
            {
                return Enumerable.Empty<SectorNews>().AsQueryable();
            }
        }

        public IQueryable<SectorNews> GetAllIncludingLastSectorNews()
        {
            try
            {
                return _sectorNewsRepository.GetAllInclude(new Expression<Func<SectorNews, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.SavedContents).OrderByDescending(i => i.CreatedDate).Take(5);
            }
            catch (Exception)
            {
                return Enumerable.Empty<SectorNews>().AsQueryable();
            }
        }

        public IQueryable<SectorNews> GetAllIncludingLastSectorNewsForIndex()
        {
            try
            {
                //var today = DateTime.Today;
                //var tomorrow = today.AddDays(1);

                return _sectorNewsRepository.GetAllInclude(new Expression<Func<SectorNews, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    //i => i.CreatedDate >= today && i.CreatedDate < tomorrow
                }, null, y => y.SavedContents).OrderByDescending(i => Guid.NewGuid()).Take(35);
            }
            catch (Exception)
            {
                return Enumerable.Empty<SectorNews>().AsQueryable();
            }
        }

        public IQueryable<SectorNews> GetAllIncludingLastSectorNewsForTimeline()
        {
            try
            {
                //var today = DateTime.Today;
                //var tomorrow = today.AddDays(1);

                return _sectorNewsRepository.GetAllInclude(new Expression<Func<SectorNews, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    //i => i.CreatedDate >= today && i.CreatedDate < tomorrow
                }, null, y => y.SavedContents).OrderByDescending(i => Guid.NewGuid()).Take(25);
            }
            catch (Exception)
            {
                return Enumerable.Empty<SectorNews>().AsQueryable();
            }
        }

        public IQueryable<SectorNews> GetAllIncludingSectorNewsPopular()
        {
            try
            {
                return _sectorNewsRepository.GetAllInclude(new Expression<Func<SectorNews, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i => i.Hit>350
                }, null).OrderByDescending(i => i.CreatedDate).Take(15);
            }
            catch (Exception)
            {
                return Enumerable.Empty<SectorNews>().AsQueryable();
            }
        }

        public IQueryable<SectorNews> GetAllIncludingSectorNewsRandom()
        {
            try
            {
                return _sectorNewsRepository.GetAllInclude(new Expression<Func<SectorNews, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                }, null).OrderBy(i => i.CreatedDate).OrderBy(i => Guid.NewGuid()).Take(15);
            }
            catch (Exception)
            {
                return Enumerable.Empty<SectorNews>().AsQueryable();
            }
        }

        public IQueryable<SectorNews> GetAllSectorNewsForPublicUser()
        {
            try
            {
                return _sectorNewsRepository.GetAllInclude(new Expression<Func<SectorNews, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null).OrderByDescending(i => i.CreatedDate).Take(5);
            }
            catch (Exception)
            {
                return Enumerable.Empty<SectorNews>().AsQueryable();
            }
        }
        public async Task<SectorNews?> GetBySlugAsync(string slug)
        {
            var match = await _sectorNewsRepository.GetBySlugAsync(slug);
            if (match == null)
            {
                return null;
            }
            return await GetByIdAsync(match.Id);
        }
        public async Task<SectorNews> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                return await _sectorNewsRepository.GetIncludeAsync(i => i.Id == id, y => y.Reports, y => y.SavedContents);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public SectorNews HitRead(int id)
        {
            return _sectorNewsRepository.HitRead(id);
        }

        public async Task<bool> LikeAsync(int id)
        {
            var result = await _sectorNewsRepository.LikeAsync(id);
            return result;
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _sectorNewsRepository.SetActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _sectorNewsRepository.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _sectorNewsRepository.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _sectorNewsRepository.SetNotDeletedAsync(id);
            return result;
        }

        public async Task<bool> UpdateAsync(SectorNews entity, IFormFile image)
        {
            try
            {
                var errors = new List<string>();
                if (image != null)
                {
                    var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/img/sectorNews/");
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
                        var result = await _sectorNewsRepository.UpdateAsync(entity);
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

        public async Task<IEnumerable<SectorNews>> GetAllForSignalRAsync()
        {
            try
            {
                var data = await _sectorNewsRepository.GetAllIncludeAsync(new Expression<Func<SectorNews, bool>>[]
                {

                }, null, y => y.Reports, y => y.SavedContents);
                return data.OrderByDescending(i => i.Hit).ToList();
            }
            catch (Exception)
            {
                return new List<SectorNews>();
            }
        }
    }
}
