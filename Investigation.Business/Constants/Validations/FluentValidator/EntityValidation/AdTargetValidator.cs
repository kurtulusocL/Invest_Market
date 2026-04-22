using FluentValidation;
using Investigation.Domain.Entities;

namespace Investigation.Business.Constants.Validations.FluentValidator.EntityValidation
{
    public class AdTargetValidator : AbstractValidator<AdTarget>
    {
        public AdTargetValidator()
        {
            RuleFor(i => i.MinInteractionCount).NotEmpty();
            RuleFor(i => i.IncludeBlogInteractions).NotEmpty();
            RuleFor(i => i.IncludeInvestorInteractions).NotEmpty();
            RuleFor(i => i.IncludeCompanyInteractions).NotEmpty();
            RuleFor(i => i.IncludePostInteractions).NotEmpty();
            RuleFor(i => i.AdId).NotEmpty().WithMessage("ad Id can not be null");
        }
    }
}
