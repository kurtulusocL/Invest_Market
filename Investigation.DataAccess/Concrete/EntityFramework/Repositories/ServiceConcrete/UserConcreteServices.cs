using Investigation.DataAccess.Abstract.ServiceAbstract;
using Investigation.DataAccess.Concrete.EntityFramework.Context.MSSQL;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Investigation.DataAccess.Concrete.EntityFramework.Repositories.ServiceConcrete
{
    public class UserConcreteServices : IUserAbstractServices
    {
        readonly ApplicationDbContext _context;
        public UserConcreteServices(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<IdentityUserRole<string>>> GetAllUserRoles()
        {
            return await _context.UserRoles.ToListAsync();
        }
    }
}
