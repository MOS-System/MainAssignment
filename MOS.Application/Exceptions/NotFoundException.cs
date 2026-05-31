using MOS.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MOS.Application.Exceptions
{
    // Thrown when a requested resource doesn't exist → 404
    public class NotFoundException : AppException
    {
        public NotFoundException(string entity, object key)
            : base($"{entity} with key '{key}' was not found.") { }
    }
}
