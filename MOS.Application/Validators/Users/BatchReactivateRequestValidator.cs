using FluentValidation;
using MOS.Application.DTOs.Requests.Users;

namespace MOS.Application.Validators.Users
{
    public class BatchReactivateRequestValidator
        : AbstractValidator<BatchReactivateRequest>
    {
        public BatchReactivateRequestValidator()
        {
            RuleFor(x => x.UserIds)
                .NotEmpty()
                .WithMessage("At least one user is required.");

            RuleForEach(x => x.UserIds)
                .NotEmpty()
                .WithMessage("User ID cannot be empty.");

            RuleFor(x => x.UserIds)
                .Must(ids => ids.Distinct().Count() == ids.Count)
                .When(x => x.UserIds != null && x.UserIds.Any())
                .WithMessage("Duplicate user IDs are not allowed.");
        }
    }
}