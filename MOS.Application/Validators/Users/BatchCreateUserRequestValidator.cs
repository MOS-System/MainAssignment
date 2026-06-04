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

            RuleFor(x => x.Users)
                .Must(users =>
                    users.Select(u => u.Email.Trim().ToLower())
                         .Distinct()
                         .Count() == users.Count)
                .When(x => x.Users != null && x.Users.Any())
                .WithMessage("Duplicate emails are not allowed within the batch.");
        }
    }
}