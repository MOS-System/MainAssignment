using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MOS.Application.DTOs.Requests.Audit;
using MOS.Application.Services.Interfaces;
using MOS.Domain.Entities;
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

        public async Task<bool> AddAuditLog(AuditAddRequest request)
        {
            var auditlog = _mapper.Map<AuditLog>(request);
            await _auditRepository.AddAsync(auditlog);

            return true;
        }
    }
}
