using FluentValidation;
using Investigation.Domain.Entities;

namespace Investigation.Business.Constants.Validations.FluentValidator.EntityValidation
{
    public class InvestorCategoryValidator:AbstractValidator<InvestorCategory>
    {
        public InvestorCategoryValidator()
        {
            RuleFor(i => i.Name).NotEmpty().WithMessage("name can not be null");
        }
    }
}
