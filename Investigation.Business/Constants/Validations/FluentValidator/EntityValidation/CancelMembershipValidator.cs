using FluentValidation;
using Investigation.Domain.Entities;

namespace Investigation.Business.Constants.Validations.FluentValidator.EntityValidation
{
    public class CancelMembershipValidator:AbstractValidator<CancelMembership>
    {
        public CancelMembershipValidator()
        {
            RuleFor(i => i.Title).NotEmpty().WithMessage("title can not be null");
            RuleFor(i => i.Desc).NotEmpty().WithMessage("description can not be null");
            RuleFor(i => i.AppUserId).NotEmpty().WithMessage("user Id can not be null");
            RuleFor(i => i.CancelMembershipCategoryId).NotEmpty().WithMessage("category Id can not be null");
        }
    }
}
