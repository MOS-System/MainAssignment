using System;
using System.Collections.Generic;
using System.Text;

namespace MOS.Domain.Enums
{
    // Administrator = full access to all products
    // TenantUser = access to specific products only
    public enum RoleType
    {
        Administrator = 1,
        TenantUser = 2
    }
}
