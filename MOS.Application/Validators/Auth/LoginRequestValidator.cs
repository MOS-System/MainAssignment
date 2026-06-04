using MOS.Application.DTOs.Requests.Auth;
using FluentValidation;


namespace MOS.Application.Validators.Auth
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            // Email
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Email must be a valid email address.")
                .MaximumLength(200).WithMessage("Email must be less than 200 characters.");

            // Password
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(1).WithMessage("Password must be at least 1 characters long.")
                .MaximumLength(100).WithMessage("Password must be less than 100 characters.");
        }
    }
}
