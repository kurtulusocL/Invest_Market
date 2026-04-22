using FluentValidation;
using Investigation.Domain.Entities;

namespace Investigation.Business.Constants.Validations.FluentValidator.EntityValidation
{
    public class PictureValidator : AbstractValidator<Picture>
    {
        public PictureValidator()
        {
            RuleFor(i => i.ImageUrl).NotEmpty().WithMessage("image can not be empty");
        }
    }
}
