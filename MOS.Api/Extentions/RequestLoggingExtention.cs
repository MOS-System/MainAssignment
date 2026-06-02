using MOS.Api.Middleware;

namespace MOS.Api.Extentions
{
    public static class RequestLoggingExtention
    {
        public static IApplicationBuilder UseRequestLogging(this IApplicationBuilder app)
        {
            return app.UseMiddleware<RequestLoggingMiddleware>();
        }
    }
}
