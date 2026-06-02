using System;
using System.Collections.Generic;
using System.Text;

namespace MOS.Application.DTOs.Responses.Tenants
{
    public class TenantResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
