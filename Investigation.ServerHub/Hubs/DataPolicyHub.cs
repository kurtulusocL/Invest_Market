using Investigation.Business.Services.Abstract;
using Investigation.ServerHub.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Investigation.ServerHub.Hubs
{
    [Authorize(AuthenticationSchemes = "ApiKey")]
    public class DataPolicyHub : Hub
    {
        readonly IDataPolicyService _dataPolicyService;
        public DataPolicyHub(IDataPolicyService dataPolicyService)
        {
            _dataPolicyService = dataPolicyService;
        }
        public async Task<IEnumerable<DataPolicyDto>> GetAllAsync()
        {
            try
            {
                var data = await _dataPolicyService.GetAllForSignalRAsync();
                if (data != null)
                {
                    return data.Select(i => new DataPolicyDto
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
                    }).ToList();
                }
                return new List<DataPolicyDto>();
            }
            catch (Exception)
            {
                return new List<DataPolicyDto>();
            }
        }
    }
}
