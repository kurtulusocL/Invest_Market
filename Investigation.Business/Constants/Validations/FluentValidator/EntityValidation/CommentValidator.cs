using FluentValidation;
using Investigation.Domain.Entities;

namespace Investigation.Business.Constants.Validations.FluentValidator.EntityValidation
{
    public class CommentValidator:AbstractValidator<Comment>
    {
        public CommentValidator()
        {
            RuleFor(i => i.Text).NotEmpty().WithMessage("comment can not be empty");
            RuleFor(i => i.AppUserId).NotEmpty().WithMessage("user Id can not be empty");
        }
    }
}
