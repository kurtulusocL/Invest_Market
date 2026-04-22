using Investigation.Shared.Domain.EntityFramework;

namespace Investigation.Domain.Entities
{
    public class InvestorCategory:BaseEntity
    {
        public string Name { get; set; }

        public virtual ICollection<Investor> Investors { get; set; }
    }
}
