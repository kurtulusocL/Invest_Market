
namespace Investigation.ServerHub.Dtos
{
    public class SubSectorDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public DateTime? SuspendedDate { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;

        public int? SectorDtoId { get; set; }
        public virtual SectorDto SectorDto { get; set; }

        public virtual ICollection<CompanyDto> CompaniesDto { get; set; }       
        public virtual ICollection<RecentlyInvestDto> RecentlyInvestsDto { get; set; }

        public int CompanyCount { get; set; }
        public int RecentlyInvestCount { get; set; }

    }
}
