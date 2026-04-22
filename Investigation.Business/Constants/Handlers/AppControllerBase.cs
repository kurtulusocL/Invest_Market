using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Investigation.Business.Constants.Handlers
{
    public abstract class AppControllerBase : Controller
    {
        public static async Task<IActionResult> CheckCompanyOwnershipAsync(HttpContext httpContext, object entityId, Func<object, Task<object>> getEntityById,
            Func<object, object> getCompanyIdFromEntity)
        {
            if (entityId == null)
                return new BadRequestObjectResult("Id is mandatory.");

            var entity = await getEntityById(entityId);
            if (entity == null)
                return new NotFoundResult();

            var sessionStr = httpContext.Session.GetString("companyId");
            if (string.IsNullOrEmpty(sessionStr))
                return new UnauthorizedObjectResult("Company auth information is missing or wrong.");

            object currentId = sessionStr;
            if (int.TryParse(sessionStr, out int parsed))
                currentId = parsed;

            var entityIdValue = getCompanyIdFromEntity(entity);
            if (entityIdValue == null || currentId == null || entityIdValue.ToString() != currentId.ToString())
            {
                return new ObjectResult("You are not owner of this request.") { StatusCode = 403 };
            }
            return null;
        }

        public static async Task<IActionResult> CheckInvestorOwnershipAsync(HttpContext httpContext, object entityId, Func<object, Task<object>> getEntityFunc,
            Func<object, object> getInvestorIdFunc)
        {
            if (entityId == null)
            {
                return new BadRequestObjectResult("Investor Id is mandatory.");
            }

            var entity = await getEntityFunc(entityId);
            if (entity == null)
            {
                return new NotFoundResult();
            }

            var currentInvestorIdStr = httpContext.Session.GetString("investorId");
            if (string.IsNullOrEmpty(currentInvestorIdStr))
            {
                return new UnauthorizedObjectResult("Investor auth information is missing or wrong.");
            }

            object currentInvestorId = currentInvestorIdStr;
            if (int.TryParse(currentInvestorIdStr, out int parsedInt))
            {
                currentInvestorId = parsedInt;
            }

            var entityInvestorId = getInvestorIdFunc(entity);            
            if (!object.Equals(entityInvestorId, currentInvestorId))
            {
                return new ObjectResult("You are not authorize for access for this investor.") { StatusCode = 403 };
            }
            return null;
        }

        public static async Task<IActionResult> CheckUserOwnershipAsync(HttpContext httpContext, object entityId, Func<object, Task<object>> getEntityById,
            Func<object, object> getUserIdFromEntity)
        {
            if (entityId == null)
            {
                return new BadRequestObjectResult("Id is mandatory.");
            }

            var entity = await getEntityById(entityId);
            if (entity == null)
            {
                return new NotFoundResult();
            }

            var sessionUserStr = httpContext.Session.GetString("userId");
            if (string.IsNullOrEmpty(sessionUserStr))
            {
                return new UnauthorizedObjectResult("User auth information is missing or wrong.");
            }

            object currentUserId = sessionUserStr;
            if (int.TryParse(sessionUserStr, out int parsedInt))
            {
                currentUserId = parsedInt;
            }

            var entityUserId = getUserIdFromEntity(entity);
            if (entityUserId == null || currentUserId == null || entityUserId.ToString() != currentUserId.ToString())
            {
                return new ObjectResult("You are not authorize for this request.") { StatusCode = 403 };
            }
            return null;
        }
    }
}
