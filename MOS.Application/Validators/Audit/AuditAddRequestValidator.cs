using FluentValidation;
using MOS.Application.DTOs.Requests.Audit;

namespace MOS.Application.Validators.Audit
{
    public class AuditAddRequestValidator : AbstractValidator<AuditAddRequest>
    {
        public AuditAddRequestValidator()
        {

            // Name
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name must be less than 100 characters.");

            // UserName
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("UserName is required.")
                .MaximumLength(50).WithMessage("UserName must be less than 50 characters.");

            // Email
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Email must be a valid email address.")
                .MaximumLength(200).WithMessage("Email must be less than 200 characters.");

            // ObjectAffected
            RuleFor(x => x.ObjectAffected)
                .NotEmpty().WithMessage("ObjectAffected is required.")
                .MaximumLength(100).WithMessage("ObjectAffected must be less than 100 characters.");

            // Category
            RuleFor(x => x.Category)
                .IsInEnum().WithMessage("Category must be a valid CategoryLogType value.");

            // Action
            RuleFor(x => x.Action)
                .IsInEnum().WithMessage("Action must be a valid AuditAction value.");

            // Timestamp
            RuleFor(x => x.Timestamp)
                .NotEmpty().WithMessage("Timestamp is required.")
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Timestamp cannot be in the future.");
        }
    }
}
