using Microsoft.AspNetCore.Http;

namespace MOS.Api.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // TODO: wrap _next(context) in try/catch
            // TODO: catch AppException → map to correct HTTP status code
            // TODO: catch unhandled Exception → return 500
            // TODO: return consistent error response shape
        }
    }
}