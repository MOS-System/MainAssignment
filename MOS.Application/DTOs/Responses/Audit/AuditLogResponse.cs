using MOS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MOS.Application.DTOs.Responses.Audit
{
    // action, user, timestamp, object
    public class AuditLogResponse
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public AuditAction Action { get; set; }
        public string ObjectAffected { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
    }
}
