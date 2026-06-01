using MOS.Domain.Entities;
using MOS.Infrastructure.Db;
using MOS.Infrastructure.Interfaces;

namespace MOS.Infrastructure.Implements
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly AppDbContext _context;

        public PermissionRepository(AppDbContext context)
        {
            _context = context;
        }

        // TODO: GetByUserIdAsync
        // TODO: AddAsync
        // TODO: RemoveByUserIdAsync
        // TODO: ExistsAsync
    }
}