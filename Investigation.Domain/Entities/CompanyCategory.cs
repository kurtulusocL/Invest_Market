using Investigation.Shared.Domain.EntityFramework;

namespace Investigation.Domain.Entities
{
    public class CompanyCategory : BaseEntity
    {
        public string Name { get; set; }

        public virtual ICollection<Company> Companies { get; set; }
    }
}
