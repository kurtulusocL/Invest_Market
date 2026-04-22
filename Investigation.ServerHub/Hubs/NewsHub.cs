using Investigation.Business.Services.Abstract;
using Investigation.ServerHub.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Investigation.ServerHub.Hubs
{
    [Authorize(AuthenticationSchemes = "ApiKey")]
    public class NewsHub : Hub
    {
        readonly INewsService _newsService;
        public NewsHub(INewsService newsService)
        {
            _newsService = newsService;
        }
        public async Task<IEnumerable<NewsDto>> GetAllIncludingAsync()
        {
            try
            {
                var data = await _newsService.GetAllForSignalRAsync();
                if (data != null)
                {
                    return data.Select(i => new NewsDto
                    {
                        Id = i.Id,
                        Title = i.Title,
                        Subtitle = i.Subtitle,
                        Detail = i.Detail,
                        Desc = i.Desc,
                        ImageUrl = i.ImageUrl,
                        Hit = i.Hit,
                        Like = i.Like,
                        ReportCount = i.Reports?.Count ?? 0,
                        CreatedDate = i.CreatedDate,
                        UpdatedDate = i.UpdatedDate,
                        SuspendedDate = i.SuspendedDate,
                        DeletedDate = i.DeletedDate,
                        IsActive = i.IsActive,
                        IsDeleted = i.IsDeleted
                    }).ToList();
                }
                return new List<NewsDto>();
            }
            catch (Exception)
            {
                return new List<NewsDto>();
            }
        }
    }
}
