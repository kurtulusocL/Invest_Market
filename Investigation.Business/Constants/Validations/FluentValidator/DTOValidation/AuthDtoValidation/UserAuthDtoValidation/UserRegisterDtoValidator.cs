using FluentValidation;
using Investigation.Shared.Dtos.AuthDtos.UserAuthDtos;

namespace Investigation.Business.Constants.Validations.FluentValidator.DTOValidation.AuthDtoValidation.UserAuthDtoValidation
{
    public class UserRegisterDtoValidator:AbstractValidator<UserRegisterDto>
    {
        public UserRegisterDtoValidator()
        {
            RuleFor(i => i.NameSurname).NotEmpty().WithMessage("name surname can not be empty");
            RuleFor(i => i.Country).NotEmpty().WithMessage("country can not be empty");
            RuleFor(i => i.Birthdate).Must(date => date.Year < DateTime.Now.Year).NotEmpty().WithMessage("name surname can not be empty");
            RuleFor(i => i.PhoneNumber).NotEmpty().WithMessage("phone number can not be empty");
            RuleFor(i => i.Email).EmailAddress().NotEmpty().WithMessage("email address can not be empty and must be valid");
            RuleFor(i => i.Password).MinimumLength(8).NotEmpty().WithMessage("password can not be empty and must be min 8 characters");
            RuleFor(i => i.ConfirmPassword).MinimumLength(8).Equal(i=>i.Password).NotEmpty().WithMessage("confirm password can not be empty, must be min 8 characters and must be equal with password");
        }
    }
}
