using FluentValidation;
using Investigation.Domain.Entities;

namespace Investigation.Business.Constants.Validations.FluentValidator.EntityValidation
{
    public class CountryValidator : AbstractValidator<Country>
    {
        public CountryValidator()
        {
            RuleFor(i => i.Name).NotEmpty().WithMessage("name can not be empty");
        }
    }
}
