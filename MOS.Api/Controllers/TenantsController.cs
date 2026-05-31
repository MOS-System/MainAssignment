using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MOS.Api.Controllers;
using MOS.Application.DTOs.Requests.Tenant;
using MOS.Application.Services.Implements;
using MOS.Application.Services.Interfaces;
using MOS.Domain.Constants;


[Authorize(Policy = Permissions.AdminPolicy)]
public class TenantController : BaseController<TenantController>
{
    private readonly ITenantService _tenantService;

    public TenantController(TenantService tenantService, ILogger<TenantController> logger) : base(logger)
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