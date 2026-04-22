using System.Reflection;
using Investigation.Business.Attributes;
using Investigation.Business.Constants.Handlers.HandlerClass;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;

namespace Investigation.Business.Constants.Handlers
{
    public class ProfileOwnerRequirementHandler : AuthorizationHandler<ProfileOwnerRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IServiceProvider _serviceProvider;
        public ProfileOwnerRequirementHandler(IHttpContextAccessor httpContextAccessor, IServiceProvider serviceProvider)
        {
            _httpContextAccessor = httpContextAccessor;
            _serviceProvider = serviceProvider;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, ProfileOwnerRequirement requirement)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null || httpContext.Session == null)
            {
                context.Fail();
                return;
            }

            var endpoint = httpContext.GetEndpoint();
            var actionDescriptor = endpoint?.Metadata.GetMetadata<ControllerActionDescriptor>();
            if (actionDescriptor != null)
            {
                var skipCheck = actionDescriptor.MethodInfo.GetCustomAttribute<SkipOwnershipCheckAttribute>();
                if (skipCheck != null)
                {
                    context.Succeed(requirement);
                    return;
                }
            }

            string? currentUserId = null;
            string? currentCompanyId = null;
            string? currentInvestorId = null;

            try
            {
                await httpContext.Session.LoadAsync();
                currentUserId = httpContext.Session.GetString("userId");
                currentCompanyId = httpContext.Session.GetString("companyId");
                currentInvestorId = httpContext.Session.GetString("investorId");
            }
            catch
            {
                context.Fail();
                return;
            }

            if (string.IsNullOrWhiteSpace(currentUserId))
            {
                context.Fail();
                return;
            }

            //var routeId = httpContext.Request.RouteValues["id"]?.ToString();
            //var controller = httpContext.Request.RouteValues["controller"]?.ToString();

            //if (string.IsNullOrWhiteSpace(routeId))
            //{
            //    context.Succeed(requirement);
            //    return;
            //}

            //if (string.IsNullOrWhiteSpace(controller))
            //{
            //    context.Fail();
            //    return;
            //}
            //-----------------------------------------------------------------------------------------------------------------------------
            var routeId = httpContext.Request.RouteValues["id"]?.ToString();
            if (string.IsNullOrWhiteSpace(routeId))
            {
                routeId = httpContext.Request.Query["id"].FirstOrDefault();
            }

            var controller = httpContext.Request.RouteValues["controller"]?.ToString();

            if (string.IsNullOrWhiteSpace(routeId))
            {
                context.Succeed(requirement);
                return;
            }
           
            if (string.IsNullOrWhiteSpace(controller))
            {
                context.Fail();
                return;
            }
            //-------------------------------------------------------------------------------------------------------------------------------
            var controllerLower = controller.ToLowerInvariant();

            bool isAllowedController = controllerLower == "homecompany" || controllerLower == "companyhome" || controllerLower == "companyoperation" ||
                                      controllerLower == "homeinvestor" || controllerLower == "investorhome" || controllerLower == "investoroperation";
            if (!isAllowedController)
            {
                context.Fail();
                return;
            }

            bool isDirectMatch =
                (!string.IsNullOrWhiteSpace(currentUserId) && currentUserId.Equals(routeId, StringComparison.OrdinalIgnoreCase)) ||
                (!string.IsNullOrWhiteSpace(currentCompanyId) && currentCompanyId.Equals(routeId, StringComparison.OrdinalIgnoreCase)) ||
                (!string.IsNullOrWhiteSpace(currentInvestorId) && currentInvestorId.Equals(routeId, StringComparison.OrdinalIgnoreCase));

            if (isDirectMatch)
            {
                context.Succeed(requirement);
                return;
            }

            if (!int.TryParse(routeId, out int entityId))
            {
                using var scope = _serviceProvider.CreateScope();
                var action = httpContext.Request.RouteValues["action"]?.ToString()?.ToLowerInvariant();

                var (entity, serviceType) = await FindEntityFromServicesWithStringId(httpContext, scope, routeId, action);
                if (entity == null)
                {
                    context.Fail();
                    return;
                }

                var ownerProp = entity.GetType().GetProperty("UserId") ??
                               entity.GetType().GetProperty("Id");
                if (ownerProp == null)
                {
                    context.Fail();
                    return;
                }

                var ownerValue = ownerProp.GetValue(entity)?.ToString();
                if (string.Equals(ownerValue, currentUserId, StringComparison.OrdinalIgnoreCase))
                {
                    context.Succeed(requirement);
                    return;
                }
                context.Fail();
                return;
            }

            string? currentOwnerId = null;
            string? ownerProperty = null;

            if (controllerLower == "homeinvestor" || controllerLower == "investorhome" || controllerLower == "investoroperation")
            {
                if (string.IsNullOrWhiteSpace(currentInvestorId))
                {
                    context.Fail();
                    return;
                }
                currentOwnerId = currentInvestorId;
                ownerProperty = "InvestorId";
            }
            else if (controllerLower == "homecompany" || controllerLower == "companyhome" || controllerLower == "companyoperation")
            {
                if (string.IsNullOrWhiteSpace(currentCompanyId))
                {
                    context.Fail();
                    return;
                }
                currentOwnerId = currentCompanyId;
                ownerProperty = "CompanyId";
            }

            if (string.IsNullOrWhiteSpace(currentOwnerId) || string.IsNullOrWhiteSpace(ownerProperty))
            {
                context.Fail();
                return;
            }

            using var intScope = _serviceProvider.CreateScope();
            var intAction = httpContext.Request.RouteValues["action"]?.ToString()?.ToLowerInvariant();

            var (intEntity, intServiceType) = await FindEntityFromServices(httpContext, intScope, entityId, intAction);
            if (intEntity == null)
            {
                context.Fail();
                return;
            }

            var intOwnerProp = intEntity.GetType().GetProperty(ownerProperty);
            if (intOwnerProp == null)
            {
                context.Fail();
                return;
            }

            var intOwnerValue = intOwnerProp.GetValue(intEntity)?.ToString();
            if (string.Equals(intOwnerValue, currentOwnerId, StringComparison.OrdinalIgnoreCase))
            {
                context.Succeed(requirement);
                return;
            }
            context.Fail();
        }

        private async Task<(object entity, Type serviceType)> FindEntityFromServices(HttpContext httpContext, IServiceScope scope, int entityId, string actionLower)
        {
            var endpoint = httpContext.GetEndpoint();
            var actionDescriptor = endpoint?.Metadata.GetMetadata<ControllerActionDescriptor>();

            if (actionDescriptor == null)
                return (null, null);

            var serviceTypeAttribute = actionDescriptor.MethodInfo.GetCustomAttribute<ServiceTypeAttribute>();

            if (serviceTypeAttribute == null)
                return (null, null);

            var serviceType = serviceTypeAttribute.ServiceType;

            var service = scope.ServiceProvider.GetService(serviceType);
            if (service == null)
                return (null, null);

            var getByIdMethod = service.GetType().GetMethod("GetByIdAsync");
            if (getByIdMethod == null)
                return (null, null);

            try
            {
                var parameters = getByIdMethod.GetParameters();
                object[] invokeParams;

                if (parameters.Length > 0 && parameters[0].ParameterType == typeof(int?))
                {
                    invokeParams = new object[] { (int?)entityId };
                }
                else
                {
                    invokeParams = new object[] { entityId };
                }

                var task = (Task)getByIdMethod.Invoke(service, invokeParams);
                await task.ConfigureAwait(false);

                var resultProperty = task.GetType().GetProperty("Result");
                var entity = resultProperty?.GetValue(task);

                return (entity, serviceType);
            }
            catch
            {
                return (null, null);
            }
        }
        private async Task<(object entity, Type serviceType)> FindEntityFromServicesWithStringId(HttpContext httpContext, IServiceScope scope, string entityId, string actionLower)
        {
            var endpoint = httpContext.GetEndpoint();
            var actionDescriptor = endpoint?.Metadata.GetMetadata<ControllerActionDescriptor>();

            if (actionDescriptor == null)
                return (null, null);

            var serviceTypeAttribute = actionDescriptor.MethodInfo.GetCustomAttribute<ServiceTypeAttribute>();

            if (serviceTypeAttribute == null)
                return (null, null);

            var serviceType = serviceTypeAttribute.ServiceType;

            var service = scope.ServiceProvider.GetService(serviceType);
            if (service == null)
                return (null, null);

            var getByIdMethod = service.GetType().GetMethod("GetByIdAsync") ??
                               service.GetType().GetMethod("FindByIdAsync");

            if (getByIdMethod == null)
                return (null, null);

            try
            {
                var parameters = getByIdMethod.GetParameters();
                if (parameters.Length == 0)
                    return (null, null);

                object[] invokeParams = new object[] { entityId };

                var task = (Task)getByIdMethod.Invoke(service, invokeParams);
                await task.ConfigureAwait(false);

                var resultProperty = task.GetType().GetProperty("Result");
                var entity = resultProperty?.GetValue(task);

                return (entity, serviceType);
            }
            catch
            {
                return (null, null);
            }
        }
    }
}