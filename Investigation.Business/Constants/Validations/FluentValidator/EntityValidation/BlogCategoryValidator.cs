using FluentValidation;
using Investigation.Domain.Entities;

namespace Investigation.Business.Constants.Validations.FluentValidator.EntityValidation
{
    public class BlogCategoryValidator : AbstractValidator<BlogCategory>
    {
        public BlogCategoryValidator()
        {
            RuleFor(i => i.Name).NotEmpty().WithMessage("name can not be null");
        }
    }
}
