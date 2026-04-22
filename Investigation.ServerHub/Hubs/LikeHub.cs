using Investigation.Business.Services.Abstract;
using Investigation.ServerHub.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Investigation.ServerHub.Hubs
{
    [Authorize(AuthenticationSchemes = "ApiKey")]
    public class LikeHub:Hub
    {
        readonly ILikeService _likeService;
        public LikeHub(ILikeService likeService)
        {
            _likeService = likeService;
        }
        public async Task<IEnumerable<LikeDto>> GetAllIncludingAsync()
        {
            try
            {
                var data = await _likeService.GetAllForSignalRAsync();
                if (data != null)
                {
                    return data.Select(i => new LikeDto
                    {
                        Id = i.Id,
                        CurrentValue = i.CurrentValue,
                        IsLiked = i.IsLiked,
                        AppUserDtoId = i.AppUserId,
                        BlogDtoId = i.BlogId,
                        CommentDtoId = i.CommentId,
                        CommentAnswerDtoId = i.CommentAnswerId,
                        CompanyDtoId = i.CompanyId,
                        InvestorDtoId = i.InvestorId,
                        PostDtoId = i.PostId,
                        SurveyDtoId = i.SurveyId,
                        CreatedDate = i.CreatedDate,
                        UpdatedDate = i.UpdatedDate,
                        SuspendedDate = i.SuspendedDate,
                        DeletedDate = i.DeletedDate,
                        IsActive = i.IsActive,
                        IsDeleted = i.IsDeleted
                    }).ToList();
                }
                return new List<LikeDto>();
            }
            catch (Exception)
            {
                return new List<LikeDto>();
            }
        }
    }
}
