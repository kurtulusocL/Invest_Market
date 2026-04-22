using FluentValidation;
using Investigation.Domain.Entities;

namespace Investigation.Business.Constants.Validations.FluentValidator.EntityValidation
{
    public class ReportValidator:AbstractValidator<Report>
    {
        public ReportValidator()
        {
            RuleFor(i => i.Title).NotEmpty().WithMessage("title can not be empty");
            RuleFor(i => i.Subject).NotEmpty().WithMessage("subject can not be empty");
            RuleFor(i => i.AppUserId).NotEmpty().WithMessage("user Id can not be empty");
            RuleFor(i => i.ReportCategoryId).NotEmpty().WithMessage("category Id can not be empty");
        }
    }
}
