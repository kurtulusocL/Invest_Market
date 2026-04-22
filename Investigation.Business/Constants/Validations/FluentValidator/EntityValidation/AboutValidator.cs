using FluentValidation;
using Investigation.Domain.Entities;

namespace Investigation.Business.Constants.Validations.FluentValidator.EntityValidation
{
    public class AboutValidator : AbstractValidator<About>
    {
        public AboutValidator()
        {
            RuleFor(i => i.Title).NotEmpty().WithMessage("Title can not be null");
            RuleFor(i => i.Subtitle).NotEmpty().WithMessage("Subtitle can not be null");           
            RuleFor(i => i.Desc).NotEmpty().WithMessage("Desc can not be null");
            RuleFor(i => i.ImageUrl).NotEmpty().WithMessage("ImageUrl can not be null");
        }        
    }
}
