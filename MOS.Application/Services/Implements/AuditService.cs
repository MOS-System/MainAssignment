using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MOS.Application.DTOs.Requests.Audit;
using MOS.Application.DTOs.Responses.Audit;
using MOS.Application.Services.Interfaces;
using MOS.Domain.Entities;
using MOS.Domain.Enums;
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

        public Task<List<AuditLogResponse>> GetAuditLogAsync()
        {
            throw new NotImplementedException();
        }

        public async Task LogLogoutAsync()
        {
            await _auditRepository.AddAsync(new AuditLog(
                GetUserIdFromJWT(),
                GetUserNameFromJWT(),      // Name
                GetUserNameFromJWT(),      // UserName
                "Acccount",                // Category
                GetUserEmailFromJWT(),     // Email
                AuditAction.SignOut,
                $"User {GetUserEmailFromJWT()} logged out"
            ));
        }
    }
}
