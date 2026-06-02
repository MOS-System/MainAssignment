using MOS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MOS.Application.DTOs.Requests.Users
{
    public class CreateUserRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public RoleType Role { get; set; } 
        public int TenantId { get; set; }          // ← add this
        public List<int> ProductIds { get; set; } = new();
    }
}

