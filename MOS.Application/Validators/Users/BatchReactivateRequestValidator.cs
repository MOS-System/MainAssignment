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

        }
    }
}