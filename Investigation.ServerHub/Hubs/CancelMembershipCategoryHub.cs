using Investigation.Business.Services.Abstract;
using Investigation.ServerHub.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Investigation.ServerHub.Hubs
{
    [Authorize(AuthenticationSchemes = "ApiKey")]
    public class CancelMembershipCategoryHub : Hub
    {
        readonly ICancelMembershipCategoryService _cancelMembershipCategoy;
        public CancelMembershipCategoryHub(ICancelMembershipCategoryService cancelMembershipCategoryService)
        {
            _cancelMembershipCategoy = cancelMembershipCategoryService;
        }
        public async Task<IEnumerable<CancelMembershipCategoryDto>> GetAllIncludingAsync()
        {
            try
            {
                var data = await _cancelMembershipCategoy.GetAllForSignalRAsync();
                if (data != null)
                {
                    return data.Select(i => new CancelMembershipCategoryDto
                    {
                        Id = i.Id,
                        Name = i.Name,
                        CancelMembershipCount = i.CancelMemberships?.Count ?? 0,
                        CreatedDate = i.CreatedDate,
                        UpdatedDate = i.UpdatedDate,
                        SuspendedDate = i.SuspendedDate,
                        DeletedDate = i.DeletedDate,
                        IsActive = i.IsActive,
                        IsDeleted = i.IsDeleted
                    }).ToList();
                }
                return new List<CancelMembershipCategoryDto>();
            }
            catch (Exception)
            {
                return new List<CancelMembershipCategoryDto>();
            }
        }
    }
}