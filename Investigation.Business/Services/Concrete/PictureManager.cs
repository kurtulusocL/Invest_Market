using System.Linq.Expressions;
using Investigation.Business.Constants.Helpers;
using Investigation.Business.Services.Abstract;
using Investigation.DataAccess.Abstract;
using Investigation.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Investigation.Business.Services.Concrete
{
    public class PictureManager : IPictureService
    {
        readonly IPictureRepository _pictureRepository;
        public PictureManager(IPictureRepository pictureRepository)
        {
            _pictureRepository = pictureRepository;
        }

        public async Task<bool> CreateBlogPictureAsync(int? blogId, IEnumerable<IFormFile> images)
        {
            try
            {
                if (blogId == null)
                    throw new ArgumentNullException(nameof(blogId), "blogId was null");

                if (images != null)
                {
                    foreach (var file in images)
                    {
                        ServiceImageHelper.ImageValidation(file);
                        try
                        {
                            string savedFileName = await ServiceImageHelper.MultipleBlogImageResize(file);
                            var model = new Picture
                            {
                                BlogId = blogId,
                                ImageUrl = savedFileName,
                            };
                            var result = await _pictureRepository.AddAsync(model);
                            if (!result)
                            {
                                return false;
                            }
                        }
                        catch (Exception)
                        {
                            return false;
                        }
                    }
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding Blog Images the entity.", ex);
            }
        }

        public async Task<bool> CreateCompanyPictureAsync(int? companyId, IEnumerable<IFormFile> images)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                if (images != null)
                {
                    foreach (var file in images)
                    {
                        ServiceImageHelper.ImageValidation(file);
                        try
                        {
                            string savedFileName = await ServiceImageHelper.MultipleCompanyImageResize(file);

                            var model = new Picture
                            {
                                CompanyId = companyId,
                                ImageUrl = savedFileName
                            };
                            var result = await _pictureRepository.AddAsync(model);
                            if (!result)
                            {
                                return false;
                            }
                        }
                        catch (Exception)
                        {
                            return false;
                        }
                    }
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding Company Images the entity.", ex);
            }
        }

        public async Task<bool> CreatePostPictureAsync(int? postId, IEnumerable<IFormFile> images)
        {
            try
            {
                if (postId == null)
                    throw new ArgumentNullException(nameof(postId), "postId was null");

                if (images != null)
                {
                    foreach (var file in images)
                    {
                        ServiceImageHelper.ImageValidation(file);
                        try
                        {
                            string savedFileName = await ServiceImageHelper.MultiplePostImageResize(file);

                            var model = new Picture
                            {
                                PostId = postId,
                                ImageUrl = savedFileName
                            };

                            var result = await _pictureRepository.AddAsync(model);
                            if (!result)
                            {
                                return false;
                            }
                        }
                        catch (Exception)
                        {
                            return false;
                        }
                    }
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding Post Images the entity.", ex);
            }
        }

        public async Task<bool> DeleteAllByIdAsync(List<int> ids)
        {
            try
            {
                if (ids == null || !ids.Any())
                    throw new ArgumentNullException(nameof(ids), "id list was null or empty");

                var result = await _pictureRepository.DeleteByIdsAsync(ids.Cast<object>());
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while bulk deleting entities.", ex);
            }
        }

        public async Task<bool> DeleteAsync(Picture entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var data = await _pictureRepository.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _pictureRepository.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public IQueryable<Picture> GetAllIncludingAsync()
        {
            try
            {
                var data =  _pictureRepository.GetAllInclude(new Expression<Func<Picture, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Blog, y => y.Company, y => y.Post);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Picture>().AsQueryable();
            }
        }

        public IQueryable<Picture> GetAllIncludingPostIdAsync(int? postId)
        {
            try
            {
                if (postId == null)
                    throw new ArgumentNullException(nameof(postId), "postId was null");

                var data =  _pictureRepository.GetAllIncludeById(postId, "PostId", new Expression<Func<Picture, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Blog, y => y.Company, y => y.Post);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Picture>().AsQueryable();
            }
        }

        public IQueryable<Picture> GetAllIncludingByBlogIdAsync(int? blogId)
        {
            try
            {
                if (blogId == null)
                    throw new ArgumentNullException(nameof(blogId), "blogId was null");

                var data =  _pictureRepository.GetAllIncludeById(blogId, "BlogId", new Expression<Func<Picture, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Blog, y => y.Company, y => y.Post);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Picture>().AsQueryable();
            }
        }

        public IQueryable<Picture> GetAllIncludingByCompanyIdAsync(int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                var data =  _pictureRepository.GetAllIncludeById(companyId, "CompanyId", new Expression<Func<Picture, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Blog, y => y.Company, y => y.Post);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Picture>().AsQueryable();
            }
        }

        public IQueryable<Picture> GetAllIncludingForAdminAsync()
        {
            try
            {
                var data =  _pictureRepository.GetAllInclude(new Expression<Func<Picture, bool>>[]
                {

                }, null, y => y.Blog, y => y.Company, y => y.Post);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Picture>().AsQueryable();
            }
        }

        public async Task<Picture> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                return await _pictureRepository.GetIncludeAsync(i => i.Id == id, y => y.Blog, y => y.Company, y => y.Post);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _pictureRepository.SetActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _pictureRepository.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _pictureRepository.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _pictureRepository.SetNotDeletedAsync(id);
            return result;
        }

        public async Task<bool> UpdateBlogPictureAsync(int? blogId, IFormFile image, int id)
        {
            try
            {
                if (blogId == null)
                    throw new ArgumentNullException(nameof(blogId), "blogId was null");

                if (image != null && image.Length > 0)
                {
                    ServiceImageHelper.ImageValidation(image);
                    try
                    {
                        string savedFileName = await ServiceImageHelper.MultipleBlogImageResize(image);
                        var entity = await _pictureRepository.GetIncludeAsync(i => i.Id == id);
                        entity.BlogId = blogId;
                        entity.ImageUrl = savedFileName;
                        //Id = id,
                        entity.UpdatedDate = DateTime.UtcNow;
                        //var entity = new Picture
                        //{

                        //};

                        var result = await _pictureRepository.UpdateAsync(entity);
                        if (!result)
                        {
                            return false;
                        }
                        return true;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while updating Blog Image the entity.", ex);
            }
        }

        public async Task<bool> UpdateCompanyPictureAsync(int? companyId, IFormFile image, int id)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                if (image != null && image.Length > 0)
                {
                    ServiceImageHelper.ImageValidation(image);
                    try
                    {
                        string savedFileName = await ServiceImageHelper.MultipleCompanyImageResize(image);

                        var entity = await _pictureRepository.GetIncludeAsync(i => i.Id == id);
                        entity.CompanyId = companyId;
                        entity.ImageUrl = savedFileName;
                        entity.UpdatedDate = DateTime.UtcNow;

                        var result = await _pictureRepository.UpdateAsync(entity);
                        if (!result)
                        {
                            return false;
                        }
                        return true;
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while updating Company Image the entity.", ex);
            }
        }

        public async Task<bool> UpdatePostPictureAsync(int? postId, IFormFile image, int id)
        {
            try
            {
                if (postId == null)
                    throw new ArgumentNullException(nameof(postId), "postId was null");

                if (image != null && image.Length > 0)
                {
                    ServiceImageHelper.ImageValidation(image);
                    try
                    {
                        string savedFileName = await ServiceImageHelper.MultiplePostImageResize(image);

                        var entity = await _pictureRepository.GetIncludeAsync(i => i.Id == id);
                        entity.PostId = postId;
                        entity.ImageUrl = savedFileName;
                        entity.UpdatedDate = DateTime.UtcNow;

                        var result = await _pictureRepository.UpdateAsync(entity);
                        if (!result)
                        {
                            return false;
                        }
                        return true;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while updating Post Image the entity.", ex);
            }
        }

        public IQueryable<Picture> GetAllIncludingBlogPictureForInvestorByBlogIdAsync(int? blogId)
        {
            try
            {
                if (blogId == null)
                    throw new ArgumentNullException(nameof(blogId), "blogId was null");

                var data =  _pictureRepository.GetAllIncludeById(blogId, "BlogId", new Expression<Func<Picture, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Blog);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Picture>().AsQueryable();
            }
        }

        public IQueryable<Picture> GetAllIncludingPostPictureForInvestorByPostIdAsync(int? postId)
        {
            try
            {
                if (postId == null)
                    throw new ArgumentNullException(nameof(postId), "postId was null");

                var data =  _pictureRepository.GetAllIncludeById(postId, "PostId", new Expression<Func<Picture, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Post);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Picture>().AsQueryable();
            }
        }

        public IQueryable<Picture> GetAllIncludingBlogPictureForCompanyByBlogIdAsync(int? blogId)
        {
            try
            {
                if (blogId == null)
                    throw new ArgumentNullException(nameof(blogId), "blogId was null");

                var data =  _pictureRepository.GetAllIncludeById(blogId, "BlogId", new Expression<Func<Picture, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Company);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Picture>().AsQueryable();
            }
        }

        public IQueryable<Picture> GetAllIncludingPostPictureForCompanyByPostIdAsync(int? postId)
        {
            try
            {
                if (postId == null)
                    throw new ArgumentNullException(nameof(postId), "postId was null");

                var data =  _pictureRepository.GetAllIncludeById(postId, "PostId", new Expression<Func<Picture, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Post);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Picture>().AsQueryable();
            }
        }

        public IQueryable<Picture> GetAllIncludingCompanyPictureForCompanyByCompanyIdAsync(int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                var data =  _pictureRepository.GetAllIncludeById(companyId, "CompanyId", new Expression<Func<Picture, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Company);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Picture>().AsQueryable();
            }
        }

        public IQueryable<Picture> GetAllIncludingBlogPictureForInvestorByBlogId(int? blogId)
        {
            try
            {
                if (blogId == null)
                    throw new ArgumentNullException(nameof(blogId), "blogId was null");

                return _pictureRepository.GetAllIncludeById(blogId, "BlogId", new Expression<Func<Picture, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Blog).OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Picture>().AsQueryable();
            }
        }

        public IQueryable<Picture> GetAllIncludingPostPictureForInvestorByPostId(int? postId)
        {
            try
            {
                if (postId == null)
                    throw new ArgumentNullException(nameof(postId), "postId was null");

                return _pictureRepository.GetAllIncludeById(postId, "PostId", new Expression<Func<Picture, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Post).OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Picture>().AsQueryable();
            }
        }

        public IQueryable<Picture> GetAllIncludingBlogPictureForCompanyByBlogId(int? blogId)
        {
            try
            {
                if (blogId == null)
                    throw new ArgumentNullException(nameof(blogId), "blogId was null");

                return _pictureRepository.GetAllIncludeById(blogId, "BlogId", new Expression<Func<Picture, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Blog).OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Picture>().AsQueryable();
            }
        }

        public IQueryable<Picture> GetAllIncludingPostPictureForCompanyByPostId(int? postId)
        {
            try
            {
                if (postId == null)
                    throw new ArgumentNullException(nameof(postId), "postId was null");

                return _pictureRepository.GetAllIncludeById(postId, "PostId", new Expression<Func<Picture, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Post).OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Picture>().AsQueryable();
            }
        }

        public IQueryable<Picture> GetAllIncludingCompanyPictureForCompanyByCompanyId(int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                return _pictureRepository.GetAllIncludeById(companyId, "CompanyId", new Expression<Func<Picture, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Company).OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Picture>().AsQueryable();
            }
        }

        public IQueryable<Picture> GetAllCompanyImageRandomForCompanyCoverImageByUserId(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                return _pictureRepository.GetAllInclude(new Expression<Func<Picture, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Company.AppUserId==userId
                }, y => y.Company, y => y.Company.Hits).AsEnumerable().OrderByDescending(i => Guid.NewGuid()).Take(1).AsQueryable();
            }
            catch (Exception)
            {
                return Enumerable.Empty<Picture>().AsQueryable();
            }
        }

        public async Task<IEnumerable<Picture>> GetAllForSignalRAsync()
        {
            try
            {
                var data = await _pictureRepository.GetAllIncludeAsync(new Expression<Func<Picture, bool>>[]
                {
                    
                }, null, y => y.Blog, y => y.Company, y => y.Post);
                return data.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Picture>();
            }
        }
    }
}
