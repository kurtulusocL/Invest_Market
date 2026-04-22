using FluentValidation;
using Investigation.Domain.Entities;

namespace Investigation.Business.Constants.Validations.FluentValidator.EntityValidation
{
    public class BlogValidator:AbstractValidator<Blog>
    {
        public BlogValidator()
        {
            RuleFor(i => i.Title).NotEmpty().WithMessage("Title can not be null");
            RuleFor(i => i.Subtitle).NotEmpty().WithMessage("Subtitle can not be null");           
            RuleFor(i => i.Content).NotEmpty().WithMessage("Content can not be null and must be valid");
            RuleFor(i => i.CoverImage).NotEmpty().WithMessage("CoverImage can not be null");
            RuleFor(i => i.AppUserId).NotEmpty().WithMessage("AppUserId can not be null");
            RuleFor(i => i.BlogCategoryId).NotEmpty().WithMessage("CategoryId can not be null");
        }
    }
}
