using System;
using System.Collections.Generic;
using System.Text;

namespace MOS.Application.DTOs.Requests.Users
{
    public class BatchDeleteRequest
    {
        public List<Guid> UserIds { get; set; } = new();
    }
}
