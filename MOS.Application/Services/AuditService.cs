using MOS.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MOS.Application.Services
{
    // log and query audit records
    public class AuditService
    {
        private readonly IAuditRepository _auditRepository;

        public AuditService(IAuditRepository auditRepository)
        {
            _auditRepository = auditRepository;
        }

        // TODO: GetPagedAsync - takes AuditQueryRequest, returns PagedAuditResponse
        // search by object, name, userId

        // TODO: LogAsync - takes userId, userName, action, objectAffected
        // create and save AuditLog entry
    }
}
