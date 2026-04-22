using Investigation.Shared.Domain.EntityFramework;

namespace Investigation.Domain.Entities
{
    public class RecentlyInvest : BaseEntity
    {
        public string Title { get; set; }
        public string? Desc { get; set; }
        public DateTime InvestDate { get; set; }
        public bool IsExit { get; set; }
        public DateTime? ExitDate { get; set; }
        public string? WebUrl { get; set; }
        public string? ImageUrl { get; set; }

        public int? InvestorId { get; set; }
        public int SectorId { get; set; }
        public int? SubSectorId { get; set; }

        public virtual Investor Investor { get; set; }
        public virtual Sector Sector { get; set; }
        public virtual SubSector SubSector { get; set; }
    }
}
