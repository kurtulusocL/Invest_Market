using FluentValidation;
using Investigation.Domain.Entities;

namespace Investigation.Business.Constants.Validations.FluentValidator.EntityValidation
{
    public class CancelMembershipCategoryValidator:AbstractValidator<CancelMembershipCategory>
    {
        public CancelMembershipCategoryValidator()
        {
            RuleFor(i => i.Name).NotEmpty().WithMessage("name can not be null");
        }
    }
}
