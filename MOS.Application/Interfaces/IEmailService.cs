using System;
using System.Collections.Generic;
using System.Text;

namespace MOS.Application.Interfaces
{
    // for email verification (bonus/optional for now)
    public interface IEmailService
    {
        // TODO: SendAsync - takes toEmail, subject, body
        // NOTE: must check whitelist before sending
    }
}
