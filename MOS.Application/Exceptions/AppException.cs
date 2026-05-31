using System;
using System.Collections.Generic;
using System.Text;

namespace MOS.Application.Exceptions
{
    public abstract class AppException : Exception
    {
        protected AppException(string message) : base(message) { }
    }
}
