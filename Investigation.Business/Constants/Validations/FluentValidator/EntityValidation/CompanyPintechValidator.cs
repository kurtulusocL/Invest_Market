using FluentValidation;
using Investigation.Domain.Entities;

namespace Investigation.Business.Constants.Validations.FluentValidator.EntityValidation
{
    public class CompanyPintechValidator:AbstractValidator<CompanyPintech>
    {
        public CompanyPintechValidator()
        {
            RuleFor(i => i.WorkPlan).NotEmpty().WithMessage("work plan can not be null");
            RuleFor(i => i.ServiceProduct).NotEmpty().WithMessage("service product can not be null");
            RuleFor(i => i.Description).NotEmpty().WithMessage("description can not be null");
            RuleFor(i => i.GrowingPotantial).NotEmpty().WithMessage("growing potantial can not be null");
            RuleFor(i => i.MarketingStrategy).NotEmpty().WithMessage("marketing strategy can not be null");
        }
    }
}
