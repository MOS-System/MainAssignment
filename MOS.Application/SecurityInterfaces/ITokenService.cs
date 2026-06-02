using MOS.Application.DTOs.Responses.Auth;
using MOS.Domain.Entities;

namespace MOS.Application.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(AuthResponse user);
        string? GetClaim(string claimName);
    }
}
