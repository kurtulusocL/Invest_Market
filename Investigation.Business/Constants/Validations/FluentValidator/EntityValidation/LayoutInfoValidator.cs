using FluentValidation;
using Investigation.Domain.Entities;

namespace Investigation.Business.Constants.Validations.FluentValidator.EntityValidation
{
    public class LayoutInfoValidator:AbstractValidator<LayoutInfo>
    {
        public LayoutInfoValidator()
        {
            RuleFor(i => i.Title).NotEmpty().WithMessage("title can not be emptry");
            RuleFor(i => i.Author).NotEmpty().WithMessage("author can not be emptry");
            RuleFor(i => i.Keyword).NotEmpty().WithMessage("keyword can not be emptry");
            RuleFor(i => i.Content).NotEmpty().WithMessage("content can not be emptry");
            RuleFor(i => i.Language).NotEmpty().WithMessage("language can not be emptry");
        }
    }
}
