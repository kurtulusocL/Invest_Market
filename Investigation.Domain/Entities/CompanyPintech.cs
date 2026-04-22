using Investigation.Shared.Domain.EntityFramework;

namespace Investigation.Domain.Entities
{
    public class CompanyPintech : BaseEntity
    {
        public string WorkPlan { get; set; }
        public string ServiceProduct { get; set; }
        public string Description { get; set; }
        public string MarketingStrategy { get; set; }
        public string GrowingPotantial { get; set; }

        public int? CompanyId { get; set; }
        public int? VisibilitySettingId { get; set; }

        public virtual Company Company { get; set; }
        public virtual VisibilitySetting VisibilitySetting{ get; set; }

        public virtual ICollection<Hit> Hits { get; set; }
    }
}
