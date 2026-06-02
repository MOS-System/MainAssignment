using MOS.Domain.Entities;

namespace MOS.Application.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(Tenant tenant, string role);
        string? GetClaim(string claimName);
    }
}
