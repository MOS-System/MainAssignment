using System;
using System.Collections.Generic;
using System.Text;

namespace MOS.Application.DTOs.Responses.EmailWhitelist
{
    public class EmailWhitelistResponse
    {
        public bool IsEnabled { get; set; }
        public List<EmailWhitelistItemResponse> Emails { get; set; } = new();
    }
}
