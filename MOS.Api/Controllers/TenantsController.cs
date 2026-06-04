using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MOS.Api.EndPoints;
using MOS.Application.DTOs.Requests.Tenants;
using MOS.Application.Services.Interfaces;

namespace MOS.Api.Controllers
{
    [ApiController]
    public class TenantsController : BaseController<TenantsController>
    {
        private readonly ITenantService _tenantService;

        public TenantsController(IConfiguration configuration, ILogger<TenantsController> logger, ITenantService tenantService) : base(configuration, logger)
        {
            _tenantService = tenantService;
        }

        [HttpGet(Endpoints.TenantEnpoints.GetAllTenantNames)]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllTenantNames()
        {
            var result = await _tenantService.GetAllTenantNamesAsync();
            return Ok(result);
        }

        [HttpGet(Endpoints.TenantEnpoints.GetTenantById)]
        [AllowAnonymous]
        public async Task<IActionResult> GetTenantById(Guid id)
        {
            var result = await _tenantService.GetTenantByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CreateTenant([FromBody] CreateTenantRequest request)
        {
            var result = await _tenantService.CreateTenantAsync(request);
            return Ok(result);
        }
    }
}