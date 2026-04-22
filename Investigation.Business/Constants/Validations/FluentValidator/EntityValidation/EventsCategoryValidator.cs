using FluentValidation;
using Investigation.Domain.Entities;

namespace Investigation.Business.Constants.Validations.FluentValidator.EntityValidation
{
    public class EventsCategoryValidator:AbstractValidator<EventsCategory>
    {
        public EventsCategoryValidator()
        {
            RuleFor(i => i.Name).NotEmpty().WithMessage("name can not be empty");
        }
    }
}
