
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MOS.Application.DTOs.Responses.Auth;
using MOS.Application.Services.Interfaces;
using MOS.Infrastructure.ExternalServices.SecurityImplements;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MOS.Infrastructure.ExternalServices.Security
{
    public class TokenService : ITokenService
    {
        private readonly TokenSetting _tokenSetting;

        public TokenService(IOptions<TokenSetting> options)
        {
            _tokenSetting = options.Value;
        }

        public string GenerateToken(AuthResponse user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenSetting.SecretKey));
            var securityKey = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim("userId", user.UserId),
                new Claim("email", user.Email),
                new Claim("status", user.Status.ToString()),
                new Claim("phone", user.Phone),
                new Claim("scope", "mos_api"),       // required by ApiScope policy
                new Claim(ClaimTypes.Role, user.Role.ToString())     // required by role policies
            };

            var token = new JwtSecurityToken(
                issuer: _tokenSetting.Issuer,
                audience: _tokenSetting.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: securityKey);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string? GetClaim(string claimName)
        {
            return Thread.CurrentPrincipal is ClaimsPrincipal principal
                ? principal.FindFirst(claimName)?.Value
                : null;
        }
    }
}