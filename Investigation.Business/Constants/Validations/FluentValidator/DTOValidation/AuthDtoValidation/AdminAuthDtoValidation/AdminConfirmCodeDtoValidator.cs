using FluentValidation;
using Investigation.Shared.Dtos.AuthDtos.AdminAuthDtos;

namespace Investigation.Business.Constants.Validations.FluentValidator.DTOValidation.AuthDtoValidation.AdminAuthDtoValidation
{
    public class AdminConfirmCodeDtoValidator:AbstractValidator<AdminConfirmCodeDto>
    {
        public AdminConfirmCodeDtoValidator()
        {
            RuleFor(i => i.Email).EmailAddress().NotEmpty().WithMessage("email address can not be empty and must be valid.");
        }
    }
}
