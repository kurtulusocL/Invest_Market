
namespace Investigation.ServerHub.Dtos
{
    public class CompanyTeamDto
    {
        public int Id { get; set; }
        public string NameSurname { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
        public int TotalExperienceDuration { get; set; }
        public string PhotoUrl { get; set; }
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
