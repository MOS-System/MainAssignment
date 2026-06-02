using MOS.Application.DTOs.Requests.Users;
using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;


namespace MOS.Application.Validators.Users
{
    public class BatchDeleteRequestValidator : AbstractValidator<BatchDeleteRequest>
    {
        public BatchDeleteRequestValidator()
        {
            RuleFor(x => x.UserIds)
                .NotEmpty()
                .WithMessage("At least one user is required");

            RuleForEach(x => x.UserIds)
                .GreaterThan(0)
                .WithMessage("User ID must be greater than 0");
        }
    }
}
