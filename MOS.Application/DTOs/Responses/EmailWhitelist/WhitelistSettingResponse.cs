using System;
using System.Collections.Generic;
using System.Text;

namespace MOS.Application.DTOs.Responses.EmailWhitelist
{
    // isEnabled + list of emails
    public class WhitelistSettingResponse
    {
        public bool IsEnabled { get; set; }
        public List<string> Emails { get; set; } = new();
    }
}
