using Investigation.Shared.Domain.EntityFramework;

namespace Investigation.Domain.Entities
{
    public class UserSocialMedia : BaseEntity
    {
        public string Name { get; set; }
        public string Url { get; set; }

        public int? CompanyId { get; set; }
        public int? InvestorId { get; set; }

        public virtual Company Company { get; set; }
        public virtual Investor Investor { get; set; }
    }
}
