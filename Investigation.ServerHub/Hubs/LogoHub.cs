using Investigation.Business.Services.Abstract;
using Investigation.ServerHub.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Investigation.ServerHub.Hubs
{
    [Authorize(AuthenticationSchemes = "ApiKey")]
    public class LogoHub:Hub
    {
        readonly ILogoService _logoService;
        public LogoHub(ILogoService logoService)
        {
            _logoService = logoService;
        }
        public async Task<IEnumerable<LogoDto>> GetAllAsync()
        {
            try
            {
                var data = await _logoService.GetAllForSignalRAsync();
                if (data != null)
                {
                    return data.Select(i => new LogoDto
                    {
                        Id = i.Id,
                        UseFor = i.UseFor,
                        ImageUrl = i.ImageUrl,
                        CreatedDate = i.CreatedDate,
                        UpdatedDate = i.UpdatedDate,
                        SuspendedDate = i.SuspendedDate,
                        DeletedDate = i.DeletedDate,
                        IsActive = i.IsActive,
                        IsDeleted = i.IsDeleted
                    }).ToList();
                }
                return new List<LogoDto>();
            }
            catch (Exception)
            {
                return new List<LogoDto>();
            }
        }
    }
}
