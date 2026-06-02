using System;
using System.Collections.Generic;
using System.Text;

namespace MOS.Application.DTOs.Requests.Tenants
{
    public class CreateTenantRequest
    {
        public string Name { get; set; }
        public string Slug { get; set; }
    }
}
