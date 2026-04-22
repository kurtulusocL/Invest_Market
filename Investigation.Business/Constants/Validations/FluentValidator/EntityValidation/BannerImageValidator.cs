using FluentValidation;
using Investigation.Domain.Entities;

namespace Investigation.Business.Constants.Validations.FluentValidator.EntityValidation
{
    public class BannerImageValidator:AbstractValidator<BannerImage>
    {
        public BannerImageValidator()
        {
            RuleFor(i => i.ControllerName).NotEmpty().WithMessage("page can not be empty");
            RuleFor(i => i.Image).NotEmpty().WithMessage("image not be empty");
        }
    }
}
