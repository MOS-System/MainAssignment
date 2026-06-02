
using MOS.Application.DTOs.Requests.Auth;
using MOS.Domain.Entities;


namespace MOS.Infrastructure.Interfaces
{
    public interface ITenantRepository
    {
      
        Task AddTenantAsync(Tenant tenant);
        Task<Tenant?> GetTenantByIdAsync(int id);
        Task<List<Tenant>> GetAllTenantAsync();
    }
}
