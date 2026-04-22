using Investigation.Business.Services.Abstract;
using Investigation.ServerHub.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Investigation.ServerHub.Hubs
{
    [Authorize(AuthenticationSchemes = "ApiKey")]
    public class CancelMembershipHub : Hub
    {
        readonly ICancelMembershipService _cancelMembershipService;
        public CancelMembershipHub(ICancelMembershipService cancelMembershipService)
        {
            _cancelMembershipService = cancelMembershipService;
        }
        public async Task<IEnumerable<CancelMembershipDto>> GetAllIncludingAsync()
        {
            try
            {
                var data = await _cancelMembershipService.GetAllForSignalRAsync();
                if (data != null)
                {
                    return data.Select(i => new CancelMembershipDto
                    {
                        Id = i.Id,
                        Title = i.Title,
                        Desc = i.Desc,
                        IsRequestCancelled = i.IsRequestCancelled,
                        IsCancelled = i.IsCancelled,
                        CancelDate = i.CancelDate,
                        RequestCancelledDate = i.RequestCancelledDate,
                        Hit = i.Hit,
                        AppUserDtoId = i.AppUserId,
                        CancelMembershipCategoryDtoId = i.CancelMembershipCategoryId,
                        CreatedDate = i.CreatedDate,
                        UpdatedDate = i.UpdatedDate,
                        SuspendedDate = i.SuspendedDate,
                        DeletedDate = i.DeletedDate,
                        IsActive = i.IsActive,
                        IsDeleted = i.IsDeleted
                    }).ToList();
                }
                return new List<CancelMembershipDto>();
            }
            catch (Exception)
            {
                return new List<CancelMembershipDto>();
            }
        }
    }
}
