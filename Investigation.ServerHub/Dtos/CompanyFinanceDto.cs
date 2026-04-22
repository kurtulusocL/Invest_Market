
namespace Investigation.ServerHub.Dtos
{
    public class CompanyFinanceDto
    {
        public int Id { get; set; }
        public decimal? MarketValue { get; set; }
        public decimal? ARRIncome { get; set; } // yıllık tekrarlayan gelir
        public decimal TotalIncome { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public DateTime? SuspendedDate { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;

        public int? CompanyDtoId { get; set; }
        public int? VisibilitySettingDtoId { get; set; }

        public virtual CompanyDto CompanyDto { get; set; }
        public virtual VisibilitySettingDto VisibilitySettingDto { get; set; }


        public virtual ICollection<HitDto> HitsDto { get; set; }
        public int HitCount { get; set; }
    }
}
