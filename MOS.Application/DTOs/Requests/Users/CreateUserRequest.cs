using MOS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MOS.Application.DTOs.Requests.Users
{
    public class CreateUserRequest
    {
        public string Name { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string RandomPassword { get; set;  } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        
        public RoleType Role { get; set; }
        public Guid TenantId { get; set; }
        public List<Guid> ProductIds { get; set; } = new();
    }
}

