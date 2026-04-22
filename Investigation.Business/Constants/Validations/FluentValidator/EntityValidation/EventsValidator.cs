using FluentValidation;
using Investigation.Domain.Entities;

namespace Investigation.Business.Constants.Validations.FluentValidator.EntityValidation
{
    public class EventsValidator : AbstractValidator<Events>
    {
        public EventsValidator()
        {
            RuleFor(i => i.Title).NotEmpty().WithMessage("title can not be empty");
            RuleFor(i => i.StartedDate).NotEmpty().WithMessage("start date can not be empty");
            RuleFor(i => i.EndDate).NotEmpty().WithMessage("end date can not be empty");
            RuleFor(i => i.DurationDay).NotEmpty().WithMessage("duration day can not be empty");
            RuleFor(i => i.Content).NotEmpty().WithMessage("content can not be empty");
            RuleFor(i => i.Location).NotEmpty().WithMessage("location can not be empty");
            RuleFor(i => i.EventsCategoryId).NotEmpty().WithMessage("cateogory Id can not be empty");
        }
    }
}
