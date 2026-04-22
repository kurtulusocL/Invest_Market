using Investigation.Business.Services.Abstract;
using Investigation.ServerHub.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Investigation.ServerHub.Hubs
{
    [Authorize(AuthenticationSchemes = "ApiKey")]
    public class UserAgreementHub : Hub
    {
        readonly IUserAgreementService _userAgreementService;
        public UserAgreementHub(IUserAgreementService userAgreementService)
        {
            _userAgreementService = userAgreementService;
        }
        public async Task<IEnumerable<UserAgreementDto>> GetAllAsync()
        {
            try
            {
                var data = await _userAgreementService.GetAllForSignalRAsync();
                if (data != null)
                {
                    return data.Select(i => new UserAgreementDto
                    {
                        Id = i.Id,
                        Title = i.Title,
                        Subtitle = i.Subtitle,
                        Desc = i.Desc,
                        CreatedDate = i.CreatedDate,
                        UpdatedDate = i.UpdatedDate,
                        SuspendedDate = i.SuspendedDate,
                        DeletedDate = i.DeletedDate,
                        IsActive = i.IsActive,
                        IsDeleted = i.IsDeleted
                    });
                }
                return new List<UserAgreementDto>();
            }
            catch (Exception)
            {
                return new List<UserAgreementDto>();
            }
        }
    }
}
