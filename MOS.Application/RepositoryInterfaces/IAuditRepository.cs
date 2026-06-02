using MOS.Application.Common;
using MOS.Application.DTOs.Requests.Audit;
using MOS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MOS.Infrastructure.Interfaces
{
    public interface IAuditRepository
    {
        // TODO: GetPagedAsync - takes AuditQueryRequest, returns PagedResult<AuditLog>
        // TODO: AddAsync
        Task AddAsync(AuditLog log);
        Task<PagedResult<AuditLog>> GetPagedAsync(AuditQueryRequest query);
    }
}
