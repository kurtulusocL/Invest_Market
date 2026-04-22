

namespace Investigation.ServerHub.Dtos
{
    public class VisibilitySettingDto
    {
        public int Id { get; set; }
        public bool IsVisibleForCompanies { get; set; } = true;
        public bool IsVisibleForInvestors { get; set; } = true;
        public bool IsVisibleForAll { get; set; } = true;
        public bool IsVisibleForNone { get; set; } = false;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public DateTime? SuspendedDate { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;

        public int? CompanyFinanceDtoId { get; set; }
        public int? CompanyPintechDtoId { get; set; }
        public int? CompanyStageDtoId { get; set; }

        public virtual CompanyFinanceDto CompanyFinanceDto { get; set; }
        public virtual CompanyPintechDto CompanyPintechDto { get; set; }
        public virtual CompanyStageDto CompanyStageDto { get; set; }

    }
}
