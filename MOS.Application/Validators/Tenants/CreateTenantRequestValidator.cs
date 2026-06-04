using FluentValidation;
using MOS.Application.DTOs.Requests.Tenants;

namespace MOS.Application.Validators.Tenants
{
    public class CreateTenantRequestValidator
        : AbstractValidator<CreateTenantRequest>
    {
        public CreateTenantRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Tenant name is required.")
                .MaximumLength(150).WithMessage("Tenant name must be less than 150 characters.")
                .Must(name => !string.IsNullOrWhiteSpace(name))
                .WithMessage("Tenant name cannot consist only of whitespace.");

            RuleFor(x => x.Slug)
                .NotEmpty().WithMessage("Tenant slug is required.")
                .MaximumLength(100).WithMessage("Tenant slug must be less than 100 characters.")
                .Matches("^[a-z0-9-]+$")
                .WithMessage("Tenant slug can only contain lowercase letters, numbers, and dashes.");

            RuleFor(x => x.Slug)
                .Must(slug => slug == slug.Trim())
                .WithMessage("Tenant slug cannot contain leading or trailing spaces.");
        }
    }
}