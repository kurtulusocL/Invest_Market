using FluentValidation;
using Investigation.Domain.Entities;

namespace Investigation.Business.Constants.Validations.FluentValidator.EntityValidation
{
    public class CompanyContactValidator : AbstractValidator<CompanyContact>
    {
        public CompanyContactValidator()
        {
            RuleFor(i => i.Website).NotEmpty()
                .Must(url => Uri.TryCreate(url, UriKind.Absolute, out var uriResult) && uriResult.Scheme == Uri.UriSchemeHttps && !string.IsNullOrWhiteSpace(uriResult.Host))
                .WithMessage("website url can not be empty and mustt be valid");
            RuleFor(i => i.Email).NotEmpty().EmailAddress().WithMessage("email can not be empty and must be valid");
            RuleFor(i => i.Location).NotEmpty().WithMessage("location can not be empty");
        }
    }
}
