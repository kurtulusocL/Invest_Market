using Investigation.Business.Services.Abstract;
using Investigation.ServerHub.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Investigation.ServerHub.Hubs
{
    [Authorize(AuthenticationSchemes = "ApiKey")]
    public class CommentHub : Hub
    {
        readonly ICommentService _commentService;
        public CommentHub(ICommentService commentService)
        {
            _commentService = commentService;
        }
        public async Task<IEnumerable<CommentDto>> GetAllIncludingAsync()
        {
            try
            {
                var data = await _commentService.GetAllForSignalRAsync();
                if (data != null)
                {
                    return data.Select(i => new CommentDto
                    {
                        Id = i.Id,
                        Text = i.Text,
                        AppUserDtoId = i.AppUserId,
                        BlogDtoId = i.BlogId,
                        CompanyDtoId = i.CompanyId,
                        PostDtoId = i.PostId,
                        HitCount = i.Hits?.Count ?? 0,
                        LikeCount = i.Likes?.Count ?? 0,
                        CommentAnswerCount = i.CommentAnswers?.Count ?? 0,
                        ReportCount = i.Reports?.Count ?? 0,
                        CreatedDate = i.CreatedDate,
                        UpdatedDate = i.UpdatedDate,
                        SuspendedDate = i.SuspendedDate,
                        DeletedDate = i.DeletedDate,
                        IsActive = i.IsActive,
                        IsDeleted = i.IsDeleted
                    }).ToList();
                }
                return new List<CommentDto>();
            }
            catch (Exception)
            {
                return new List<CommentDto>();
            }
        }
    }
}
