using Investigation.DataAccess.Abstract;
using Investigation.DataAccess.Concrete.EntityFramework.Context.MSSQL;
using Investigation.Domain.Entities;
using Investigation.Shared.DataAccess.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Investigation.DataAccess.Concrete.EntityFramework.Repositories
{
    public class CancelMembershipRepository : EntityRepositoryBase<CancelMembership, ApplicationDbContext>, ICancelMembershipRepository
    {
        readonly ApplicationDbContext _context;
        public CancelMembershipRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public CancelMembership ReadNonUniqueHit(int id)
        {
            try
            {
                var hitRead = _context.Set<CancelMembership>().Where(i => i.Id == id).FirstOrDefault();
                if (hitRead != null && hitRead.Hit >= 0)
                {
                    hitRead.Hit++;
                    _context.SaveChanges();
                    return hitRead;
                }
                hitRead.Hit = 0;
                _context.SaveChanges();
                return hitRead;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting Hit the entity.", ex);
            }
        }

        public async Task<bool> SetAccountCancelAsync(int id)
        {
            try
            {
                var isAccountCancel = await _context.Set<CancelMembership>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (isAccountCancel != null)
                {
                    isAccountCancel.IsCancelled = true;
                    isAccountCancel.CancelDate = DateTime.UtcNow;
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting Account Cancelled the entity.", ex);
            }
        }

        public async Task<bool> SetAccountNotCancelAsync(int id)
        {
            try
            {
                var isAccountCancel = await _context.Set<CancelMembership>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (isAccountCancel != null)
                {
                    isAccountCancel.IsCancelled = false;
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting Account Not Cancelled the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            try
            {
                var active = await _context.Set<CancelMembership>().Where(i => i.Id == id).FirstOrDefaultAsync();
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
                var active = await _context.Set<CancelMembership>().Where(i => i.Id == id).FirstOrDefaultAsync();
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
                var deleted = await _context.Set<CancelMembership>().Where(i => i.Id == id).FirstOrDefaultAsync();
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

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            try
            {
                var deleted = await _context.Set<CancelMembership>().Where(i => i.Id == id).FirstOrDefaultAsync();
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

        public async Task<bool> SetRequestCancelAsync(int id)
        {
            try
            {
                var requestCancel = await _context.Set<CancelMembership>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (requestCancel != null)
                {
                    requestCancel.IsRequestCancelled = true;
                    requestCancel.RequestCancelledDate = DateTime.UtcNow;
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting Request Cancelled the entity.", ex);
            }
        }

        public async Task<bool> SetRequestNotCancelAsync(int id)
        {
            try
            {
                var requestCancel = await _context.Set<CancelMembership>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (requestCancel != null)
                {
                    requestCancel.IsRequestCancelled = false;
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting Request Not Cancelled the entity.", ex);
            }
        }
    }
}
