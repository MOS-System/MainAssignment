using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MOS.Application.DTOs.Requests.Audit;
using MOS.Application.Services;
using MOS.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace MOS.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = Permissions.AdminPolicy)]
    public class AuditController : ControllerBase
    {
        private readonly AuditService _auditService;

        public AuditController(AuditService auditService)
        {
            _auditService = auditService;
        }

        // GET api/audit?search=john&page=1&pageSize=10
        [HttpGet]
        public async Task<IActionResult> GetAuditLogs([FromQuery] AuditQueryRequest request)
        {
            // TODO: call _auditService.GetPagedAsync
            // TODO: return 200 with PagedAuditResponse
            throw new NotImplementedException();
        }
    }
}
