using MOS.Domain.Entities;
using MOS.Infrastructure.Db;
using MOS.Infrastructure.Interfaces;

namespace MOS.Infrastructure.Implements
{
    public class AuditRepository : IAuditRepository
    {
        private readonly AppDbContext _context;

        public AuditRepository(AppDbContext context)
        {
            _context = context;
        }

        // TODO: GetPagedAsync - search by object, name, userId
        // TODO: AddAsync
    }
}
