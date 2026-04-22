using Investigation.Business.Services.Abstract;
using Investigation.ServerHub.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Investigation.ServerHub.Hubs
{
    [Authorize(AuthenticationSchemes = "ApiKey")]
    public class KVKKHub:Hub
    {
        readonly IKVKKService _kvkkService;
        public KVKKHub(IKVKKService kvkkervice)
        {
            _kvkkService = kvkkervice;
        }
        public async Task<IEnumerable<KVKKDto>> GetAllAsync()
        {
            try
            {
                var data = await _kvkkService.GetAllForSignalRAsync();
                if (data != null)
                {
                    return data.Select(i => new KVKKDto
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
                return new List<KVKKDto>();
            }
            catch (Exception)
            {
                return new List<KVKKDto>();
            }
        }
    }
}
