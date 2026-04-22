using Microsoft.AspNetCore.Identity;

namespace Investigation.DataAccess.Abstract.ServiceAbstract
{
    public interface IUserAbstractServices
    {
        Task<List<IdentityUserRole<string>>> GetAllUserRoles();
    }
}
