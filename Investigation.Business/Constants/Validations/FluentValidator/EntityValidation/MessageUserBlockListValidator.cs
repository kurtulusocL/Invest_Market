using FluentValidation;
using Investigation.Domain.Entities;

namespace Investigation.Business.Constants.Validations.FluentValidator.EntityValidation
{
    public class MessageUserBlockListValidator:AbstractValidator<MessageUserBlockList>
    {
        public MessageUserBlockListValidator()
        {
            RuleFor(i => i.BlockedId).NotEmpty().WithMessage("blocked Id can not be null");
            RuleFor(i => i.BlockerId).NotEmpty().WithMessage("blocker Id can not be null");
        }
    }
}
