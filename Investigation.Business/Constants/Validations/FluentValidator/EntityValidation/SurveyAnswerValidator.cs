using FluentValidation;
using Investigation.Domain.Entities;

namespace Investigation.Business.Constants.Validations.FluentValidator.EntityValidation
{
    public class SurveyAnswerValidator:AbstractValidator<SurveyAnswer>
    {
        public SurveyAnswerValidator()
        {
            RuleFor(i => i.AppUserId).NotEmpty().WithMessage("user Id can not be empty");
        }
    }
}
