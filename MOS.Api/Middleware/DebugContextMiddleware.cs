using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using MOS.Domain.Enums;
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
            var id = "0";
            var userId = "mv-ts-dev-3";
            var name = "tester";
            var email = "tester@mavenpoint.com";
            var phone = "0123456789";
            var role = RoleType.Administrator;

            var tokenHandler = new JwtSecurityTokenHandler();
            var secretKey = _configuration["Jwt:SecretKey"];
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                      new Claim(ClaimType.Id, id),
                      new Claim(ClaimType.UserId, userId),
                      new Claim(ClaimType.Name, name),
                      new Claim(ClaimType.Email, email),
                      new Claim(ClaimType.Phone, phone),
                      new Claim(ClaimType.Role, role.ToString())

                }),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };


            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);
            var requestHeaders = context.Request.Headers;

            if (!requestHeaders.TryGetValue(HeaderNames.Authorization, out var value))
            {
                requestHeaders.Append(HeaderNames.Authorization, "Bearer " + jwtToken);
            }

            await next(context);

        }

        public class ClaimType
        {
            public static readonly String Scope = "scope";
            public static readonly String Issuer = "iss";
            public static readonly String Audience = "aud";
            public static readonly String Id = "id";
            public static readonly String UserId = "userId";
            public static readonly String Name = ClaimTypes.Upn;
            public static readonly String Email = "email";
            public static readonly String Phone = "phone";
            public static readonly String Role = ClaimTypes.Role;
        }

    }
}
