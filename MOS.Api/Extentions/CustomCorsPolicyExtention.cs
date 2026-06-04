using Microsoft.AspNetCore.Cors.Infrastructure;

namespace MOS.Api.Extentions
{
    public static class CustomCorsPolicyExtention
    {
        public static IServiceCollection AddCustomCorsPolicy(this IServiceCollection services)
        {
           return services.AddCors(delegate (CorsOptions options)
            {
                options.AddDefaultPolicy(delegate (CorsPolicyBuilder builder)
                {
                    builder.AllowAnyMethod().SetPreflightMaxAge(TimeSpan.FromDays(1)).SetIsOriginAllowed(
                        (string origin) =>
                        {
                            if (string.IsNullOrEmpty(origin))
                            {
                                return false;
                            }

                            if (origin.StartsWith("http://localhost:3000", StringComparison.OrdinalIgnoreCase) ||
                               origin.StartsWith("https://localhost:5173", StringComparison.OrdinalIgnoreCase))
                            {
                                return true;
                            }

                            return (origin.EndsWith("", StringComparison.OrdinalIgnoreCase) ||
                                    Uri.TryCreate(origin, UriKind.Absolute, out var uri) &&
                                    uri.Scheme.Equals("https", StringComparison.OrdinalIgnoreCase));
                        })
                    .AllowAnyHeader()
                    .AllowCredentials();

                });
            });
        }
    }
}
