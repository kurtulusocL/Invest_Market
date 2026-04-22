using Investigation.Shared.Domain.EntityFramework;

namespace Investigation.Domain.Entities
{
    public class Ad : BaseEntity
    {
        public string CompanyName { get; set; }
        public string? Text { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public string ImageUrl { get; set; }
        public string? RedirectUrl { get; set; }
        public int? NonUniqueHit { get; set; } = 0;
        public bool HasTarget { get; set; }

        public virtual ICollection<AdTarget> AdTargets { get; set; }
        public virtual ICollection<Hit> Hits { get; set; }
    }
}
