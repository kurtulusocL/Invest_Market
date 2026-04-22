using FluentValidation;
using Investigation.Domain.Entities;

namespace Investigation.Business.Constants.Validations.FluentValidator.EntityValidation
{
    public class AdValidator:AbstractValidator<Ad>
    {
        public AdValidator()
        {
            RuleFor(i => i.CompanyName).NotEmpty().WithMessage("company name can not be empty");
            RuleFor(i => i.StartDate).NotEmpty().WithMessage("start date can not be empty");
            RuleFor(i => i.FinishDate).NotEmpty().WithMessage("finish date can not be empty");
            RuleFor(i => i.ImageUrl).NotEmpty().WithMessage("image url can not be empty");
        }
    }
}
