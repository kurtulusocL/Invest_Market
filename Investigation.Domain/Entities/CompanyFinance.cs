using Investigation.Shared.Domain.EntityFramework;

namespace Investigation.Domain.Entities
{
    public class CompanyFinance : BaseEntity
    {
        public decimal? MarketValue { get; set; }
        public decimal? ARRIncome { get; set; } // yıllık tekrarlayan gelir
        public decimal TotalIncome { get; set; }        

        public int? CompanyId { get; set; }
        public int? VisibilitySettingId { get; set; }

        public virtual Company Company { get; set; }
        public virtual VisibilitySetting VisibilitySetting { get; set; }


        public virtual ICollection<Hit> Hits { get; set; }
    }
}
