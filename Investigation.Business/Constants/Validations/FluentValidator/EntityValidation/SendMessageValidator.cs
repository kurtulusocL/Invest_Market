using FluentValidation;
using Investigation.Domain.Entities;

namespace Investigation.Business.Constants.Validations.FluentValidator.EntityValidation
{
    public class SendMessageValidator:AbstractValidator<SendMessage>
    {
        public SendMessageValidator()
        {
            RuleFor(i => i.NameSurname).NotEmpty().WithMessage("name surname can not be empty");
            RuleFor(i => i.Email).NotEmpty().EmailAddress().WithMessage("email address can not be empty and must be valid");
            RuleFor(i => i.PhoneNumber).NotEmpty().WithMessage("phone number can not be empty");
            RuleFor(i => i.MessageTitle).NotEmpty().WithMessage("message title can not be empty");
            RuleFor(i => i.MessageSubject).NotEmpty().WithMessage("message subtitle can not be empty");
            RuleFor(i => i.MessageContent).NotEmpty().MinimumLength(150).MaximumLength(1000).WithMessage("message can not be empty and message must be between min 150 characters and max 1000 characters.");
        }
    }
}
