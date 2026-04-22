using FluentValidation;
using Investigation.Shared.Dtos.AuthDtos.UserAuthDtos;

namespace Investigation.Business.Constants.Validations.FluentValidator.DTOValidation.AuthDtoValidation.UserAuthDtoValidation
{
    public class UserChangePasswordDtoValidator : AbstractValidator<UserChangePasswordDto>
    {
        public UserChangePasswordDtoValidator()
        {
            RuleFor(i => i.CurrentPassword).NotEmpty().WithMessage("current password can not be empty");
            RuleFor(i => i.NewPassword).MinimumLength(8).NotEmpty().WithMessage("new password can not be empty and must be min 8 characters");
            RuleFor(i => i.ConfirmNewPassword).MinimumLength(8).Equal(i => i.NewPassword).NotEmpty().WithMessage("comfirm new password can not be empty or password are not same and must be min 8 characters");
        }
    }
}
