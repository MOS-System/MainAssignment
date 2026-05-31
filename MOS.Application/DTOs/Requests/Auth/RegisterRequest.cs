using System;
using System.Collections.Generic;
using System.Text;

namespace MOS.Application.DTOs.Requests.Auth
{
    public class RegisterRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string TenantName { get; set; }  // creates a new tenant
    }
}
