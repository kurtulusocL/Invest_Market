using FluentValidation;
using Investigation.Domain.Entities;

namespace Investigation.Business.Constants.Validations.FluentValidator.EntityValidation
{
    public class SubsectorValidator:AbstractValidator<SubSector>
    {
        public SubsectorValidator()
        {
            RuleFor(i=>i.Name).NotEmpty().WithMessage("name can not be empty");
        }
    }
}
