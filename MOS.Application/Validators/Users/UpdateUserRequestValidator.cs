using FluentValidation;
using MOS.Application.DTOs.Requests.Users;
using MOS.Domain.Enums;

namespace MOS.Application.Validators.Users
{
    public class UpdateUserRequestValidator
        : AbstractValidator<UpdateUserRequest>
    {
        public UpdateUserRequestValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("User ID is required.")
                .MaximumLength(10).WithMessage("User ID must be less than 10 characters.")
                .Matches("^[A-Za-z0-9]+$")
                .WithMessage("User ID may only contain letters and numbers.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name must be less than 100 characters.");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^\d{10}$")
                .WithMessage("Phone number must contain exactly 10 digits.");

            RuleFor(x => x.Role)
                .IsInEnum()
                .WithMessage("Invalid role.");

            RuleFor(x => x.ProductIds)
                .NotEmpty()
                .When(x => x.Role == RoleType.TenantUser)
                .WithMessage("Tenant users must have at least one assigned product.");

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
