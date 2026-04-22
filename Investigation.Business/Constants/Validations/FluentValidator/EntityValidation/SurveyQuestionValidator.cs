using FluentValidation;
using Investigation.Domain.Entities;

namespace Investigation.Business.Constants.Validations.FluentValidator.EntityValidation
{
    public class SurveyQuestionValidator:AbstractValidator<SurveyQuestion>
    {
        public SurveyQuestionValidator()
        {
            RuleFor(i => i.QuestionText).NotEmpty().WithMessage("question text can not be empty");
            RuleFor(i => i.OrderIndex).NotEmpty().WithMessage("order index can not be empty");
        }
    }
}
