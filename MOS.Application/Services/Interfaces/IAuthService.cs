


using MOS.Application.DTOs.Requests.Auth;
using MOS.Domain.Entities;

namespace MOS.Application.Services.Interfaces
{
    public interface IAuthService
    {
        Task<Tenant> GetTenantByLoginRequest(LoginRequest loginRequest);
    }
}
