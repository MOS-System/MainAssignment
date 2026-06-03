using FluentValidation;
using MOS.Application.DTOs.Requests.EmailWhitelist;

namespace MOS.Application.Validators.EmailWhitelist
{
    public class AddEmailWhitelistRequestValidator
        : AbstractValidator<AddEmailWhitelistRequest>
    {
        public AddEmailWhitelistRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Email must be a valid email address.")
                .MaximumLength(256).WithMessage("Email must be less than 256 characters.");
        }
    }
}