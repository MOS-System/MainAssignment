using MOS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MOS.Application.DTOs.Responses.Auth
{
    // JWT token, user info
    public class AuthResponse
    {
        public string Token { get; set; }       // JWT token
        public string Name { get; set; }
        public string Email { get; set; }
        public RoleType Role { get; set; }
    }
}
