using FluentValidation;
using MOS.Application.DTOs.Requests.EmailWhitelist;

namespace MOS.Application.Validators.EmailWhitelist
{
    public class AddEmailWhitelistRequestValidator
        : AbstractValidator<AddEmailWhitelistRequest>
    {
        public AddEmailWhitelistRequestValidator()
        {
            // Email
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Email must be a valid email address.")
                .MaximumLength(200).WithMessage("Email must be less than 200 characters.");
        }
    }
}