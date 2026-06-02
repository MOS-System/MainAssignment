
using MOS.Application.DTOs.Requests.Auth;
using MOS.Domain.Entities;


namespace MOS.Infrastructure.Interfaces
{
    public interface ITenantRepository
    {
        Task<Tenant?> GetTenantByNameAndPasswordAsync(LoginRequest request);
        // TODO: GetByIdAsync
        // TODO: GetByNameAsync
        // TODO: AddAsync
    }
}
