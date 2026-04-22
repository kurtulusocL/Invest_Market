using FluentValidation;
using Investigation.Domain.Entities;

namespace Investigation.Business.Constants.Validations.FluentValidator.EntityValidation
{
    public class UserProfileImageValidator:AbstractValidator<UserProfileImage>
    {
        public UserProfileImageValidator()
        {
            RuleFor(i => i.ImageUrl).NotEmpty().WithMessage("profile image can not be empty");
            RuleFor(i => i.AppUserId).NotEmpty().WithMessage("user Id can not be empty");
        }
    }
}
