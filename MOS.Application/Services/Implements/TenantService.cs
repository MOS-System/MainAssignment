using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MOS.Application.DTOs.Requests.Tenants;
using MOS.Application.DTOs.Responses.Tenants;
using MOS.Application.Exceptions;
using MOS.Application.Services.Interfaces;
using MOS.Domain.Entities;
using MOS.Domain.Enums;
using MOS.Infrastructure.Interfaces;

namespace MOS.Application.Services.Implements
{
    public class TenantService : BaseService<TenantService>, ITenantService
    {
        private readonly ITenantRepository _tenantRepository;
        private readonly IAuditRepository _auditRepository;
        private readonly IUserRepository _userRepository;

        public TenantService(
            ITenantRepository tenantRepository,
            IAuditRepository auditRepository,
            IUserRepository userRepository,
            ILogger<TenantService> logger,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration)
            : base(logger, mapper, httpContextAccessor, configuration)
        {
            _tenantRepository = tenantRepository;
            _auditRepository = auditRepository;
        }

        public async Task<TenantResponse> CreateTenantAsync(CreateTenantRequest request)
        {
            var currentUser = await _userRepository.GetUserByIdAsync(GetUserIdFromJWT());
            if (currentUser == null) throw new NotFoundException("User", request);
            
            var tenant = new Tenant(request.Name, request.Slug);

            await _tenantRepository.AddTenantAsync(tenant);

            await _auditRepository.AddAsync(new AuditLog(
                currentUser.Id,
                currentUser.Name,
                currentUser.UserName,
                CategoryLogType.Account.ToString(),
                currentUser.Email,
                AuditAction.TenantAdded,
                $"Tenant {tenant.Name} created"
            ));

            return _mapper.Map<TenantResponse>(tenant);
        }

        public async Task<TenantResponse> GetTenantByIdAsync(Guid id)
        {
            var tenant = await _tenantRepository.GetTenantByIdAsync(id)
                ?? throw new NotFoundException("Tenant", id);

            return _mapper.Map<TenantResponse>(tenant);
        }

        public async Task<List<TenantNameResponse>> GetAllTenantNamesAsync()
        {
            var tenants = await _tenantRepository.GetAllTenantAsync();

            return _mapper.Map<List<TenantNameResponse>>(tenants);
        }
    }
}