using MOS.Application.DTOs.Requests.Audit;
using MOS.Application.Common;
using MOS.Domain.Entities;
using MOS.Infrastructure.Db;
using MOS.Application.Services.Interfaces;

namespace MOS.Infrastructure.Repositories.Implements
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
