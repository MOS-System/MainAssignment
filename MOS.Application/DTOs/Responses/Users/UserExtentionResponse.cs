using System;
using System.Collections.Generic;
using System.Text;

namespace MOS.Application.DTOs.Responses.Users
{
    public class UserExtentionResponse : UserResponse
    {
        public List<string> ProductNames { get; set; } = new();
        public string? TemporaryPassword { get; set; }
    }
}
