using MOS.Domain.Enums;
using MOS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MOS.Application.DTOs.Responses.Users
{
    // wraps PagedResult<UserResponse>
    public class UserResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public UserStatus Status { get; set; }
        public RoleType Role { get; set; }
        public List<string> ProductNames { get; set; } = new();
        public string? TemporaryPassword { get; set; }
    }
}
