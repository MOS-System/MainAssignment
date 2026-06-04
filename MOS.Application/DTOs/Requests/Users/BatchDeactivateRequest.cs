using System;
using System.Collections.Generic;
using System.Text;

namespace MOS.Application.DTOs.Requests.Users
{
    public class BatchDeactivateRequest
    {
        public List<Guid> UserIds { get; set; } = new();
    }
}
