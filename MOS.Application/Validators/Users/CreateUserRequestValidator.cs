using MOS.Application.DTOs.Requests.Users;
using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;


namespace MOS.Application.Validators.Users
{
    public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
    {
        public CreateUserRequestValidator()
        {
            // TODO: validate Name - not empty, max length
            // TODO: validate Email - not empty, valid email format, max length
            // TODO: validate Role - must be valid enum value
            // TODO: validate ProductIds - not empty when role is TenantUser
        }
    }
}
