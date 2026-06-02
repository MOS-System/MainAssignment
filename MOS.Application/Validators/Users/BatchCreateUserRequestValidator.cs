using FluentValidation;
using MOS.Application.DTOs.Requests.Users;

namespace MOS.Application.Validators.Users
{
    public class BatchCreateUserRequestValidator
        : AbstractValidator<BatchCreateUserRequest>
    {
        public BatchCreateUserRequestValidator()
        {
            RuleFor(x => x.Users)
                .NotEmpty()
                .WithMessage("At least one user is required.");

            RuleForEach(x => x.Users)
                .SetValidator(new CreateUserRequestValidator());
        }
    }
}