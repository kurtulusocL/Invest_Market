using FluentValidation;
using Investigation.Domain.Entities;

namespace Investigation.Business.Constants.Validations.FluentValidator.EntityValidation
{
    public class CompanyCategoryValidator:AbstractValidator<CompanyCategory>
    {
        public CompanyCategoryValidator()
        {
            RuleFor(i => i.Name).NotEmpty().WithMessage("name can not be null");
        }
    }
}
