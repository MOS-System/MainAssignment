using System.Diagnostics;

namespace MOS.Api.Middleware
{
    public class PerformanceLoggingMiddleware
    {
        public readonly RequestDelegate _next;
        public readonly ILogger<PerformanceLoggingMiddleware> _logger;

        public PerformanceLoggingMiddleware(RequestDelegate next, ILogger<PerformanceLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();

            var method = context.Request.Method;
            var path = context.Request.Path;
            var queryString = context.Request.QueryString;
            try
            {
                await _next(context);

            }
            finally
            {
                stopwatch.Stop();

                var statusCode = context.Response.StatusCode;
                var elapsedMs = stopwatch.ElapsedMilliseconds;
                _logger.LogInformation("Request {Method} {Path}{QueryString} responsed {StatusCode} executed in {ElapsedMilliseconds} ms", method, path, queryString, statusCode, elapsedMs);
            }
        }
    }
}
