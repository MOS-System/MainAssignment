using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MOS.Application.Services.Interfaces;


namespace MOS.Application.Services.Implements
{
    // tenant/account management
    public class TenantService : BaseService<TenantService>, ITenantService
    {
        private readonly ITenantRepository _tenantRepository;

        public TenantService(ITenantRepository tenantRepository,
            ILogger<TenantService> logger, 
            IMapper mapper, 
            IHttpContextAccessor httpContextAccessor, 
            IConfiguration configuration) : base(logger, mapper, httpContextAccessor, configuration)
        {
            _tenantRepository = tenantRepository;
        }



        // TODO: GetByIdAsync - takes id, returns TenantResponse
        // throw NotFoundException if not found

        // TODO: CreateAsync - takes CreateTenantRequest, returns TenantResponse
        // check name not duplicate, create tenant
    }
}
