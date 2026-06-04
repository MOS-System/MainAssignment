
using Microsoft.EntityFrameworkCore;
using MOS.Application.DTOs.Requests.Auth;
using MOS.Application.Services.Implements;
using MOS.Domain.Entities;
using MOS.Infrastructure.Db;
using MOS.Infrastructure.Interfaces;

namespace MOS.Infrastructure.Implements
{
    public class TenantRepository : ITenantRepository
    {
        private readonly AppDbContext _context;

        public TenantRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddTenantAsync (Tenant tenant)
        {
            await _context.AddAsync(tenant);
            await _context.SaveChangesAsync();
        }

        public async Task<Tenant?> GetTenantByIdAsync(Guid id)
        {
            return await _context.Tenants.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<List<Tenant>> GetAllTenantAsync()
        {
            return await _context.Tenants.ToListAsync();
        }
    }
}
