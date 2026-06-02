using MOS.Api.Middleware;

namespace MOS.Api.Extentions
{
    public static class PerformanceLoggingExtention
    {
        public static IApplicationBuilder UserPerformanceLogging (this IApplicationBuilder app)
        {
            return app.UseMiddleware<PerformanceLoggingMiddleware>();
        }
    }
}
