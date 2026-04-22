using Investigation.Business.Services.Abstract;
using Investigation.ServerHub.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Investigation.ServerHub.Hubs
{
    [Authorize(AuthenticationSchemes = "ApiKey")]
    public class PostHub : Hub
    {
        readonly IPostService _postService;
        public PostHub(IPostService postService)
        {
            _postService = postService;
        }
        public async Task<IEnumerable<PostDto>> GetAllIncludingAsync()
        {
            try
            {
                var data = await _postService.GetAllForSignalRAsync();
                if (data != null)
                {
                    return data.Select(i => new PostDto
                    {
                        Id = i.Id,
                        Text = i.Text,
                        ImageUrl = i.ImageUrl,
                        IsCommentable = i.IsCommentable,
                        AppUserDtoId = i.AppUserId,
                        CompanyDtoId = i.CompanyId,
                        InvestorDtoId = i.InvestorId,
                        CommentCount = i.Comments?.Count ?? 0,
                        HitCount = i.Hits?.Count ?? 0,
                        LikeCount = i.Likes?.Count ?? 0,
                        PictureCount = i.Pictures?.Count ?? 0,
                        ReportCount = i.Reports?.Count ?? 0,
                        SavedContentCount = i.SavedContents?.Count ?? 0,
                        CreatedDate = i.CreatedDate,
                        UpdatedDate = i.UpdatedDate,
                        SuspendedDate = i.SuspendedDate,
                        DeletedDate = i.DeletedDate,
                        IsActive = i.IsActive,
                        IsDeleted = i.IsDeleted
                    }).ToList();
                }
                return new List<PostDto>();
            }
            catch (Exception)
            {
                return new List<PostDto>();
            }
        }
    }
}
