using FluentValidation;
using Investigation.Domain.Entities;

namespace Investigation.Business.Constants.Validations.FluentValidator.EntityValidation
{
    public class QuestionOptionValidator:AbstractValidator<QuestionOption>
    {
        public QuestionOptionValidator()
        {
            RuleFor(i => i.OptionText).NotEmpty().WithMessage("option text can not be empty");
            RuleFor(i => i.OrderIndex).NotEmpty().WithMessage("order index can not be empty");
            RuleFor(i => i.SurveyQuestionId).NotEmpty().WithMessage("survey question Id can not be empty");
        }
    }
}
