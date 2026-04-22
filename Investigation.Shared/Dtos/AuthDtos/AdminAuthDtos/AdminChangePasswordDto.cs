using System.ComponentModel.DataAnnotations;

namespace Investigation.Shared.Dtos.AuthDtos.AdminAuthDtos
{
    public class AdminChangePasswordDto
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }

        [Compare("NewPassword", ErrorMessage = "Passwords are not same.")]
        public string ConfirmNewPassword { get; set; }
        public string StatusMessage { get; set; }
    }
}
