using Investigation.Business.Services.Abstract;
using Investigation.DataAccess.Abstract;
using Investigation.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Investigation.Business.Services.Concrete
{
    public class BannerImageManager : IBannerImageService
    {
        readonly IBannerImageRepository _bannerImageRepository;
        public BannerImageManager(IBannerImageRepository bannerImageRepository)
        {
            _bannerImageRepository = bannerImageRepository;
        }

        public async Task<bool> CreateAsync(BannerImage entity, IFormFile image)
        {
            try
            {
                var errors = new List<string>();
                if (image != null)
                {
                    var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/img/banner/");
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
                        entity.Image = fileName;
                        var result = await _bannerImageRepository.AddAsync(entity);
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

        public async Task<bool> DeleteAsync(BannerImage entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var data = await _bannerImageRepository.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _bannerImageRepository.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public IQueryable<BannerImage> GetAllAboutBannerImage()
        {
            try
            {
                return _bannerImageRepository.GetAll(i => i.IsActive == true && i.IsDeleted == false && i.ControllerName == "About").OrderByDescending(i => i.CreatedDate).Take(1);
            }
            catch (Exception)
            {
                return Enumerable.Empty<BannerImage>().AsQueryable();
            }
        }

        public IQueryable<BannerImage> GetAllAgreementBannerImage()
        {
            try
            {
                return _bannerImageRepository.GetAll(i => i.IsActive == true && i.IsDeleted == false && i.ControllerName == "Agreement").OrderByDescending(i => i.CreatedDate).Take(1);
            }
            catch (Exception)
            {
                return Enumerable.Empty<BannerImage>().AsQueryable();
            }
        }

        public IQueryable<BannerImage> GetAllAsync()
        {
            try
            {
                var data = _bannerImageRepository.GetAll(i => i.IsActive == true && i.IsDeleted == false);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<BannerImage>().AsQueryable();
            }
        }

        public IQueryable<BannerImage> GetAllBlogBannerImage()
        {
            try
            {
                return _bannerImageRepository.GetAll(i => i.IsActive == true && i.IsDeleted == false && i.ControllerName == "Blog").OrderByDescending(i => i.CreatedDate).Take(1);
            }
            catch (Exception)
            {
                return Enumerable.Empty<BannerImage>().AsQueryable();
            }
        }

        public IQueryable<BannerImage> GetAllContactBannerImage()
        {
            try
            {
                return _bannerImageRepository.GetAll(i => i.IsActive == true && i.IsDeleted == false && i.ControllerName == "Contact").OrderByDescending(i => i.CreatedDate).Take(1);
            }
            catch (Exception)
            {
                return Enumerable.Empty<BannerImage>().AsQueryable();
            }
        }

        public IQueryable<BannerImage> GetAllDataPagesBannerImage()
        {
            try
            {
                return _bannerImageRepository.GetAll(i => i.IsActive == true && i.IsDeleted == false && i.ControllerName == "Data").OrderByDescending(i => i.CreatedDate).Take(1);
            }
            catch (Exception)
            {
                return Enumerable.Empty<BannerImage>().AsQueryable();
            }
        }

        public IQueryable<BannerImage> GetAllEntrepreneurBannerImage()
        {
            try
            {
                return _bannerImageRepository.GetAll(i => i.IsActive == true && i.IsDeleted == false && i.ControllerName == "Entrepreneur").OrderByDescending(i => i.CreatedDate).Take(1);
            }
            catch (Exception)
            {
                return Enumerable.Empty<BannerImage>().AsQueryable();
            }
        }

        public IQueryable<BannerImage> GetAllEventBannerImage()
        {
            try
            {
                return _bannerImageRepository.GetAll(i => i.IsActive == true && i.IsDeleted == false && i.ControllerName == "Event").OrderByDescending(i => i.CreatedDate).Take(1);
            }
            catch (Exception)
            {
                return Enumerable.Empty<BannerImage>().AsQueryable();
            }
        }

        public IQueryable<BannerImage> GetAllFAQBannerImage()
        {
            try
            {
                return _bannerImageRepository.GetAll(i => i.IsActive == true && i.IsDeleted == false && i.ControllerName == "FAQ").OrderByDescending(i => i.CreatedDate).Take(1);
            }
            catch (Exception)
            {
                return Enumerable.Empty<BannerImage>().AsQueryable();
            }
        }

        public IQueryable<BannerImage> GetAllForAdminAsync()
        {
            try
            {
                var data = _bannerImageRepository.GetAll();
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<BannerImage>().AsQueryable();
            }
        }

        public async Task<IEnumerable<BannerImage>> GetAllForSignalRAsync()
        {
            try
            {
                var data = await _bannerImageRepository.GetAllAsync();
                return data.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<BannerImage>();
            }
        }

        public IQueryable<BannerImage> GetAllHowItWorksBannerImage()
        {
            try
            {
                return _bannerImageRepository.GetAll(i => i.IsActive == true && i.IsDeleted == false && i.ControllerName == "HowItWork").OrderByDescending(i => i.CreatedDate).Take(1);
            }
            catch (Exception)
            {
                return Enumerable.Empty<BannerImage>().AsQueryable();
            }
        }

        public IQueryable<BannerImage> GetAllInvestorBannerImage()
        {
            try
            {
                return _bannerImageRepository.GetAll(i => i.IsActive == true && i.IsDeleted == false && i.ControllerName == "Investor").OrderByDescending(i => i.CreatedDate).Take(1);
            }
            catch (Exception)
            {
                return Enumerable.Empty<BannerImage>().AsQueryable();
            }
        }

        public IQueryable<BannerImage> GetAllKvkkBannerImage()
        {
            try
            {
                return _bannerImageRepository.GetAll(i => i.IsActive == true && i.IsDeleted == false && i.ControllerName == "Kvkk").OrderByDescending(i => i.CreatedDate).Take(1);
            }
            catch (Exception)
            {
                return Enumerable.Empty<BannerImage>().AsQueryable();
            }
        }

        public IQueryable<BannerImage> GetAllNewsBannerImage()
        {
            try
            {
                return _bannerImageRepository.GetAll(i => i.IsActive == true && i.IsDeleted == false && i.ControllerName == "News").OrderByDescending(i => i.CreatedDate).Take(1);
            }
            catch (Exception)
            {
                return Enumerable.Empty<BannerImage>().AsQueryable();
            }
        }

        public IQueryable<BannerImage> GetAllSector400BannerImage()
        {
            try
            {
                return _bannerImageRepository.GetAll(i => i.IsActive == true && i.IsDeleted == false && i.ControllerName == "400").OrderByDescending(i => i.CreatedDate).Take(1);
            }
            catch (Exception)
            {
                return Enumerable.Empty<BannerImage>().AsQueryable();
            }
        }

        public IQueryable<BannerImage> GetAllSector404BannerImage()
        {
            try
            {
                return _bannerImageRepository.GetAll(i => i.IsActive == true && i.IsDeleted == false && i.ControllerName == "404").OrderByDescending(i => i.CreatedDate).Take(1);
            }
            catch (Exception)
            {
                return Enumerable.Empty<BannerImage>().AsQueryable();
            }
        }

        public IQueryable<BannerImage> GetAllSector500BannerImage()
        {
            try
            {
                return _bannerImageRepository.GetAll(i => i.IsActive == true && i.IsDeleted == false && i.ControllerName == "500").OrderByDescending(i => i.CreatedDate).Take(1);
            }
            catch (Exception)
            {
                return Enumerable.Empty<BannerImage>().AsQueryable();
            }
        }

        public IQueryable<BannerImage> GetAllSectorNewsBannerImage()
        {
            try
            {
                return _bannerImageRepository.GetAll(i => i.IsActive == true && i.IsDeleted == false && i.ControllerName == "Sector News").OrderByDescending(i => i.CreatedDate).Take(1);
            }
            catch (Exception)
            {
                return Enumerable.Empty<BannerImage>().AsQueryable();
            }
        }

        public IQueryable<BannerImage> GetAllServicesBannerImage()
        {
            try
            {
                return _bannerImageRepository.GetAll(i => i.IsActive == true && i.IsDeleted == false && i.ControllerName == "Our Services").OrderByDescending(i => i.CreatedDate).Take(1);
            }
            catch (Exception)
            {
                return Enumerable.Empty<BannerImage>().AsQueryable();
            }
        }

        public IQueryable<BannerImage> GetAllSurveyBannerImage()
        {
            try
            {
                return _bannerImageRepository.GetAll(i => i.IsActive == true && i.IsDeleted == false && i.ControllerName == "Survey").OrderByDescending(i => i.CreatedDate).Take(1);
            }
            catch (Exception)
            {
                return Enumerable.Empty<BannerImage>().AsQueryable();
            }
        }

        public async Task<BannerImage> GetByIdAsync(int? id)
        {
            try
            {
                return await _bannerImageRepository.GetAsync(i => i.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _bannerImageRepository.SetActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _bannerImageRepository.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _bannerImageRepository.SetNotDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _bannerImageRepository.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> UpdateAsync(BannerImage entity, IFormFile image)
        {
            try
            {
                var errors = new List<string>();
                if (image != null)
                {
                    var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/img/banner/");
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
                        entity.Image = fileName;
                        entity.UpdatedDate = DateTime.UtcNow;
                        var result = await _bannerImageRepository.UpdateAsync(entity);
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
    }
}
