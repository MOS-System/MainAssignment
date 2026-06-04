using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MOS.Api.EndPoints;
using MOS.Application.DTOs.Requests.EmailWhitelist;
using MOS.Application.Services.Interfaces;
using MOS.Domain.Constants;

namespace MOS.Api.Controllers
{
    [ApiController]
    [Route("api/email-whitelist")]
    [Authorize(Policy = Permissions.AdminPolicy)]
    public class EmailWhitelistController : BaseController<EmailWhitelistController>
    {
        private readonly IEmailWhitelistService _emailWhitelistService;

        public EmailWhitelistController(IConfiguration configuration, ILogger<EmailWhitelistController> logger, IEmailWhitelistService emailWhitelistService) : base(configuration, logger)
        {
            _emailWhitelistService = emailWhitelistService;
        }

        [HttpGet]
        public async Task<IActionResult> GetWhitelist()
        {
            var result = await _emailWhitelistService.GetWhitelistAsync();
            return Ok(result);
        }

        [HttpPut(Endpoints.EmailWhiteListEnpoints.Setting)]
        public async Task<IActionResult> UpdateSetting(
            [FromBody] UpdateEmailWhitelistSettingRequest request)
        {
            await _emailWhitelistService.UpdateSettingAsync(request);
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> AddEmail(
            [FromBody] AddEmailWhitelistRequest request)
        {
            await _emailWhitelistService.AddEmailAsync(request);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> RemoveEmail(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid Id Format");
            }
            await _emailWhitelistService.RemoveEmailAsync(id);
            return NoContent();
        }
    }
}