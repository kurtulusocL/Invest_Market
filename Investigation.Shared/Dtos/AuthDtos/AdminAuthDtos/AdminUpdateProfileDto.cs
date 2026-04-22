
namespace Investigation.Shared.Dtos.AuthDtos.AdminAuthDtos
{
    public class AdminUpdateProfileDto
    {
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
        public string Title { get; set; }
        public bool IsLoginConfirmCodeActive { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
        public DateTime UpdatedDate { get; set; }
    }
}
