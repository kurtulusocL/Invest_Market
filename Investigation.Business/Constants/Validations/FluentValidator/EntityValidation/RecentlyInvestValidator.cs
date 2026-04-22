using FluentValidation;
using Investigation.Domain.Entities;

namespace Investigation.Business.Constants.Validations.FluentValidator.EntityValidation
{
    public class RecentlyInvestValidator : AbstractValidator<RecentlyInvest>
    {
        public RecentlyInvestValidator()
        {
            RuleFor(i => i.Title).NotEmpty().WithMessage("title can not be empty");
            RuleFor(i => i.InvestDate).NotEmpty().WithMessage("invest date can not be empty");
            RuleFor(i => i.SectorId).NotEmpty().WithMessage("sector Id can not be empty");
        }
    }
}
