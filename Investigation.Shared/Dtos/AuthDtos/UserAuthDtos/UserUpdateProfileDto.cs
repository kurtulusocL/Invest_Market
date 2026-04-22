
namespace Investigation.Shared.Dtos.AuthDtos.UserAuthDtos
{
    public class UserUpdateProfileDto
    {
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
        public bool IsLoginConfirmCodeActive { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime UpdatedDate { get; set; }
    }
}
