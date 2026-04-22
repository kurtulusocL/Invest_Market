using FluentValidation;
using Investigation.Domain.Entities;

namespace Investigation.Business.Constants.Validations.FluentValidator.EntityValidation
{
    public class SavedContentValidator:AbstractValidator<SavedContent>
    {
        public SavedContentValidator()
        {
            RuleFor(i => i.SaveDate).NotEmpty().WithMessage("save date can not be empty");
            RuleFor(i => i.AppUserId).NotEmpty().WithMessage("user Id can not be empty");
        }
    }
}
