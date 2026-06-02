using MOS.Api.Middleware;

namespace MOS.Api.Extentions
{
    public static class DebugContextExtention
    {
        public static IApplicationBuilder UseDebugContext(this IApplicationBuilder app)
        {
            return app.UseMiddleware<DebugContextMiddleware>();
        }
    }
}
