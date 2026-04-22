using FluentValidation;
using Investigation.Domain.Entities;

namespace Investigation.Business.Constants.Validations.FluentValidator.EntityValidation
{
    public class CompanyFinanceValidator:AbstractValidator<CompanyFinance>
    {
        public CompanyFinanceValidator()
        {
            RuleFor(i => i.TotalIncome).NotEmpty().WithMessage("total income value can not be empty and must be valid number");
        }
    }
}
