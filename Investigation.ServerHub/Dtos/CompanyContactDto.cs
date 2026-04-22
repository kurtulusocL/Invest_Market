
namespace Investigation.ServerHub.Dtos
{
    public class CompanyContactDto
    {
        public int Id { get; set; }
        public string Website { get; set; }
        public string? PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Location { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public DateTime? SuspendedDate { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;

        public int? CompanyDtoId { get; set; }
        public virtual CompanyDto CompanyDto { get; set; }
    }
}
