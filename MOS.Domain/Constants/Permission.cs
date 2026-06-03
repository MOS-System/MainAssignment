using System;
using System.Collections.Generic;
using System.Text;

namespace MOS.Domain.Constants
{
    // Product permission keys
    public static class Permissions
    {
        // Used in JWT claims and authorization checks
        public const string AdminPolicy = "Administrator";
        public const string TenantAdministratorPolicy = "TenantAdministrator";
        public const string TenantUserPolicy = "TenantUser";
        public const string WhitelistPolicy = "WhitelistAccess";
    }
}
