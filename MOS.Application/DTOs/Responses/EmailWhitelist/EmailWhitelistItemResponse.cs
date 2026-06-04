using System;
using System.Collections.Generic;
using System.Text;

namespace MOS.Application.DTOs.Responses.EmailWhitelist
{
    public class EmailWhitelistItemResponse
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public DateTime AddedAt { get; set; }
    }
}
