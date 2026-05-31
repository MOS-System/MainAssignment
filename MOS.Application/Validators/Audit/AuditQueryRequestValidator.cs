using MOS.Application.DTOs.Requests.Audit;
using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;


namespace MOS.Application.Validators.Audit
{
    public class AuditQueryRequestValidator : AbstractValidator<AuditQueryRequest>
    {
        public AuditQueryRequestValidator()
        {
            // TODO: validate Page - greater than 0
            // TODO: validate PageSize - greater than 0, max 100
        }
    }
}
