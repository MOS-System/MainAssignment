using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MOS.Application.DTOs.Requests.Tenant;
using MOS.Application.Services;
using MOS.Domain.Constants;

[ApiController]
[Route("api/[controller]")]
[Authorize(Policy = Permissions.AdminPolicy)]
public class TenantController : ControllerBase
{
    private readonly TenantService _tenantService;

    public TenantController(TenantService tenantService)
    {
        _tenantService = tenantService;
    }

    // GET api/tenant/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        // TODO: call _tenantService.GetByIdAsync
        // TODO: return 200 with TenantResponse
        throw new NotImplementedException();
    }

    // POST api/tenant
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTenantRequest request)
    {
        // TODO: call _tenantService.CreateAsync
        // TODO: return 201 with TenantResponse
        throw new NotImplementedException();
    }
}