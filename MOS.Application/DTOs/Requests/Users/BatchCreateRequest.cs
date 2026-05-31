using System;
using System.Collections.Generic;
using System.Text;

namespace MOS.Application.DTOs.Requests.Users
{
    // Supports batch addition of users
    public class BatchCreateUserRequest
    {
        public List<CreateUserRequest> Users { get; set; } = new();
    }
}
