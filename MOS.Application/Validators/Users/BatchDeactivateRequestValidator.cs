using MOS.Application.DTOs.Requests.Users;
using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;


namespace MOS.Application.Validators.Users
{
    public class BatchDeactivateRequestValidator : AbstractValidator<BatchDeactivateRequest>
    {
        public BatchDeactivateRequestValidator()
        {
            // TODO: validate UserIds - not empty
        }
    }
}
