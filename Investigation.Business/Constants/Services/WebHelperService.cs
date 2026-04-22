using Microsoft.AspNetCore.Http;

namespace Investigation.Business.Constants.Services
{
    public class WebHelperService:IWebHelperService
    {
        private readonly IHttpContextAccessor _accessor;

        public WebHelperService(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public string GetClientIp()
        {
            var context = _accessor.HttpContext;
            if (context == null) return "unknown";

            var forwardedFor = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (!string.IsNullOrEmpty(forwardedFor))
            {
                return forwardedFor.Split(',').FirstOrDefault()?.Trim();
            }

            return context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
        }

        public string GetUserAgent()
        {
            return _accessor.HttpContext?.Request.Headers.UserAgent.ToString() ?? "unknown";
        }
    }
}
