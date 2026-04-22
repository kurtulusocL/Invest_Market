using FluentValidation;
using Investigation.Shared.Dtos.AuthDtos.UserAuthDtos;

namespace Investigation.Business.Constants.Validations.FluentValidator.DTOValidation.AuthDtoValidation.UserAuthDtoValidation
{
    public class UserProfileUpdateDtoValidator:AbstractValidator<UserUpdateProfileDto>
    {
        public UserProfileUpdateDtoValidator()
        {
            RuleFor(i => i.PhoneNumber).NotEmpty().WithMessage("phone number can not be empty");
            RuleFor(i => i.Email).EmailAddress().NotEmpty().WithMessage("email address can not be empty and must be valid");
            RuleFor(i => i.Country).NotEmpty().WithMessage("country can not be empty");
        }
    }
}
