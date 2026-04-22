using FluentValidation;
using Investigation.Domain.Entities;

namespace Investigation.Business.Constants.Validations.FluentValidator.EntityValidation
{
    public class AnnouncementValidator : AbstractValidator<Announcement>
    {
        public AnnouncementValidator()
        {
            RuleFor(i => i.Title).NotEmpty().WithMessage("title can not be empty");
            RuleFor(i => i.Content).NotEmpty().WithMessage("content can not be empty");
            RuleFor(i => i.AnnouncementCategoryId).NotEmpty().WithMessage("category Id can not be empty");
        }
    }
}
