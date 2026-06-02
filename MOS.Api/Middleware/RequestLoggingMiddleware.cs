using Microsoft.AspNetCore.Http;

namespace MOS.Api.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
          var request = context.Request;
            _logger.LogInformation("==== Request Started ====");
            _logger.LogInformation("Method: {Method}", request.Method);
            _logger.LogInformation("Path: {Path}", request.Path);
            _logger.LogInformation("QueryString: {QueryString}", request.QueryString);
            _logger.LogInformation("Host: {Host}", request.Host);
            _logger.LogInformation("ContentType: {ContentType}", request.ContentType);
            _logger.LogInformation("Time: {Time}", DateTime.Now);

            foreach(var header in request.Headers)
            {
                _logger.LogInformation("Header: {Key} = {Value}", header.Key, header.Value);
            }

            await _next(context);

            _logger.LogInformation($"Response Status Code: {context.Response.StatusCode}");
            _logger.LogInformation("==== Request Ended ====\n");

        }
    }
}
