using Investigation.Business.Services.Abstract;
using Investigation.DataAccess.Abstract;
using Investigation.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Investigation.Business.Services.Concrete
{
    public class SliderManager : ISliderService
    {
        readonly ISliderRepository _sliderRepository;
        public SliderManager(ISliderRepository sliderRepository)
        {
            _sliderRepository = sliderRepository;
        }

        public async Task<bool> CreateAsync(Slider entity, IFormFile image)
        {
            try
            {
                var errors = new List<string>();
                if (image != null)
                {
                    var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/img/slider/");
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
                        var result = await _sliderRepository.AddAsync(entity);
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

        public async Task<bool> DeleteAllByIdAsync(List<int> ids)
        {
            try
            {
                if (ids == null || !ids.Any())
                    throw new ArgumentNullException(nameof(ids), "id list was null or empty");

                var result = await _sliderRepository.DeleteByIdsAsync(ids.Cast<object>());
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while bulk deleting entities.", ex);
            }
        }

        public async Task<bool> DeleteAsync(Slider entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entit was null");

                var data = await _sliderRepository.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _sliderRepository.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public IQueryable<Slider> GetAllAsync()
        {
            try
            {
                var data = _sliderRepository.GetAll(i => i.IsActive == true && i.IsDeleted == false);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Slider>().AsQueryable();
            }
        }

        public IQueryable<Slider> GetAllForAdminAsync()
        {
            try
            {
                var data = _sliderRepository.GetAll();
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Slider>().AsQueryable();
            }
        }

        public async Task<IEnumerable<Slider>> GetAllForSignalRAsync()
        {
            try
            {
                var data = await _sliderRepository.GetAllAsync();
                return data.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Slider>();
            }
        }

        public IQueryable<Slider> GetAllForSitemap()
        {
            try
            {
                return _sliderRepository.GetAll(i => i.IsActive == true && i.IsDeleted == false).OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Slider>().AsQueryable();
            }
        }

        public IQueryable<Slider> GetAllSliderRandom()
        {
            try
            {
                return _sliderRepository.GetAll(i => i.IsActive == true && i.IsDeleted == false).OrderByDescending(i => Guid.NewGuid());
            }
            catch (Exception)
            {
                return Enumerable.Empty<Slider>().AsQueryable();
            }
        }

        public async Task<Slider> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                return await _sliderRepository.GetAsync(i => i.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _sliderRepository.SetActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _sliderRepository.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _sliderRepository.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _sliderRepository.SetNotDeletedAsync(id);
            return result;
        }

        public async Task<bool> UpdateAsync(Slider entity, IFormFile image)
        {
            try
            {
                var errors = new List<string>();
                if (image != null)
                {
                    var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/img/slider/");
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
                        var result = await _sliderRepository.UpdateAsync(entity);
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
