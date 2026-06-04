
namespace MOS.Application.Exceptions
{
    public class ForbiddenException : AppException
    {
        public ForbiddenException(string entity, object key)
            : base($"{entity} with key '{key}' is not allowed in System.") { }
    }

}
