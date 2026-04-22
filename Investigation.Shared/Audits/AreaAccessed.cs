using Microsoft.AspNetCore.Mvc.Filters;

namespace Investigation.Shared.Audits
{
    public static class AreaAccessed
    {
        public static string GetDetailedAreaAccessed(ActionExecutingContext filterContext)
        {
            try
            {
                var routeData = filterContext.RouteData;
                var request = filterContext.HttpContext.Request;

                if (routeData?.Values == null)
                {
                    return $"Unknown - {request.Method} {request.Path}";
                }

                var controllerName = routeData.Values["controller"]?.ToString() ?? "Unknown";
                var actionName = routeData.Values["action"]?.ToString() ?? "Unknown";
                var areaName = routeData.DataTokens?["area"]?.ToString();

                var httpMethod = request.Method;
                var path = request.Path.Value ?? "";

                if (!string.IsNullOrEmpty(areaName))
                {
                    return $"{areaName}/{controllerName}/{actionName} - {httpMethod} {path}";
                }

                return $"{controllerName}/{actionName} - {httpMethod} {path}";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DetailedAreaAccessed hatası: {ex.Message}");
                return "Error/Error";
            }
        }
    }
}
