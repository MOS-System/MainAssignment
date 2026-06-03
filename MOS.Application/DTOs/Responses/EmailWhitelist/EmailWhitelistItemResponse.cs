using System;
using System.Collections.Generic;
using System.Text;

namespace MOS.Application.DTOs.Responses.EmailWhitelist
{
    public class EmailWhitelistItemResponse
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public DateTime AddedAt { get; set; }
    }
}
