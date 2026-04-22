
namespace Investigation.ServerHub.Dtos
{
    public class RecentlyInvestDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Desc { get; set; }
        public DateTime InvestDate { get; set; }
        public bool IsExit { get; set; }
        public DateTime? ExitDate { get; set; }
        public string? WebUrl { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public DateTime? SuspendedDate { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;

        public int? InvestorDtoId { get; set; }
        public int SectorDtoId { get; set; }
        public int? SubSectorDtoId { get; set; }

        public virtual InvestorDto InvestorDto { get; set; }
        public virtual SectorDto SectorDto { get; set; }
        public virtual SubSectorDto SubSectorDto { get; set; }
    }
}
