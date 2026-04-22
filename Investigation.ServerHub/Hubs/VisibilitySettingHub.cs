using Investigation.Business.Services.Abstract;
using Investigation.ServerHub.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Investigation.ServerHub.Hubs
{
    [Authorize(AuthenticationSchemes = "ApiKey")]
    public class VisibilitySettingHub : Hub
    {
        readonly IVisibilitySettingService _visibilitySettingService;
        public VisibilitySettingHub(IVisibilitySettingService visibilitySettingService)
        {
            _visibilitySettingService = visibilitySettingService;
        }
        public async Task<IEnumerable<VisibilitySettingDto>> GetAllIncludingAsync()
        {
            try
            {
                var data = await _visibilitySettingService.GetAllForSignalRAsync();
                if (data != null)
                {
                    return data.Select(i => new VisibilitySettingDto
                    {
                        Id = i.Id,
                        IsVisibleForCompanies = i.IsVisibleForCompanies,
                        IsVisibleForInvestors = i.IsVisibleForInvestors,
                        IsVisibleForAll=i.IsVisibleForAll,
                        IsVisibleForNone = i.IsVisibleForNone,
                        CompanyFinanceDtoId = i.CompanyFinanceId,
                        CompanyPintechDtoId = i.CompanyPintechId,
                        CompanyStageDtoId = i.CompanyStageId,
                        CreatedDate = i.CreatedDate,
                        UpdatedDate = i.UpdatedDate,
                        SuspendedDate = i.SuspendedDate,
                        DeletedDate = i.DeletedDate,
                        IsActive = i.IsActive,
                        IsDeleted = i.IsDeleted
                    }).ToList();
                }
                return new List<VisibilitySettingDto>();
            }
            catch (Exception)
            {
                return new List<VisibilitySettingDto>();
            }
        }
    }
}
