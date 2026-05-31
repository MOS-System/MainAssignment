using System;
using System.Collections.Generic;
using System.Text;

namespace MOS.Application.DTOs.Requests.Users
{
    public class BatchDeleteRequest
    {
        public List<int> UserIds { get; set; } = new();
    }
}
