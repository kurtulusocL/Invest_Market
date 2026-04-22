using FluentValidation;
using Investigation.Domain.Entities;

namespace Investigation.Business.Constants.Validations.FluentValidator.EntityValidation
{
    public class SliderValidator:AbstractValidator<Slider>
    {
        public SliderValidator()
        {
            RuleFor(i=>i.ImageUrl).NotEmpty().WithMessage("image can not be null");
        }
    }
}
