
using Microsoft.EntityFrameworkCore;
using MOS.Application.DTOs.Requests.Auth;
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

        public async Task<Tenant?> GetTenantByNameAndPasswordAsync(LoginRequest request)
        {
       
            return await _context.Tenants.FirstOrDefaultAsync(t =>
                    t.Name == request.Email &&
                    t.Slug == request.Password);
        }

        // TODO: GetByIdAsync
        // TODO: GetByNameAsync
        // TODO: AddAsync
    }
}
