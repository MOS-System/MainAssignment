using MOS.Api.Middleware;

namespace MOS.Api.Extentions
{
    public static class ExceptionHandlingExtention
    {
        public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}
