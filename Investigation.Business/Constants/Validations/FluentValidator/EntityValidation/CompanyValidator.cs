using FluentValidation;
using Investigation.Domain.Entities;

namespace Investigation.Business.Constants.Validations.FluentValidator.EntityValidation
{
    public class CompanyValidator : AbstractValidator<Company>
    {
        public CompanyValidator()
        {
            RuleFor(i => i.Name).NotEmpty().WithMessage("name can not be empty");
            RuleFor(i => i.Slogan).NotEmpty().WithMessage("slogan can not be empty");
            RuleFor(i => i.ShortBio).NotEmpty().WithMessage("short bio can not be empty");
            RuleFor(i => i.Desc).NotEmpty().WithMessage("description can not be empty");
            RuleFor(i => i.FoundationDate).NotEmpty().Must(date => date <= DateTime.Today).WithMessage("foundation date can not be empty and must be valid");
            RuleFor(i => i.LinkedIn).NotEmpty().Must(url => Uri.TryCreate(url, UriKind.Absolute, out var uriResult) && uriResult.Scheme == Uri.UriSchemeHttps && !string.IsNullOrWhiteSpace(uriResult.Host)).WithMessage("linkedin can not be empty and must be valid");
            RuleFor(i => i.LogoUrl).NotEmpty().WithMessage("logo can not be empty");
            RuleFor(i => i.AppUserId).NotEmpty().WithMessage("user Id can not be empty");
            RuleFor(i => i.CompanyCategoryId).NotEmpty().WithMessage("category Id can not be empty");
            RuleFor(i => i.CountryId).NotEmpty().WithMessage("country Id can not be empty");
            RuleFor(i => i.SectorId).NotEmpty().WithMessage("sector Id can not be empty");
        }
    }
}
