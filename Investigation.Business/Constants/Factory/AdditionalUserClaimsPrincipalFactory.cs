using System.Security.Claims;
using Investigation.Domain.Entities.UserEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Investigation.Business.Constants.Factory
{
    public class AdditionalUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<AppUser>
    {
        readonly UserManager<AppUser> _userManager;
        public AdditionalUserClaimsPrincipalFactory(UserManager<AppUser> userManager, IOptions<IdentityOptions> optionsAccessor) : base(userManager, optionsAccessor) 
        {
            _userManager = userManager;
        }

        public override async Task<ClaimsPrincipal> CreateAsync(AppUser user)
        {
            var principal = await base.CreateAsync(user);
            var identity = (ClaimsIdentity)principal.Identity!;

            identity.AddClaim(new Claim("UserType", user.IsCompany ? "Company" : "Investor"));

            if (user.IsCompany)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, "CompanyUsers"));
            }
            else
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, "InvestorUsers"));
            }

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                if (role is "Admin" or "WorkerAdmin" or "AssistantAdmin" or "HelperAdmin")
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, role));
                }
            }
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            identity.AddClaim(new Claim("UserId", user.Id.ToString()));
            
            return principal;
        }
    }
}
