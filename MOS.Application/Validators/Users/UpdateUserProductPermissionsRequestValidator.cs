using FluentValidation;
using MOS.Application.DTOs.Requests.Users;

namespace MOS.Application.Validators.Users
{
    public class UpdateUserProductPermissionsRequestValidator
        : AbstractValidator<UpdateUserProductPermissionsRequest>
    {
        public UpdateUserProductPermissionsRequestValidator()
        {
            RuleFor(x => x.ProductIds)
                .NotEmpty()
                .WithMessage("At least one product must be assigned.");

            RuleForEach(x => x.ProductIds)
                .NotEmpty()
                .WithMessage("Product ID cannot be empty.");

            RuleFor(x => x.ProductIds)
                .Must(ids => ids.Distinct().Count() == ids.Count)
                .When(x => x.ProductIds != null && x.ProductIds.Any())
                .WithMessage("Duplicate product IDs are not allowed.");
        }
    }
}