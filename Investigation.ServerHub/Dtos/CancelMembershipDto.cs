
namespace Investigation.ServerHub.Dtos
{
    public class CancelMembershipDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public bool IsRequestCancelled { get; set; } = false; //is user cancelled to his request
        public bool IsCancelled { get; set; } = false;
        public DateTime? CancelDate { get; set; }
        public DateTime? RequestCancelledDate { get; set; }
        public int Hit { get; set; } = 0;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public DateTime? SuspendedDate { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;

        public string AppUserDtoId { get; set; }
        public int CancelMembershipCategoryDtoId { get; set; }

        public virtual AppUserDto AppUserDto { get; set; }
        public virtual CancelMembershipCategoryDto CancelMembershipCategoryDto { get; set; }
    }
}
