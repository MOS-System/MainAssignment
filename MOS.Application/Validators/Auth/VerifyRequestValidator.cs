using FluentValidation;
using MOS.Application.DTOs.Requests.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace MOS.Application.Validators.Auth
{
    internal class VerifyRequestValidator : AbstractValidator<VerifyRequest>
    {
        public VerifyRequestValidator()
        {
            // Email rules
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Email must be a valid email address.")
                .MaximumLength(200).WithMessage("Email must be less than 200 characters.");

            // Password rules
            RuleFor(x => x.Password)
           .NotEmpty().WithMessage("Password is required.")
           .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
           .MaximumLength(100).WithMessage("Password must be less than 100 characters.");

            // MFA Code rules
            RuleFor(x => x.MfaCode)
                .Length(6).WithMessage("MFA code must be exactly 6 digits.")
                .Matches(@"^\d{6}$").WithMessage("MFA code must contain only digits.");
        }
    }
}

