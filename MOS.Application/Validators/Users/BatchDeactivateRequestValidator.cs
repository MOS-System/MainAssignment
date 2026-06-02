using FluentValidation;
using MOS.Application.DTOs.Requests.Users;

namespace MOS.Application.Validators.Users
{
    public class BatchDeactivateRequestValidator
        : AbstractValidator<BatchDeactivateRequest>
    {
        public BatchDeactivateRequestValidator()
        {
            RuleFor(x => x.UserIds)
                .NotEmpty()
                .WithMessage("At least one user is required.");

            RuleForEach(x => x.UserIds)
                .GreaterThan(0)
                .WithMessage("User ID must be greater than 0.");
        }
    }
}
