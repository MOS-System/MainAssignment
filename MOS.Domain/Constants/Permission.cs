using System;
using System.Collections.Generic;
using System.Text;

namespace MOS.Domain.Constants
{
    // Product permission keys
    public static class Permissions
    {
        // Used in JWT claims and authorization checks
        public const string AdminPolicy = "AdminOnly";
        public const string TenantUserPolicy = "TenantUser";
    }
}
