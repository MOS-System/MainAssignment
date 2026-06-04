using System;
using System.Collections.Generic;

namespace MOS.Application.DTOs.Requests.Users
{
    public class UpdateUserProductPermissionsRequest
    {
        public List<Guid> ProductIds { get; set; } = new();
    }
}