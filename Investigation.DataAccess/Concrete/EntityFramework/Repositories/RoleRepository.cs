using Investigation.DataAccess.Abstract;
using Investigation.DataAccess.Concrete.EntityFramework.Context.MSSQL;
using Investigation.Domain.Entities.UserEntities;
using Investigation.Shared.DataAccess.EntityFramework;
using Investigation.Shared.Dtos.UserDto;
using Microsoft.EntityFrameworkCore;

namespace Investigation.DataAccess.Concrete.EntityFramework.Repositories
{
    public class RoleRepository : EntityRepositoryBase<AppRole, ApplicationDbContext>, IRoleRepository
    {
        readonly ApplicationDbContext _context;
        public RoleRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RoleUserCountDto>> GetAllUserCountsByRoleAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var roleUserCounts = await
                    (from userRole in _context.UserRoles
                     join role in _context.Roles on userRole.RoleId equals role.Id
                     group userRole by role.Name into g
                     select new RoleUserCountDto
                     {
                         RoleName = g.Key,
                         UserCount = g.Count()
                     }).OrderByDescending(x => x.UserCount).ToListAsync(cancellationToken);

                var allRoles = await _context.Roles.Select(r => r.Name).ToListAsync(cancellationToken);

                var result = allRoles.GroupJoin(roleUserCounts, roleName => roleName, count => count.RoleName,
                        (roleName, counts) => new RoleUserCountDto
                        {
                            RoleName = roleName,
                            UserCount = counts.DefaultIfEmpty(new RoleUserCountDto { UserCount = 0 }).Sum(x => x.UserCount)
                        }).OrderByDescending(x => x.UserCount).ToList();

                return result;
            }
            catch
            {
                return new List<RoleUserCountDto>();
            }
        }

        public async Task<bool> SetActiveAsync(string id)
        {
            try
            {
                var active = await _context.Set<AppRole>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (active != null)
                {
                    active.IsActive = true;
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting Active the entity.", ex);
            }
        }

        public async Task<bool> SetDeActiveAsync(string id)
        {
            try
            {
                var active = await _context.Set<AppRole>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (active != null)
                {
                    active.IsActive = false;
                    active.SuspendedDate = DateTime.Now.ToLocalTime();
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting DeActive the entity.", ex);
            }
        }

        public async Task<bool> SetDeletedAsync(string id)
        {
            try
            {
                var deleted = await _context.Set<AppRole>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (deleted != null)
                {
                    deleted.IsDeleted = true;
                    deleted.DeletedDate = DateTime.Now.ToLocalTime();
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting Deleted the entity.", ex);
            }
        }

        public async Task<bool> SetNotDeletedAsync(string id)
        {
            try
            {
                var deleted = await _context.Set<AppRole>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (deleted != null)
                {
                    deleted.IsDeleted = false;
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting Not Deleted the entity.", ex);
            }
        }
    }
}
