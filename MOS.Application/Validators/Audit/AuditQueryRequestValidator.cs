using MOS.Application.DTOs.Requests.Audit;
using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;


namespace MOS.Application.Validators.Audit
{
    public class AuditQueryRequestValidator : AbstractValidator<AuditQueryRequest>
    {
        public AuditQueryRequestValidator()
        {
            {
                // Page must be >= 1
                RuleFor(x => x.Page)
                    .GreaterThanOrEqualTo(1)
                    .WithMessage("Page must be at least 1.");

                // PageSize must be between 1 and 100 (adjust as needed)
                RuleFor(x => x.PageSize)
                    .GreaterThanOrEqualTo(1).WithMessage("PageSize must be at least 1.")
                    .LessThanOrEqualTo(100).WithMessage("PageSize must not exceed 100.");

                // Search is optional, but if provided, limit length
                RuleFor(x => x.Search)
                    .MaximumLength(200).WithMessage("Search term must be less than 200 characters.");

            }
        }
    }
}
