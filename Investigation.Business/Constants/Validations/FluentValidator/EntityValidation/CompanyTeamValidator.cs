using FluentValidation;
using Investigation.Domain.Entities;

namespace Investigation.Business.Constants.Validations.FluentValidator.EntityValidation
{
    public class CompanyTeamValidator:AbstractValidator<CompanyTeam>
    {
        public CompanyTeamValidator()
        {
            RuleFor(i => i.NameSurname).NotEmpty().WithMessage("name surname can not be null");
            RuleFor(i => i.Title).NotEmpty().WithMessage("title can not be null");
            RuleFor(i => i.Email).NotEmpty().EmailAddress().WithMessage("email can not be null and must be valid email address");
            RuleFor(i => i.TotalExperienceDuration).NotEmpty().WithMessage("total experience can not be null");
            RuleFor(i => i.PhotoUrl).NotEmpty().WithMessage("image can not be null");
        }
    }
}
