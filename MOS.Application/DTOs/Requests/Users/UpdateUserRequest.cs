using MOS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MOS.Application.DTOs.Requests.Users
{
    public class UpdateUserRequest
    {
        public string Name { get; set; }
        public RoleType Role { get; set; }
        public List<int> ProductIds { get; set; } = new();
    }

}
