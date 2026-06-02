using FluentValidation;
using MOS.Application.DTOs.Requests.Users;

namespace MOS.Application.Validators.Users
{
    public class UserQueryRequestValidator : AbstractValidator<UserQueryRequest>
    {
        public UserQueryRequestValidator()
        {
            RuleFor(x => x.Page)
                .GreaterThan(0)
                .WithMessage("Page must be greater than 0.");

            RuleFor(x => x.PageSize)
                .InclusiveBetween(1, 100)
                .WithMessage("PageSize must be between 1 and 100.");

            RuleFor(x => x.SortBy)
                .Must(sortBy =>
                    string.IsNullOrWhiteSpace(sortBy) ||
                    new[] { "id", "userId", "name", "email", "phone", "status", "role", "createdAt" }
                        .Contains(sortBy))
                .WithMessage("SortBy must be one of: id, userId, name, email, phone, status, role, createdAt.");

            RuleFor(x => x.SortDirection)
                .Must(direction =>
                    string.IsNullOrWhiteSpace(direction) ||
                    direction.ToLower() == "asc" ||
                    direction.ToLower() == "desc")
                .WithMessage("SortDirection must be asc or desc.");

            RuleFor(x => x.Search)
                .MaximumLength(200)
                .WithMessage("Search must be less than 200 characters.");

            RuleFor(x => x.StatusFilter)
                .IsInEnum()
                .When(x => x.StatusFilter.HasValue)
                .WithMessage("Invalid status filter.");

            RuleFor(x => x.RoleFilter)
                .IsInEnum()
                .When(x => x.RoleFilter.HasValue)
                .WithMessage("Invalid role filter.");
        }
    }
}