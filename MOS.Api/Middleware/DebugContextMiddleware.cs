using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MOS.Api.Middleware
{
    public class DebugContextMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IConfiguration _configuration;

        public DebugContextMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            this.next = next;
            _configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var id = "28fc82c2-66e5-4c8b-9a1c-2f0e5d6b8a1f";
            var name = "tester";
            var slug = "maven_point";
            var isActive = true;
            var createdAt = new DateTime(2026, 6, 1, 13, 0, 0);

            var tokenHandler = new JwtSecurityTokenHandler();
            var secretKey = _configuration["Jwt:SecretKey"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!));

            var tokenDecriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new[]
                {
                    new System.Security.Claims.Claim(ClaimType.Id, id),
                    new System.Security.Claims.Claim(ClaimType.Name, name),
                    new System.Security.Claims.Claim(ClaimType.Slug, slug),
                    new System.Security.Claims.Claim(ClaimType.IsActive, isActive.ToString()),
                    new System.Security.Claims.Claim(ClaimType.CreatedAt, createdAt.ToString("o")),
                    new System.Security.Claims.Claim(ClaimType.Role, "Administrator")
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDecriptor);
            var requestHeaders = context.Request.Headers;

            if (!requestHeaders.TryGetValue(HeaderNames.Authorization, out var value))
            {
                requestHeaders.Append(HeaderNames.Authorization, "Bearer" + token);
            }

            await next(context);

        }

        public class ClaimType
        {
            public static readonly String Realm = "realm";
            public static readonly String Scope = "scope";
            public static readonly String Issuer = "iss";
            public static readonly String Audience = "aud";
            public static readonly String Id = "id";
            public static readonly String Name = ClaimTypes.Upn;
            public static readonly String Slug = "slug";
            public static readonly String IsActive = "isActive";
            public static readonly String CreatedAt = "createdAt";
            public static readonly String Role = ClaimTypes.Role;

        }

    }
}
