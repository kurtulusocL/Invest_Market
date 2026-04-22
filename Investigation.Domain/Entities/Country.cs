using Investigation.Shared.Domain.EntityFramework;

namespace Investigation.Domain.Entities
{
    public class Country : BaseEntity
    {
        public string Name { get; set; }

        public virtual ICollection<Company> Companies { get; set; }
        public virtual ICollection<Investor> Investors { get; set; }
    }
}
