using FluentValidation;
using MOS.Application.DTOs.Requests.Auth;
using MOS.Domain.Entities;


namespace MOS.Application.Validators.Auth
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            {


                // UserName
                RuleFor(x => x.UserName)
                    .NotEmpty().WithMessage("UserId is required.")
                    .MaximumLength(50).WithMessage("UserId must be less than 50 characters.");

                // Name
                RuleFor(x => x.Name)
                    .NotEmpty().WithMessage("Name is required.")
                    .MaximumLength(200).WithMessage("Name must be less than 200 characters.");

                //TenantId
                RuleFor(x => x.TenantId)
               .NotEmpty().WithMessage("TenantId is required and cannot be empty.");

                // Email
                RuleFor(x => x.Email)
                    .NotEmpty().WithMessage("Email is required.")
                    .EmailAddress().WithMessage("Email must be a valid email address.")
                    .MaximumLength(200).WithMessage("Email must be less than 200 characters.");

                // Password
                RuleFor(x => x.Password)
                    .NotEmpty().WithMessage("Password is required.")
                    .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
                    .MaximumLength(100).WithMessage("Password must be less than 100 characters.")
                    .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                    .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                    .Matches("[0-9]").WithMessage("Password must contain at least one number.")
                    .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.");

                // Phone
                RuleFor(x => x.Phone)
                    .NotEmpty().WithMessage("Phone number is required.")
                    .Matches(@"^\+?[0-9]{7,15}$").WithMessage("Phone number must be valid and contain 7–15 digits.");

            }
        }
    }
}
