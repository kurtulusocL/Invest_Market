using FluentValidation;
using Investigation.Domain.Entities;

namespace Investigation.Business.Constants.Validations.FluentValidator.EntityValidation
{
    public class InvestorValidator:AbstractValidator<Investor>
    {
        public InvestorValidator()
        {
            RuleFor(i => i.Bio).NotEmpty().WithMessage("bio can not be empty");
            RuleFor(i => i.InvestArea).NotEmpty().WithMessage("invest area can not be empty");
            RuleFor(i => i.SinceWhen).NotEmpty().WithMessage("first invest date can not be empty");
            RuleFor(i => i.AppUserId).NotEmpty().WithMessage("user Id can not be empty");
            RuleFor(i => i.CountryId).NotEmpty().WithMessage("country Id can not be empty");
            RuleFor(i => i.InvestorCategoryId).NotEmpty().WithMessage("category Id can not be empty");
        }
    }
}
