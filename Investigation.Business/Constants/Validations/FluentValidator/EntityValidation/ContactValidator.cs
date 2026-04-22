using FluentValidation;
using Investigation.Domain.Entities;

namespace Investigation.Business.Constants.Validations.FluentValidator.EntityValidation
{
    public class ContactValidator:AbstractValidator<Contact>
    {
        public ContactValidator()
        {
            RuleFor(i => i.BusinessEmail).EmailAddress().NotEmpty().WithMessage("email address can not be empty and must be valid");
            RuleFor(i => i.OtherEmail).EmailAddress().NotEmpty().WithMessage("email address can not be empty and must be valid");
            RuleFor(i => i.Location).NotEmpty().WithMessage("location can not be empty");
        }
    }
}
