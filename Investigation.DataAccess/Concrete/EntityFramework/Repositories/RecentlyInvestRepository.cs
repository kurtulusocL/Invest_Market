using Investigation.DataAccess.Abstract;
using Investigation.DataAccess.Concrete.EntityFramework.Context.MSSQL;
using Investigation.Domain.Entities;
using Investigation.Shared.DataAccess.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Investigation.DataAccess.Concrete.EntityFramework.Repositories
{
    public class RecentlyInvestRepository : EntityRepositoryBase<RecentlyInvest, ApplicationDbContext>, IRecentlyInvestRepository
    {
        readonly ApplicationDbContext _context;
        public RecentlyInvestRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            try
            {
                var active = await _context.Set<RecentlyInvest>().Where(i => i.Id == id).FirstOrDefaultAsync();
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

        public async Task<bool> SetDeActiveAsync(int id)
        {
            try
            {
                var active = await _context.Set<RecentlyInvest>().Where(i => i.Id == id).FirstOrDefaultAsync();
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

        public async Task<bool> SetDeletedAsync(int id)
        {
            try
            {
                var deleted = await _context.Set<RecentlyInvest>().Where(i => i.Id == id).FirstOrDefaultAsync();
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

        public async Task<bool> SetHasExitInvestAsync(int id)
        {
            try
            {
                var isExit = await _context.Set<RecentlyInvest>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (isExit != null)
                {
                    isExit.IsExit = true;
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting Has Exit the entity.", ex);
            }
        }

        public async Task<bool> SetHasNotExitInvestAsync(int id)
        {
            try
            {
                var isExit = await _context.Set<RecentlyInvest>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (isExit != null)
                {
                    isExit.IsExit = false;
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting Has Not Exit the entity.", ex);
            }
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            try
            {
                var deleted = await _context.Set<RecentlyInvest>().Where(i => i.Id == id).FirstOrDefaultAsync();
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
