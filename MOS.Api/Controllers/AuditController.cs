using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MOS.Api.EndPoints;
using MOS.Application.DTOs.Requests.Audit;
using MOS.Application.Services.Implements;
using MOS.Application.Services.Interfaces;
using MOS.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace MOS.Api.Controllers
{
    [ApiController]
    [Authorize(Policy = Permissions.AdminPolicy)]
    public class AuditController : BaseController<AuditController>
    {
        private readonly IAuditService _auditService;

        public AuditController(IConfiguration configuration, ILogger<AuditController> logger, IAuditService auditService) : base(configuration, logger)
        {
            _auditService = auditService;
        }

        // GET api/audit?search=john&page=1&pageSize=10
        [HttpGet(Endpoints.AuditEnpoints.GetAuditLogs)]
        public async Task<IActionResult> GetPagedAuditLogs([FromQuery] AuditQueryRequest query)
        {
            var result = await _auditService.GetPagedAsync(query);
            return Ok(result);
        }
    }
}
