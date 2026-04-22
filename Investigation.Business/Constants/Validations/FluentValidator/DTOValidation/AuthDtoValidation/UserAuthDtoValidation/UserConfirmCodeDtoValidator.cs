using FluentValidation;
using Investigation.Shared.Dtos.AuthDtos.UserAuthDtos;

namespace Investigation.Business.Constants.Validations.FluentValidator.DTOValidation.AuthDtoValidation.UserAuthDtoValidation
{
    public class UserConfirmCodeDtoValidator:AbstractValidator<UserConfirmCodeDto>
    {
        public UserConfirmCodeDtoValidator()
        {
            RuleFor(i => i.Email).EmailAddress().NotEmpty().WithMessage("email address can not be empty and must be valid.");
        }
    }
}
