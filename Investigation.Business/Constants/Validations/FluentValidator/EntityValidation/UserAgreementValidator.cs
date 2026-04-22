using FluentValidation;
using Investigation.Domain.Entities;

namespace Investigation.Business.Constants.Validations.FluentValidator.EntityValidation
{
    public class UserAgreementValidator:AbstractValidator<UserAgreement>
    {
        public UserAgreementValidator()
        {
            RuleFor(i => i.Title).NotEmpty().WithMessage("title can not be empty");
            RuleFor(i => i.Desc).NotEmpty().WithMessage("description can not be empty");
        }
    }
}
