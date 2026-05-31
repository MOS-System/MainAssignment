
namespace MOS.Application.Exceptions
{
    // Thrown when business rule validation fails → 400
    public class ValidationException : AppException
    {
        public ValidationException(string message)
            : base(message) { }
    }
}
