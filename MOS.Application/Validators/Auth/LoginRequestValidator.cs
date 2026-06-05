using MOS.Application.DTOs.Requests.Auth;
using FluentValidation;


namespace MOS.Application.Validators.Auth
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            // Email rules
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Email must be a valid email address.")
                .MaximumLength(200).WithMessage("Email must be less than 200 characters.");

        }
    }
}
