using Investigation.Business.Services.Abstract;
using Investigation.ServerHub.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Investigation.ServerHub.Hubs
{
    [Authorize(AuthenticationSchemes = "ApiKey")]
    public class CommentAnswerHub : Hub
    {
        readonly ICommentAnswerService _commentAnswerService;
        public CommentAnswerHub(ICommentAnswerService commentAnswerService)
        {
            _commentAnswerService = commentAnswerService;
        }
        public async Task<IEnumerable<CommentAnswerDto>> GetAllIncludingAsync()
        {
            try
            {
                var data = await _commentAnswerService.GetAllForSignalRAsync();
                if (data != null)
                {
                    return data.Select(i => new CommentAnswerDto
                    {
                        Id = i.Id,
                        Text = i.Text,
                        AppUserDtoId = i.AppUserId,
                        CommentDtoId = i.CommentId,
                        HitCount = i.Hits?.Count ?? 0,
                        LikeCount = i.Likes?.Count ?? 0,
                        ReportCount = i.Reports?.Count ?? 0,
                        CreatedDate = i.CreatedDate,
                        UpdatedDate = i.UpdatedDate,
                        SuspendedDate = i.SuspendedDate,
                        DeletedDate = i.DeletedDate,
                        IsActive = i.IsActive,
                        IsDeleted = i.IsDeleted
                    }).ToList();
                }
                return new List<CommentAnswerDto>();
            }
            catch (Exception)
            {
                return new List<CommentAnswerDto>();
            }
        }
    }
}
