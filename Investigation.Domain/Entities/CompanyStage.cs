using Investigation.Shared.Domain.EntityFramework;

namespace Investigation.Domain.Entities
{
    public class CompanyStage : BaseEntity
    {
        public string StageName { get; set; }
        public decimal StageValue { get; set; }

        public int? CompanyId { get; set; }
        public int? VisibilitySettingId { get; set; }

        public virtual Company Company { get; set; }
        public virtual VisibilitySetting VisibilitySetting { get; set; }

        public virtual ICollection<Hit> Hits { get; set; }
    }
}
