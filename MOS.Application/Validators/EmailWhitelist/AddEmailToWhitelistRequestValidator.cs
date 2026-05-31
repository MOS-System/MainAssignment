using FluentValidation;
using MOS.Application.DTOs.Requests.EmailWhitelist;
using System;
using System.Collections.Generic;
using System.Text;


namespace MOS.Application.Validators.EmailWhitelist
{
    public class AddEmailToWhitelistRequestValidator : AbstractValidator<AddEmailToWhitelistRequest>
    {
        public AddEmailToWhitelistRequestValidator()
        {
            // TODO: validate Email - not empty, valid email format
        }
    }
}
