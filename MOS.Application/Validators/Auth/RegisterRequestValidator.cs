using MOS.Application.DTOs.Requests.Auth;
using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;


namespace MOS.Application.Validators.Auth
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            // TODO: validate Name - not empty, max length
            // TODO: validate Email - not empty, valid email format, max length
            // TODO: validate Password - not empty, min/max length
            // TODO: validate TenantName - not empty, max length
        }
    }
}
