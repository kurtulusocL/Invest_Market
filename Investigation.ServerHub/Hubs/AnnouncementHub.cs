using Investigation.Business.Services.Abstract;
using Investigation.ServerHub.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Investigation.ServerHub.Hubs
{
    [Authorize(AuthenticationSchemes = "ApiKey")]
    public class AnnouncementHub : Hub
    {
        readonly IAnnouncementService _announcementService;
        public AnnouncementHub(IAnnouncementService announcementService)
        {
            _announcementService = announcementService;
        }
        public async Task<IEnumerable<AnnouncementDto>> GetAllIncludingAsync()
        {
            try
            {
                var data = await _announcementService.GetAllForSignalRAsync();
                if (data != null)
                {
                    return data.Select(i => new AnnouncementDto
                    {
                        Id = i.Id,
                        Title = i.Title,
                        Subtitle = i.Subtitle,
                        Content = i.Content,
                        ImageUrl = i.ImageUrl,
                        HitCount = i.Hits?.Count ?? 0,
                        ReportCount = i.Reports?.Count ?? 0,
                        AnnouncementCategoryDtoId = i.AnnouncementCategoryId,
                        InvestorDtoId = i.InvestorId,
                        CompanyDtoId = i.CompanyId,
                        CreatedDate = i.CreatedDate,
                        UpdatedDate = i.UpdatedDate,
                        SuspendedDate = i.SuspendedDate,
                        DeletedDate = i.DeletedDate,
                        IsActive = i.IsActive,
                        IsDeleted = i.IsDeleted
                    }).ToList();
                }
                return new List<AnnouncementDto>();
            }
            catch (Exception)
            {
                return new List<AnnouncementDto>();
            }
        }
    }
}
