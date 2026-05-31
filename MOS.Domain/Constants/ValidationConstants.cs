using System;
using System.Collections.Generic;
using System.Text;

namespace MOS.Domain.Constants
{
    public static class ValidationConstants
    {
        // User field limits — used in both entity and validators
        public const int NameMaxLength = 100;
        public const int EmailMaxLength = 150;
        public const int PasswordMinLength = 8;
        public const int PasswordMaxLength = 256;
    }
}
