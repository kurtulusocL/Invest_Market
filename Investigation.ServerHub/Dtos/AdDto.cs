
namespace Investigation.ServerHub.Dtos
{
    public class AdDto
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string? Text { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public string ImageUrl { get; set; }
        public string? RedirectUrl { get; set; }
        public int? NonUniqueHit { get; set; } = 0;
        public bool HasTarget { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public DateTime? SuspendedDate { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;

        public virtual ICollection<AdTargetDto> AdTargetDtos { get; set; } = new List<AdTargetDto>();
        public int AdTargetCount { get; set; }        
        public virtual ICollection<HitDto> HitDtos { get; set; } = new List<HitDto>();
        public int HitCount { get; set; }
    }
}
