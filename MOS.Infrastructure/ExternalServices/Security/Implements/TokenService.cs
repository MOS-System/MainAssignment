using MOS.Application.Services.Interfaces;
using MOS.Domain.Entities;

namespace MOS.Infrastructure.ExternalServices.Security.Implements
{
    public class TokenService : ITokenService
    {
        // TODO: inject JWT configuration from appsettings.json

        // TODO: GenerateToken - takes User, returns JWT string
        // include claims: userId, email, role, tenantId

        // TODO: ValidateToken - takes token string, returns claims
    }
}