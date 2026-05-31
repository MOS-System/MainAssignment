using MOS.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MOS.Application.Exceptions
{
    // duplicate email/username
    // Thrown when a duplicate is detected → 409
    // e.g. registering with an already existing email
    public class ConflictException : AppException
    {
        public ConflictException(string entity, string field)
            : base($"{entity} with this {field} already exists.") { }
    }
}
