using Investigation.Shared.Domain.EntityFramework;

namespace Investigation.Domain.Entities
{
    public class Sector : BaseEntity
    {
        public string Name { get; set; }

        public virtual ICollection<Company> Companies { get; set; }
        public virtual ICollection<RecentlyInvest> RecentlyInvests { get; set; }
        public virtual ICollection<SubSector> SubSectors { get; set; }
    }
}
