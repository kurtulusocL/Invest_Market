using FluentValidation;
using Investigation.Domain.Entities;

namespace Investigation.Business.Constants.Validations.FluentValidator.EntityValidation
{
    public class UserSessionValidator : AbstractValidator<UserSession>
    {
        public UserSessionValidator()
        {
            RuleFor(i => i.Username).NotEmpty().WithMessage("Username can not be null");
            RuleFor(i => i.LoginDate).NotEmpty().GreaterThanOrEqualTo(i => DateTime.Now).WithMessage("Login Date can not be null");
            RuleFor(i => i.AppUserId).NotEmpty().WithMessage("AppUserId can not be null");
        }
    }
}
