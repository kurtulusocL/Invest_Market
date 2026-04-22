using Investigation.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Investigation.Business.Services.Abstract
{
    public interface IPictureService
    {
        IQueryable<Picture> GetAllIncludingAsync();
        IQueryable<Picture> GetAllIncludingByBlogIdAsync(int? blogId);
        IQueryable<Picture> GetAllIncludingByCompanyIdAsync(int? companyId);
        IQueryable<Picture> GetAllIncludingPostIdAsync(int? postId);
        IQueryable<Picture> GetAllIncludingForAdminAsync();
        IQueryable<Picture> GetAllIncludingBlogPictureForInvestorByBlogIdAsync(int? blogId);
        IQueryable<Picture> GetAllIncludingPostPictureForInvestorByPostIdAsync(int? postId);
        IQueryable<Picture> GetAllIncludingBlogPictureForCompanyByBlogIdAsync(int? blogId);
        IQueryable<Picture> GetAllIncludingPostPictureForCompanyByPostIdAsync(int? postId);
        IQueryable<Picture> GetAllIncludingCompanyPictureForCompanyByCompanyIdAsync(int? companyId);
        Task<IEnumerable<Picture>> GetAllForSignalRAsync();
        Task<Picture> GetByIdAsync(int? id);
        Task<bool> CreateBlogPictureAsync(int? blogId, IEnumerable<IFormFile> images);
        Task<bool> CreateCompanyPictureAsync(int? companyId, IEnumerable<IFormFile> images);
        Task<bool> CreatePostPictureAsync(int? postId, IEnumerable<IFormFile> images);
        Task<bool> UpdateBlogPictureAsync(int? blogId, IFormFile image, int id);
        Task<bool> UpdateCompanyPictureAsync(int? companyId, IFormFile image, int id);
        Task<bool> UpdatePostPictureAsync(int? postId, IFormFile image, int id);
        Task<bool> DeleteAsync(Picture entity, int id);
        Task<bool> DeleteAllByIdAsync(List<int> ids);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        IQueryable<Picture> GetAllCompanyImageRandomForCompanyCoverImageByUserId(string userId);
        IQueryable<Picture> GetAllIncludingBlogPictureForInvestorByBlogId(int? blogId);
        IQueryable<Picture> GetAllIncludingPostPictureForInvestorByPostId(int? postId);
        IQueryable<Picture> GetAllIncludingBlogPictureForCompanyByBlogId(int? blogId);
        IQueryable<Picture> GetAllIncludingPostPictureForCompanyByPostId(int? postId);
        IQueryable<Picture> GetAllIncludingCompanyPictureForCompanyByCompanyId(int? companyId);
    }
}
