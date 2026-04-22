using Investigation.Business.Services.Abstract;
using Investigation.ServerHub.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Investigation.ServerHub.Hubs
{
    [Authorize(AuthenticationSchemes = "ApiKey")]
    public class ExceptionLoggerHub : Hub
    {
        readonly IExceptionLoggerService _exceptionLoggerService;
        public ExceptionLoggerHub(IExceptionLoggerService exceptionLoggerService)
        {
            _exceptionLoggerService = exceptionLoggerService;
        }
        public async Task<IEnumerable<ExceptionLoggerDto>> GetAllAsync()
        {
            try
            {
                var data = await _exceptionLoggerService.GetAllForSignalRAsync();
                if (data != null)
                {
                    return data.Select(i => new ExceptionLoggerDto
                    {
                        Id = i.Id,
                        ExceptionMessage = i.ExceptionMessage,
                        ControllerName = i.ControllerName,
                        ExceptionStackTrace = i.ExceptionStackTrace,
                        CreatedDate = i.CreatedDate,
                        UpdatedDate = i.UpdatedDate,
                        SuspendedDate = i.SuspendedDate,
                        DeletedDate = i.DeletedDate,
                        IsActive = i.IsActive,
                        IsDeleted = i.IsDeleted
                    }).ToList();
                }
                return new List<ExceptionLoggerDto>();
            }
            catch (Exception)
            {
                return new List<ExceptionLoggerDto>();
            }
        }
    }
}
