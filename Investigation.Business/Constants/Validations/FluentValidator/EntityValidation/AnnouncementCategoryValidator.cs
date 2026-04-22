using FluentValidation;
using Investigation.Domain.Entities;

namespace Investigation.Business.Constants.Validations.FluentValidator.EntityValidation
{
    public class AnnouncementCategoryValidator : AbstractValidator<AnnouncementCategory>
    {
        public AnnouncementCategoryValidator()
        {
            RuleFor(i => i.Name).NotEmpty().WithMessage("name can not be null");
        }
    }
}
