using FluentValidation;
using Investigation.Domain.Entities;

namespace Investigation.Business.Constants.Validations.FluentValidator.EntityValidation
{
    public class MessageValidator : AbstractValidator<Message>
    {
        public MessageValidator()
        {
            RuleFor(i => i.Content).NotEmpty().WithMessage("content can not be empty");
            RuleFor(i => i.SentAt).NotEmpty().WithMessage("send date can not be empty");
            RuleFor(i => i.SenderId).NotEmpty().WithMessage("sender Id can not be empty");
            RuleFor(i => i.ReceiverId).NotEmpty().WithMessage("reciever Id can not be empty");
        }
    }
}
