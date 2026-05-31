using System;
using System.Collections.Generic;
using System.Text;

namespace MOS.Application.DTOs.Responses.Tenants
{
    public class TenantResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserCount { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
