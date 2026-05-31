using System;
using System.Collections.Generic;
using System.Text;

namespace MOS.Domain.Enums
{
    // SignIn, SignOut, UserAdded, UserUpdated, etc
    public enum AuditAction
    {
        // Auth actions
        SignIn = 1,
        SignOut = 2,

        // User management actions
        UserAdded = 3,
        UserUpdated = 4,
        UserDeleted = 5,
        UserDeactivated = 6
    }
}
