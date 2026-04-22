using Investigation.Business.Services.Abstract;
using Investigation.ServerHub.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Investigation.ServerHub.Hubs
{
    [Authorize(AuthenticationSchemes = "ApiKey")]
    public class AnnouncementCategoryHub : Hub
    {
        readonly IAnnouncementCategoryService _announcementCategoryService;
        public AnnouncementCategoryHub(IAnnouncementCategoryService announcementCategoryService)
        {
            _announcementCategoryService = announcementCategoryService;
        }
        public async Task<IEnumerable<AnnouncementCategoryDto>> GetAllIncludingAsync()
        {
            try
            {
                var data = await _announcementCategoryService.GetAllForSignalRAsync();
                if (data != null)
                {
                    return data.Select(i => new AnnouncementCategoryDto
                    {
                        Id = i.Id,
                        Name = i.Name,
                        AnnouncementCount = i.Announcements?.Count ?? 0,
                        CreatedDate = i.CreatedDate,
                        UpdatedDate = i.UpdatedDate,
                        SuspendedDate = i.SuspendedDate,
                        DeletedDate = i.DeletedDate,
                        IsActive = i.IsActive,
                        IsDeleted = i.IsDeleted
                    }).ToList();
                }
                return new List<AnnouncementCategoryDto>();
            }
            catch (Exception)
            {
                return new List<AnnouncementCategoryDto>();
            }
        }
    }
}
