using Investigation.Business.Services.Abstract;
using Investigation.ServerHub.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Investigation.ServerHub.Hubs
{
    [Authorize(AuthenticationSchemes = "ApiKey")]
    public class LayoutInfoHub : Hub
    {
        readonly ILayoutInfoService _layoutInfoService;
        public LayoutInfoHub(ILayoutInfoService layoutInfoService)
        {
            _layoutInfoService = layoutInfoService;
        }
        public async Task<IEnumerable<LayoutInfoDto>> GetAllAsync()
        {
            try
            {
                var data = await _layoutInfoService.GetAllForSignalRAsync();
                if (data != null)
                {
                    return data.Select(i => new LayoutInfoDto
                    {
                        Id = i.Id,
                        Title = i.Title,
                        Author = i.Author,
                        Keyword = i.Keyword,
                        Content = i.Content,
                        Language = i.Language,
                        CreatedDate = i.CreatedDate,
                        UpdatedDate = i.UpdatedDate,
                        SuspendedDate = i.SuspendedDate,
                        DeletedDate = i.DeletedDate,
                        IsActive = i.IsActive,
                        IsDeleted = i.IsDeleted
                    }).ToList();
                }
                return new List<LayoutInfoDto>();
            }
            catch (Exception)
            {
                return new List<LayoutInfoDto>();
            }
        }
    }
}
