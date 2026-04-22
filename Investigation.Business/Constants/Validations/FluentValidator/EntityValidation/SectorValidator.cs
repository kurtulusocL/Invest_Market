using FluentValidation;
using Investigation.Domain.Entities;

namespace Investigation.Business.Constants.Validations.FluentValidator.EntityValidation
{
    public class SectorValidator : AbstractValidator<Sector>
    {
        public SectorValidator()
        {
            RuleFor(i=>i.Name).NotEmpty().WithMessage("sector name can not be empty");
        }
    }
}
