using FluentValidation;
using Investigation.Domain.Entities;

namespace Investigation.Business.Constants.Validations.FluentValidator.EntityValidation
{
    public class BlackListValidator:AbstractValidator<BlackList>
    {
        public BlackListValidator()
        {
            RuleFor(i => i.RemoteIpAddress).NotEmpty().WithMessage("remote ip address can not be null");
            RuleFor(i => i.IpAddressVPN).NotEmpty().WithMessage("vpn ip address can not be null");
            RuleFor(i => i.ExpirationDate).NotEmpty().WithMessage("expiration date can not be null");
        }
    }
}
