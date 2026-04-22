using Investigation.Business.Services.Abstract;
using Investigation.ServerHub.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Investigation.ServerHub.Hubs
{
    [Authorize(AuthenticationSchemes = "ApiKey")]
    public class AdHub : Hub
    {
        private readonly IAdService _adService;

        public AdHub(IAdService adService)
        {
            _adService = adService;
        }

        public async Task<IEnumerable<AdDto>> GetAllIncludingAsync()
        {
            try
            {
                var data = await _adService.GetAllForSignalRAsync();
                if (data != null)
                {
                    return data.Select(ad => new AdDto
                    {
                        Id = ad.Id,
                        CompanyName = ad.CompanyName,
                        Text = ad.Text,
                        StartDate = ad.StartDate,
                        FinishDate = ad.FinishDate,
                        ImageUrl = ad.ImageUrl,
                        RedirectUrl = ad.RedirectUrl,
                        NonUniqueHit = ad.NonUniqueHit,
                        HasTarget = ad.HasTarget,
                        CreatedDate = ad.CreatedDate,
                        UpdatedDate = ad.UpdatedDate,
                        SuspendedDate = ad.SuspendedDate,
                        DeletedDate = ad.DeletedDate,
                        IsActive = ad.IsActive,
                        IsDeleted = ad.IsDeleted,
                        AdTargetCount = ad.AdTargets?.Count ?? 0,
                        HitCount = ad.Hits?.Count ?? 0
                    }).ToList();
                }
                return new List<AdDto>();
            }
            catch (Exception)
            {
                return new List<AdDto>();
            }
        }
    }
}