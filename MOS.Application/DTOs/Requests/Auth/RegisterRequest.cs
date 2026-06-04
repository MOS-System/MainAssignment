using MOS.Domain.Enums;
using System;


namespace MOS.Application.DTOs.Requests.Auth
{
    public class RegisterRequest
    {
        public string UserName { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public Guid TenantId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Phone { get; set;  } = string.Empty;
        public SigninMethod SigninMethod { get; set; } = SigninMethod.local;
    }
}
