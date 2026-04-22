using FluentValidation;
using Investigation.Domain.Entities;

namespace Investigation.Business.Constants.Validations.FluentValidator.EntityValidation
{
    public class LogoValidator:AbstractValidator<Logo>
    {
        public LogoValidator()
        {
            RuleFor(i => i.UseFor).NotEmpty().WithMessage("web page can not be emptry");
            RuleFor(i => i.ImageUrl).NotEmpty().WithMessage("image can not be emptry");
        }
    }
}
