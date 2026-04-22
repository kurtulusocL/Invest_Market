
namespace Investigation.Shared.Dtos.AuthDtos.AdminAuthDtos
{
    public class AdminConfirmCodeDto
    {
        public string Email { get; set; }
        public int LoginConfirmCode { get; set; }
        public string ReturnUrl { get; set; }
    }
}
