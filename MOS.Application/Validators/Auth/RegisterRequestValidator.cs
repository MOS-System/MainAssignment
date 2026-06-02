using MOS.Application.DTOs.Requests.Auth;
using FluentValidation;


namespace MOS.Application.Validators.Auth
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            // Name
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(500).WithMessage("Name must be less than 500 characters.");

            // Email
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Email must be a valid email address.")
                .MaximumLength(200).WithMessage("Email must be less than 200 characters.");

            // Password
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches("[0-9]").WithMessage("Password must contain at least one number.")
                .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.");

            // TenantName
            //RuleFor(x => x.TenantName)
            //    .NotEmpty().WithMessage("Tenant name is required.")
            //    .MaximumLength(150).WithMessage("Tenant name must be less than 150 characters.")
            //    .Matches("^[a-zA-Z0-9\\-\\s]+$").WithMessage("Tenant name can only contain letters, numbers, spaces, and dashes.");

        }
    }
}
