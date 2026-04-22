using FluentValidation;
using Investigation.Shared.Dtos.AuthDtos.AdminAuthDtos;

namespace Investigation.Business.Constants.Validations.FluentValidator.DTOValidation.AuthDtoValidation.AdminAuthDtoValidation
{
    public class AdminLoginDtoValidator:AbstractValidator<AdminLoginDto>
    {
        public AdminLoginDtoValidator()
        {
            RuleFor(i => i.Email).EmailAddress().NotEmpty().WithMessage("email address can not be empty and must be valid");
            RuleFor(i => i.Password).MinimumLength(8).NotEmpty().WithMessage("password can not be empty and must be min 8 characters");
        }
    }
}
