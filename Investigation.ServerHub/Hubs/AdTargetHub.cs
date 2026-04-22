using Investigation.Business.Services.Abstract;
using Investigation.ServerHub.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Investigation.ServerHub.Hubs
{
    [Authorize(AuthenticationSchemes = "ApiKey")]
    public class AdTargetHub : Hub
    {
        readonly IAdTargetService _adTargetService;
        public AdTargetHub(IAdTargetService adTargetService)
        {
            _adTargetService = adTargetService;
        }
        public async Task<IEnumerable<AdTargetDto>> GetAllIncludingAdTargetAsync()
        {
            try
            {
                var data = await _adTargetService.GetAllForSignalRAsync();
                if (data != null)
                {
                    return data.Select(i => new AdTargetDto
                    {
                        Id = i.Id,
                        AdDtoId = i.AdId,
                        MinAge = i.MinAge,
                        MaxAge = i.MaxAge,
                        TargetCountries = i.TargetCountries,
                        TargetCategoryType = i.TargetCategoryType,
                        TargetCategoryIdsJson = i.TargetCategoryIdsJson,
                        TargetCategoryIds = i.TargetCategoryIds,
                        MinInteractionCount = i.MinInteractionCount,
                        MinTotalLikeCount = i.MinTotalLikeCount,
                        MinTotalSaveCount = i.MinTotalSaveCount,
                        MinTotalViewCount = i.MinTotalViewCount,
                        IncludeBlogInteractions = i.IncludeBlogInteractions,
                        IncludeInvestorInteractions = i.IncludeInvestorInteractions,
                        IncludeCompanyInteractions = i.IncludeCompanyInteractions,
                        IncludePostInteractions = i.IncludePostInteractions,
                        CreatedDate = i.CreatedDate,
                        UpdatedDate = i.UpdatedDate,
                        SuspendedDate = i.SuspendedDate,
                        DeletedDate = i.DeletedDate,
                        IsActive = i.IsActive,
                        IsDeleted = i.IsDeleted
                    }).ToList();
                }
                return new List<AdTargetDto>();
            }
            catch (Exception)
            {
                return new List<AdTargetDto>();
            }
        }
    }
}
