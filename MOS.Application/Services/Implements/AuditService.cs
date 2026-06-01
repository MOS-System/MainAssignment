using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MOS.Application.Services.Interfaces;
using MOS.Infrastructure.Interfaces;



namespace MOS.Application.Services.Implements
{
    // log and query audit records
    public class AuditService : BaseService<AuditService>, IAuditService
    {
        private readonly IAuditRepository _auditRepository;

        public AuditService(
            IAuditRepository auditRepository, 
            ILogger<AuditService> logger, 
            IMapper mapper, IHttpContextAccessor httpContextAccessor, 
            IConfiguration configuration) : base(logger, mapper, httpContextAccessor, configuration)
        {
            _auditRepository = auditRepository;
        }


        // TODO: GetPagedAsync - takes AuditQueryRequest, returns PagedAuditResponse
        // search by object, name, userId

        // TODO: LogAsync - takes userId, userName, action, objectAffected
        // create and save AuditLog entry
    }
}
