

namespace MOS.Application.Exceptions
{
    // Thrown when user doesn't have permission → 403
    public class UnauthorizedException : AppException
    {
        public UnauthorizedException()
            : base("You do not have permission to perform this action.") { }
    }
}
