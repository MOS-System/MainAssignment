using System;
using System.Collections.Generic;
using System.Text;

namespace MOS.Application.DTOs.Requests.EmailWhitelist
{
    // add new email to whitelist
    public class AddEmailToWhitelistRequest
    {
        public string Email { get; set; }
    }
}
