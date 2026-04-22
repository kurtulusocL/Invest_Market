using Investigation.Business.Services.Abstract;
using Investigation.ServerHub.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Investigation.ServerHub.Hubs
{
    [Authorize(AuthenticationSchemes = "ApiKey")]
    public class SectorNewsHub : Hub
    {
        readonly ISectorNewsService _sectorNewsService;
        public SectorNewsHub(ISectorNewsService sectorNewsService)
        {
            _sectorNewsService = sectorNewsService;
        }
        public async Task<IEnumerable<SectorNewsDto>> GetAllIncludingAsync()
        {
            try
            {
                var data = await _sectorNewsService.GetAllForSignalRAsync();
                if (data != null)
                {
                    return data.Select(i => new SectorNewsDto
                    {
                        Id = i.Id,
                        Title = i.Title,
                        Subtitle = i.Subtitle,
                        Desc = i.Desc,
                        Detail = i.Detail,
                        RedirectUrl = i.RedirectUrl,
                        Source = i.Source,
                        ImageUrl = i.ImageUrl,
                        Like = i.Like,
                        Hit = i.Hit,
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
                return new List<SectorNewsDto>();
            }
            catch (Exception)
            {
                return new List<SectorNewsDto>();
            }
        }
    }
}
