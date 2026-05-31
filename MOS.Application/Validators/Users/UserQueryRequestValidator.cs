using MOS.Application.DTOs.Requests.Users;
using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;


namespace MOS.Application.Validators.Users
{
    public class UserQueryRequestValidator : AbstractValidator<UserQueryRequest>
    {
        public UserQueryRequestValidator()
        {
            // TODO: validate Page - greater than 0
            // TODO: validate PageSize - greater than 0, max 100
            // TODO: validate SortBy - must be null or one of: name, email, status
            // TODO: validate SortDirection - must be null or one of: asc, desc
        }
    }
}
