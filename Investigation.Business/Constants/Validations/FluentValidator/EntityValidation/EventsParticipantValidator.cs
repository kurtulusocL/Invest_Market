using FluentValidation;
using Investigation.Domain.Entities;

namespace Investigation.Business.Constants.Validations.FluentValidator.EntityValidation
{
    public class EventsParticipantValidator:AbstractValidator<EventsParticipant>
    {
        public EventsParticipantValidator()
        {
            RuleFor(i => i.NameSurname).NotEmpty().WithMessage("name surname can not be empty");
            RuleFor(i => i.Title).NotEmpty().WithMessage("title can not be empty");
            RuleFor(i => i.JoinTime).NotEmpty().WithMessage("join time can not be empty");
            RuleFor(i => i.ImageUrl).NotEmpty().WithMessage("image can not be empty");
        }
    }
}
