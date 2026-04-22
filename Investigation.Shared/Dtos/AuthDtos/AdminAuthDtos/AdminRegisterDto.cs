
namespace Investigation.Shared.Dtos.AuthDtos.AdminAuthDtos
{
    public class AdminRegisterDto
    {
        public string NameSurname { get; set; }
        public string Country { get; set; }
        public string Title { get; set; }
        public DateTime Birthdate { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }              
    }
}
