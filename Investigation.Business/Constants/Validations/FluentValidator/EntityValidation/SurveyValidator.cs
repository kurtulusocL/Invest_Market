using FluentValidation;
using Investigation.Domain.Entities;

namespace Investigation.Business.Constants.Validations.FluentValidator.EntityValidation
{
    public class SurveyValidator:AbstractValidator<Survey>
    {
        public SurveyValidator()
        {
            RuleFor(i => i.Title).NotEmpty().WithMessage("title can not be empty");
            RuleFor(i => i.Desc).NotEmpty().WithMessage("description can not be empty");
            RuleFor(i => i.StartDate).NotEmpty().WithMessage("start date can not be empty");
            RuleFor(i => i.ClosedDate).NotEmpty().WithMessage("close date can not be empty");
            RuleFor(i => i.AppUserId).NotEmpty().WithMessage("user Id can not be empty");
        }
    }
}
