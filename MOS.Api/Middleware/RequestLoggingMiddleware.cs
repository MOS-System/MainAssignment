using Microsoft.AspNetCore.Http;

namespace MOS.Api.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // TODO: log incoming request - method, path, timestamp
            // TODO: call _next(context)
            // TODO: log outgoing response - status code, duration
        }
    }
}
