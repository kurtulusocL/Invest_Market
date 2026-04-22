using FluentValidation;
using Investigation.Domain.Entities;

namespace Investigation.Business.Constants.Validations.FluentValidator.EntityValidation
{
    public class NewsValidator:AbstractValidator<News>
    {
        public NewsValidator()
        {
            RuleFor(i => i.Title).NotEmpty().WithMessage("title can not be empty");
            RuleFor(i => i.Desc).NotEmpty().WithMessage("description can not be empty");
            RuleFor(i => i.ImageUrl).NotEmpty().WithMessage("image can not be empty");
        }
    }
}
