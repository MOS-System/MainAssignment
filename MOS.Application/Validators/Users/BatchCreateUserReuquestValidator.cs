using MOS.Application.DTOs.Requests.Users;
using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;


namespace MOS.Application.Validators.Users
{
    public class BatchCreateUserRequestValidator : AbstractValidator<BatchCreateUserRequest>
    {
        public BatchCreateUserRequestValidator()
        {
            // TODO: validate Users list - not empty
            // TODO: validate each user using CreateUserRequestValidator
        }
    }
}
