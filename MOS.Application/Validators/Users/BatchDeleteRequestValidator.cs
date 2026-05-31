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
            // TODO: validate UserIds - not empty
        }
    }
}
