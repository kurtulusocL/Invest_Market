
namespace Investigation.ServerHub.Dtos
{
    public class CancelMembershipCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public DateTime? SuspendedDate { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;

        public virtual ICollection<CancelMembershipDto> CancelMembershipsDto { get; set; }
        public int CancelMembershipCount { get; set; }

    }
}
