using Investigation.Business.Services.Abstract;
using Investigation.Domain.Entities;
using Investigation.ServerHub.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Investigation.ServerHub.Hubs
{
    [Authorize(AuthenticationSchemes = "ApiKey")]
    public class BlogHub : Hub
    {
        readonly IBlogService _blogService;
        public BlogHub(IBlogService blogService)
        {
            _blogService = blogService;
        }
        public async Task<IEnumerable<BlogDto>> GetAllIncludingAsync()
        {
            try
            {
                var data = await _blogService.GetAllForSignalRAsync();
                if (data != null)
                {
                    return data.Select(i => new BlogDto
                    {
                        Id = i.Id,
                        Title = i.Title,
                        Subtitle = i.Subtitle,
                        Detail = i.Detail,
                        Content = i.Content,
                        CoverImage = i.CoverImage,
                        CommentCount = i.Comments != null ? i.Comments.Count : 0,
                        HitCount = i.Hits != null ? i.Hits.Count : 0,
                        LikeCount = i.Likes != null ? i.Likes.Count : 0,
                        PictureCount = i.Pictures != null ? i.Pictures.Count : 0,
                        ReportCount = i.Reports != null ? i.Reports.Count : 0,
                        SavedContentCount = i.SavedContents != null ? i.SavedContents.Count : 0,
                        BlogCategoryDtoId = i.BlogCategoryId,
                        AppUserDtoId = i.AppUserId,
                        CompanyDtoId = i.CompanyId,
                        InvestorDtoId = i.InvestorId,
                        CreatedDate = i.CreatedDate,
                        UpdatedDate = i.UpdatedDate,
                        SuspendedDate = i.SuspendedDate,
                        DeletedDate = i.DeletedDate,
                        IsActive = i.IsActive,
                        IsDeleted = i.IsDeleted
                    }).ToList();
                }
                return new List<BlogDto>();
            }
            catch (Exception)
            {
                return new List<BlogDto>();
            }
        }
    }
}
