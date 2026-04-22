using Investigation.Business.Services.Abstract;
using Investigation.ServerHub.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Investigation.ServerHub.Hubs
{
    [Authorize(AuthenticationSchemes = "ApiKey")]
    public class ReportCategoryHub:Hub
    {
        readonly IReportCategoryService _reportCategoryService;
        public ReportCategoryHub(IReportCategoryService reportCategoryService)
        {
            _reportCategoryService = reportCategoryService;
        }
        public async Task<IEnumerable<ReportCategoryDto>> GetAllIncludingAsync()
        {
            try
            {
                var data = await _reportCategoryService.GetAllForSignalRAsync();
                if (data != null)
                {
                    return data.Select(i => new ReportCategoryDto
                    {
                        Id = i.Id,
                        Name = i.Name,
                        ReportCount = i.Reports?.Count ?? 0,
                        CreatedDate = i.CreatedDate,
                        UpdatedDate = i.UpdatedDate,
                        SuspendedDate = i.SuspendedDate,
                        DeletedDate = i.DeletedDate,
                        IsActive = i.IsActive,
                        IsDeleted = i.IsDeleted
                    }).ToList();
                }
                return new List<ReportCategoryDto>();
            }
            catch (Exception)
            {
                return new List<ReportCategoryDto>();
            }
        }
    }
}
