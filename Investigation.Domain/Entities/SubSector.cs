using Investigation.Shared.Domain.EntityFramework;

namespace Investigation.Domain.Entities
{
    public class SubSector : BaseEntity
    {
        public string Name { get; set; }

        public int? SectorId { get; set; }
        public virtual Sector Sector { get; set; }

        public virtual ICollection<Company> Companies { get; set; }
        public virtual ICollection<RecentlyInvest> RecentlyInvests { get; set; }
    }
}
