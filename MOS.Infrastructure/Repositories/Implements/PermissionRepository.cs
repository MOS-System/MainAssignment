using MOS.Application.Services.Interfaces;
using MOS.Domain.Entities;
using MOS.Infrastructure.Db;

namespace MOS.Infrastructure.Repositories.Implements
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