
namespace Investigation.Shared.Dtos.AuthDtos.AdminAuthDtos
{
    public class AdminLoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public string ReturnUrl { get; set; }
    }
}
