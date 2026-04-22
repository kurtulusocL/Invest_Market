using FluentValidation;
using Investigation.Domain.Entities;

namespace Investigation.Business.Constants.Validations.FluentValidator.EntityValidation
{
    public class ReportCategoryValidator:AbstractValidator<ReportCategory>
    {
        public ReportCategoryValidator()
        {
            RuleFor(i=>i.Name).NotEmpty().WithMessage("name can not be empty");
        }
    }
}
