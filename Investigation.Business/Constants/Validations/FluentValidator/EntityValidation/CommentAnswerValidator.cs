using FluentValidation;
using Investigation.Domain.Entities;

namespace Investigation.Business.Constants.Validations.FluentValidator.EntityValidation
{
    public class CommentAnswerValidator:AbstractValidator<CommentAnswer>
    {
        public CommentAnswerValidator()
        {
            RuleFor(i => i.Text).NotEmpty().WithMessage("answer can not be empty");
            RuleFor(i => i.AppUserId).NotEmpty().WithMessage("user Id can not be empty");
        }
    }
}
