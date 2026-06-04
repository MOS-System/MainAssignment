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
        SignUp = 3,

        // User management actions
        UserAdded = 4,
        UserUpdated = 5,
        UserDeleted = 6,
        UserDeactivated = 7,
        UserReactivated = 8,
        TenantAdded = 9,


        // Whitelist action
        WhitelistSettingChanged = 10,
        AddedWhitelistEmail = 11,
        RemovedWhitelistEmail = 12,
        
    }
}
