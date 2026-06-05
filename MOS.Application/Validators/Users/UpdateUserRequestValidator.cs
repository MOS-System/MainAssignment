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
                .MaximumLength(100).WithMessage("Username must be less than 100 characters.")
                .Matches("^[A-Za-z0-9]+$").WithMessage("Username may only contain letters and numbers.")
                .When(x => !string.IsNullOrWhiteSpace(x.UserName));

            RuleFor(x => x.Name)
                .MaximumLength(100).WithMessage("Name must be less than 100 characters.")
                .When(x => !string.IsNullOrWhiteSpace(x.Name));

            RuleFor(x => x.Phone)
                .Matches(@"^\d{10}$").WithMessage("Phone number must contain exactly 10 digits.")
                .When(x => !string.IsNullOrWhiteSpace(x.Phone));

            RuleFor(x => x.Role)
                .IsInEnum()
                .When(x => x.Role.HasValue)
                .WithMessage("Invalid role.");

            RuleForEach(x => x.ProductIds)
                .NotEmpty()
                .When(x => x.ProductIds != null)
                .WithMessage("Product ID cannot be empty.");

            RuleFor(x => x.ProductIds)
                .Must(ids => ids == null || ids.Distinct().Count() == ids.Count)
                .WithMessage("Duplicate product IDs are not allowed.");

            RuleFor(x => x.ProductIds)
                .NotEmpty()
                .When(x => x.Role == RoleType.TenantUser && x.ProductIds != null)
                .WithMessage("Tenant users must have at least one assigned product.");

            RuleFor(x => x.ProductIds)
                .Empty()
                .When(x => x.Role == RoleType.Administrator && x.ProductIds != null)
                .WithMessage("Administrators automatically have access to all products.");

            RuleFor(x => x.ProductIds)
                .Empty()
                .When(x => x.Role == RoleType.TenantAdministrator && x.ProductIds != null)
                .WithMessage("Tenant administrators manage permissions and do not require product assignments.");
        }
    }
}