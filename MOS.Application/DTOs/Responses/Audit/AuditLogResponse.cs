using MOS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MOS.Application.DTOs.Responses.Audit
{
    // action, user, timestamp, object
    public class AuditLogResponse
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int UserId { get; set; }
        public AuditAction Action { get; set; }
        public string ObjectAffected { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
