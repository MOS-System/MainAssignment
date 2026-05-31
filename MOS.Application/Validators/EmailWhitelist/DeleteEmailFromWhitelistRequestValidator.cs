using MOS.Application.DTOs.Requests.EmailWhitelist;
using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;


namespace MOS.Application.Validators.EmailWhitelist
{
    public class DeleteEmailFromWhitelistRequestValidator : AbstractValidator<DeleteEmailFromWhitelistRequest>
    {
        public DeleteEmailFromWhitelistRequestValidator()
        {
            // TODO: validate Email - not empty, valid email format
        }
    }
}
