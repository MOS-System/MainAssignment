using MOS.Application.DTOs.Requests.Users;
using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;


namespace MOS.Application.Validators.Users
{
    public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
    {
        public UpdateUserRequestValidator()
        {
            // TODO: validate Name - not empty, max length
            // TODO: validate Role - must be valid enum value
            // TODO: validate ProductIds - not empty when role is TenantUser
        }
    }

}
