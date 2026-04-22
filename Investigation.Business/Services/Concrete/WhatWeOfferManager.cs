using Investigation.Business.Services.Abstract;
using Investigation.DataAccess.Abstract;
using Investigation.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Investigation.Business.Services.Concrete
{
    public class WhatWeOfferManager : IWhatWeOfferService
    {
        readonly IWhatWeOfferRepository _whatWeOfferRepository;
        public WhatWeOfferManager(IWhatWeOfferRepository whatWeOfferRepository)
        {
            _whatWeOfferRepository = whatWeOfferRepository;
        }

        public async Task<bool> CreateAsync(WhatWeOffer entity, IFormFile? image)
        {
            try
            {
                var errors = new List<string>();
                if (image != null)
                {
                    var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/img/whatweoffer/");
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
                        var result = await _whatWeOfferRepository.AddAsync(entity);
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
                else
                {
                    var result = await _whatWeOfferRepository.AddAsync(entity);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> DeleteAsync(WhatWeOffer entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "Entit was null");

                var data = await _whatWeOfferRepository.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _whatWeOfferRepository.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public IQueryable<WhatWeOffer> GetAllAsync()
        {
            try
            {
                var data = _whatWeOfferRepository.GetAll(i => i.IsActive == true && i.IsDeleted == false);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<WhatWeOffer>().AsQueryable();
            }
        }

        public IQueryable<WhatWeOffer> GetAllForAdminAsync()
        {
            try
            {
                var data = _whatWeOfferRepository.GetAll();
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<WhatWeOffer>().AsQueryable();
            }
        }

        public async Task<IEnumerable<WhatWeOffer>> GetAllForSignalRAsync()
        {
            try
            {
                var data = await _whatWeOfferRepository.GetAllAsync();
                return data.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<WhatWeOffer>();
            }
        }

        public IQueryable<WhatWeOffer> GetAllForSitemap()
        {
            try
            {
                return _whatWeOfferRepository.GetAll(i => i.IsActive == true && i.IsDeleted == false).OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<WhatWeOffer>().AsQueryable();
            }
        }

        public IQueryable<WhatWeOffer> GetAllWhatWeOfferForPublic()
        {
            try
            {
                return _whatWeOfferRepository.GetAll(i => i.IsActive == true && i.IsDeleted == false).OrderBy(i => i.CreatedDate).OrderBy(i => Guid.NewGuid()).Take(5);
            }
            catch (Exception)
            {
                return Enumerable.Empty<WhatWeOffer>().AsQueryable();
            }
        }

        public IQueryable<WhatWeOffer> GetAllWhatWeOfferForPublicHome()
        {
            try
            {
                return _whatWeOfferRepository.GetAll(i => i.IsActive == true && i.IsDeleted == false).OrderBy(i => i.CreatedDate).OrderBy(i => Guid.NewGuid()).Take(6);
            }
            catch (Exception)
            {
                return Enumerable.Empty<WhatWeOffer>().AsQueryable();
            }
        }

        public async Task<WhatWeOffer> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                return await _whatWeOfferRepository.GetAsync(i => i.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _whatWeOfferRepository.SetActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _whatWeOfferRepository.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _whatWeOfferRepository.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _whatWeOfferRepository.SetNotDeletedAsync(id);
            return result;
        }

        public async Task<bool> UpdateAsync(WhatWeOffer entity, IFormFile? image)
        {
            try
            {
                var errors = new List<string>();
                if (image != null)
                {
                    var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/img/whatweoffer/");
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
                        var result = await _whatWeOfferRepository.UpdateAsync(entity);
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
                else
                {
                    entity.UpdatedDate = DateTime.UtcNow;
                    var result = await _whatWeOfferRepository.UpdateAsync(entity);
                    return result;
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
