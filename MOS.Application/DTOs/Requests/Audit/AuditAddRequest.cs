
using MOS.Domain.Enums;

namespace MOS.Application.DTOs.Requests.Audit
{
    public class AuditAddRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string ObjectAffected { get; set; } = string.Empty;
        public CategoryLogType Category { get; set; }
        public AuditAction Action { get;  set; } 
        public DateTime Timestamp { get;  set; }
    }
}
