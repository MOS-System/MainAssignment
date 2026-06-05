

using MOS.Application.Common;
using MOS.Application.DTOs.Requests.Audit;
using MOS.Application.DTOs.Responses.Audit;
using MOS.Domain.Entities;

namespace MOS.Application.Services.Interfaces
{
    public interface IAuditService
    {
        Task<List<AuditLogResponse>> GetAuditLogAsync();

        Task<PagedResult<AuditLogResponse>> GetPagedAsync(AuditQueryRequest query);

        Task LogLogoutAsync();
    }
}
