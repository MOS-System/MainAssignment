using MOS.Application.DTOs.Requests.Tenants;
using MOS.Application.DTOs.Responses.Tenants;

namespace MOS.Application.Services.Interfaces
{
    public interface ITenantService
    {
        Task<TenantResponse> CreateTenantAsync(CreateTenantRequest request);
        Task<TenantResponse> GetTenantByIdAsync(Guid id);
        Task<List<TenantNameResponse>> GetAllTenantNamesAsync();
    }
}