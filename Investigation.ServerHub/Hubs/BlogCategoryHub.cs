using Investigation.Business.Services.Abstract;
using Investigation.ServerHub.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Investigation.ServerHub.Hubs
{
    [Authorize(AuthenticationSchemes = "ApiKey")]
    public class BlogCategoryHub : Hub
    {
        readonly IBlogCategoryService _blogCategoryService;
        public BlogCategoryHub(IBlogCategoryService blogCategoryService)
        {
            _blogCategoryService = blogCategoryService;
        }
        public async Task<IEnumerable<BlogCategoryDto>> GetAllIncludingAsync()
        {
            try
            {
                var data = await _blogCategoryService.GetAllForSignalRAsync();
                if (data != null)
                {
                    return data.Select(i => new BlogCategoryDto
                    {
                        Id = i.Id,
                        Name = i.Name,
                        BlogCount = i.Blogs?.Count ?? 0,
                        CreatedDate = i.CreatedDate,
                        UpdatedDate = i.UpdatedDate,
                        SuspendedDate = i.SuspendedDate,
                        DeletedDate = i.DeletedDate,
                        IsActive = i.IsActive,
                        IsDeleted = i.IsDeleted
                    }).ToList();
                }
                return new List<BlogCategoryDto>();
            }
            catch (Exception)
            {
                return new List<BlogCategoryDto>();
            }
        }
    }
}
