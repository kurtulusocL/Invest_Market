using FluentValidation;
using Investigation.Domain.Entities;

namespace Investigation.Business.Constants.Validations.FluentValidator.EntityValidation
{
    public class CompanyStageValidator:AbstractValidator<CompanyStage>
    {
        public CompanyStageValidator()
        {
            RuleFor(i => i.StageName).NotEmpty().WithMessage("name can not be empty");
            RuleFor(i => i.StageValue).NotEmpty().WithMessage("value can not be empty");
        }
    }
}
