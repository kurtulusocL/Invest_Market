using Investigation.Business.Services.Abstract;
using Investigation.DataAccess.Abstract;
using Investigation.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Investigation.Business.Services.Concrete
{
    public class SocialMediaManager : ISocialMediaService
    {
        readonly ISocialMediaRepository _socialMediaRepository;
        public SocialMediaManager(ISocialMediaRepository socialMediaRepository)
        {
            _socialMediaRepository = socialMediaRepository;
        }

        public async Task<bool> CreateAsync(SocialMedia entity, IFormFile image)
        {
            try
            {
                var errors = new List<string>();
                if (image != null)
                {
                    var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/img/socialMedia/");
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
                        entity.IconUrl = fileName;
                        var result = await _socialMediaRepository.AddAsync(entity);
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

        public async Task<bool> DeleteAsync(SocialMedia entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var data = await _socialMediaRepository.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _socialMediaRepository.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public IQueryable<SocialMedia> GetAllForSitemap()
        {
            try
            {
                return _socialMediaRepository.GetAll(i => i.IsActive == true && i.IsDeleted == false).OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<SocialMedia>().AsQueryable();
            }
        }

        public IQueryable<SocialMedia> GetAllAsync()
        {
            try
            {
                var data = _socialMediaRepository.GetAll(i => i.IsActive == true && i.IsDeleted == false);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<SocialMedia>().AsQueryable();
            }
        }

        public IQueryable<SocialMedia> GetAllForAdminAsync()
        {
            try
            {
                var data = _socialMediaRepository.GetAll();
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<SocialMedia>().AsQueryable();
            }
        }

        public IQueryable<SocialMedia> GetAllSocialMedia()
        {
            try
            {
                return _socialMediaRepository.GetAll(i => i.IsActive == true && i.IsDeleted == false).OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<SocialMedia>().AsQueryable();
            }
        }

        public async Task<SocialMedia> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                return await _socialMediaRepository.GetAsync(i => i.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _socialMediaRepository.SetActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _socialMediaRepository.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _socialMediaRepository.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _socialMediaRepository.SetNotDeletedAsync(id);
            return result;
        }

        public async Task<bool> UpdateAsync(SocialMedia entity, IFormFile image)
        {
            try
            {
                var errors = new List<string>();
                if (image != null)
                {
                    var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/img/socialMedia/");
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
                        entity.IconUrl = fileName;
                        entity.UpdatedDate = DateTime.UtcNow;
                        var result = await _socialMediaRepository.UpdateAsync(entity);
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

        public async Task<IEnumerable<SocialMedia>> GetAllForSignalRAsync()
        {
            try
            {
                var data = await _socialMediaRepository.GetAllAsync();
                return data.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<SocialMedia>();
            }
        }
    }
}
