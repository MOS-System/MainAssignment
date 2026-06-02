using System;
using System.Collections.Generic;
using System.Text;

namespace MOS.Domain.Enums
{
    // Administrator = full access to all products
    // TenantAdministrator = mangement permission product for tenant user
    // TenantUser = access to specific products only
    public enum RoleType
    {
        Administrator = 1,
        TenantAdministrator = 2,
        TenantUser = 3
    }
}
