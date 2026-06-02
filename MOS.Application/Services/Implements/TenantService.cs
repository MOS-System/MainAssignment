using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MOS.Application.DTOs.Requests.Tenant;
using MOS.Application.DTOs.Requests.Tenants;
using MOS.Application.DTOs.Responses.Tenants;
using MOS.Application.Exceptions;
using MOS.Application.Services.Interfaces;
using MOS.Domain.Entities;
using MOS.Infrastructure.Interfaces;

namespace MOS.Application.Services.Implements
{
    public class TenantService : BaseService<TenantService>, ITenantService
    {
        private readonly ITenantRepository _tenantRepository;

        public TenantService(
            ITenantRepository tenantRepository,
            ILogger<TenantService> logger,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration)
            : base(logger, mapper, httpContextAccessor, configuration)
        {
            _tenantRepository = tenantRepository;
        }

        public async Task<TenantResponse> CreateTenantAsync(CreateTenantRequest request)
        {
            var tenant = new Tenant(request.Name, request.Slug);

            await _tenantRepository.AddTenantAsync(tenant);

            return _mapper.Map<TenantResponse>(tenant);
        }

        public async Task<TenantResponse> GetTenantByIdAsync(int id)
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