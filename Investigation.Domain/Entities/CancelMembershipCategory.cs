using Investigation.Shared.Domain.EntityFramework;

namespace Investigation.Domain.Entities
{
    public class CancelMembershipCategory : BaseEntity
    {
        public string Name { get; set; }

        public virtual ICollection<CancelMembership> CancelMemberships { get; set; }
    }
}
