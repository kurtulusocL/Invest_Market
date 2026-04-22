using Investigation.Shared.Domain.EntityFramework;

namespace Investigation.Domain.Entities
{
    public class CompanyContact : BaseEntity
    {
        public string Website { get; set; }
        public string? PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Location { get; set; }

        public int? CompanyId { get; set; }
        public virtual Company Company { get; set; }
    }
}
