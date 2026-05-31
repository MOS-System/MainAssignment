using MOS.Application.DTOs.Requests.Auth;
using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;


namespace MOS.Application.Validators.Auth
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            // TODO: validate Email - not empty, valid email format, max length
            // TODO: validate Password - not empty, min/max length
        }
    }
}
