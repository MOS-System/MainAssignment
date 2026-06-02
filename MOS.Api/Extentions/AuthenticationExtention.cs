using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace MOS.Api.Extentions
{
    public static class AuthenticationExtention
    {
        public static IServiceCollection AddJwt(
            this IServiceCollection services, 
            X509Certificate2? customIssuerCertificate,
            IConfiguration configuration)
        {
            var authBuilder = services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            });

            var secretKey = configuration["Jwt:SecretKey"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!));

            authBuilder.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
            {
                opt.ForwardDefaultSelector = context => "Swagger";
            })
            .AddScheme<JwtBearerOptions, JwtBearerHandler>("Swagger", opt =>
            {
                opt.SaveToken = false;
                opt.RequireHttpsMetadata = false;
                opt.IncludeErrorDetails = true;
                // opt.MapInboundClaims = false;
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ClockSkew = new TimeSpan(0, 5, 0),

                    IssuerSigningKeys = new List<SecurityKey>
                    {
                        securityKey
                    },
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    //RoleClaimType = "role"
                };
                opt.Events = GetFailedEvents("Swagger");
            })
            .AddScheme<JwtBearerOptions, JwtBearerHandler>("None", null);

            return services;
        }

        private static JwtBearerEvents GetFailedEvents(string tokenSource = "Swagger")
        {
            return new JwtBearerEvents
            {
                OnAuthenticationFailed = ctx =>
                {
                    if (ctx.Exception is SecurityTokenExpiredException)
                    {
                        ctx.Response.StatusCode = 401;
                        ctx.Response.WriteAsync("Token expired.").Wait();
                        return Task.CompletedTask;
                    }
                    throw new UnauthorizedAccessException("Authentication token failed, please login", ctx.Exception);
                },
                OnTokenValidated = delegate (TokenValidatedContext context)
                {
                    var identity = context.Principal?.Identity as ClaimsIdentity;
                    if (identity != null)
                    {
                        if (tokenSource == "Swagger")
                        {
                            identity.AddClaim(new Claim("scope", "readwrite.all"));
                        }
                    }
                    return Task.CompletedTask;
                }
            };
        }
    }
}