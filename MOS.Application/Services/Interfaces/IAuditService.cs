

using MOS.Application.DTOs.Requests.Audit;

namespace MOS.Application.Services.Interfaces
{
    public interface IAuditService
    {
        Task<bool> AddAuditLog(AuditAddRequest request);
    }
}
