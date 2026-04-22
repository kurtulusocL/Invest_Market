using Investigation.Domain.Entities.UserEntities;
using Investigation.Shared.Domain.EntityFramework;

namespace Investigation.Domain.Entities
{
    public class SavedContent : BaseEntity
    {
        public bool IsSaved { get; set; }
        public DateTime SaveDate { get; set; }
        public DateTime? DisSaveDate { get; set; }

        public string AppUserId { get; set; }
        public int? BlogId { get; set; }
        public int? SectorNewsId { get; set; }
        public int? CompanyId { get; set; }
        public int? InvestorId { get; set; }
        public int? PostId { get; set; }
        public int? SurveyId { get; set; }

        public virtual AppUser AppUser { get; set; }
        public virtual Blog Blog { get; set; }
        public virtual SectorNews SectorNews { get; set; }
        public virtual Company Company { get; set; }
        public virtual Investor Investor { get; set; }
        public virtual Post Post { get; set; }
        public virtual Survey Survey { get; set; }
    }
}
