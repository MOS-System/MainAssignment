using System;
using System.Collections.Generic;
using System.Text;

namespace MOS.Application.DTOs.Requests.EmailWhitelist
{
    // disable/enable the function
    public class UpdateEmailWhitelistSettingRequest
    {
        public bool IsEnabled { get; set; }
    }
}
