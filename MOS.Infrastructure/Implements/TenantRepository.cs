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

        // TODO: GetByIdAsync
        // TODO: GetByNameAsync
        // TODO: AddAsync
    }
}
