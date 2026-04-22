using FluentValidation;
using Investigation.Domain.Entities;

namespace Investigation.Business.Constants.Validations.FluentValidator.EntityValidation
{
    public class SocialMediaValidator : AbstractValidator<SocialMedia>
    {
        public SocialMediaValidator()
        {
            RuleFor(i => i.Name).NotEmpty().WithMessage("account name can not be empty");
            RuleFor(i => i.Url).NotEmpty().Must(url => Uri.TryCreate(url, UriKind.Absolute, out var uriResult) && uriResult.Scheme == Uri.UriSchemeHttps && !string.IsNullOrWhiteSpace(uriResult.Host)).WithMessage("account url can not be empty and must be valid");
            RuleFor(i => i.IconUrl).NotEmpty().WithMessage("account icon can not be empty");
        }
    }
}
