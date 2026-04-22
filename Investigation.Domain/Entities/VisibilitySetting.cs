using Investigation.Shared.Domain.EntityFramework;

namespace Investigation.Domain.Entities
{
    public class VisibilitySetting : BaseEntity
    {
        public bool IsVisibleForCompanies { get; set; } = true;
        public bool IsVisibleForInvestors { get; set; } = true;
        public bool IsVisibleForAll { get; set; } = true;
        public bool IsVisibleForNone { get; set; } = false;

        public int? CompanyFinanceId { get; set; }
        public int? CompanyPintechId { get; set; }
        public int? CompanyStageId { get; set; }

        public virtual CompanyFinance CompanyFinance { get; set; }
        public virtual CompanyPintech CompanyPintech { get; set; }
        public virtual CompanyStage CompanyStage { get; set; }
    }
}
