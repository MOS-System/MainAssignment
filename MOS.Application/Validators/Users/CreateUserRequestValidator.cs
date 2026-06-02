using FluentValidation;
using MOS.Application.DTOs.Requests.Users;
using MOS.Domain.Enums;

namespace MOS.Application.Validators.Users
{
    public class CreateUserRequestValidator
        : AbstractValidator<CreateUserRequest>
    {
        public CreateUserRequestValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User ID is required.")
                .MaximumLength(10).WithMessage("User ID must be less than 10 characters.")
                .Matches("^[A-Za-z0-9]+$")
                .WithMessage("User ID may only contain letters and numbers.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name must be less than 100 characters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Email must be a valid email address.")
                .MaximumLength(256).WithMessage("Email must be less than 256 characters.");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^\d{10}$")
                .WithMessage("Phone number must contain exactly 10 digits.");

            RuleFor(x => x.Role)
                .IsInEnum()
                .WithMessage("Invalid role.");

            RuleFor(x => x.TenantId)
                .GreaterThan(0)
                .WithMessage("TenantId must be greater than 0.");

            RuleForEach(x => x.ProductIds)
                .GreaterThan(0)
                .WithMessage("ProductId must be greater than 0.");

            // TenantUser must have products assigned
            RuleFor(x => x.ProductIds)
                .NotEmpty()
                .When(x => x.Role == RoleType.TenantUser)
                .WithMessage("Tenant users must have at least one assigned product.");

            // Admins should not receive explicit product assignments
            RuleFor(x => x.ProductIds)
                .Empty()
                .When(x => x.Role == RoleType.Administrator)
                .WithMessage("Administrators automatically have access to all products.");

            RuleFor(x => x.ProductIds)
                .Empty()
                .When(x => x.Role == RoleType.TenantAdministrator)
                .WithMessage("Tenant administrators manage permissions and do not require product assignments.");
        }
    }
}