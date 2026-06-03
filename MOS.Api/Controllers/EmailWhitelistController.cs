using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MOS.Application.DTOs.Requests.EmailWhitelist;
using MOS.Application.Services.Interfaces;
using MOS.Domain.Constants;

namespace MOS.Api.Controllers
{
    [ApiController]
    [Route("api/email-whitelist")]
    [Authorize(Policy = Permissions.AdminPolicy)]
    public class EmailWhitelistController : ControllerBase
    {
        private readonly IEmailWhitelistService _emailWhitelistService;

        public EmailWhitelistController(
            IEmailWhitelistService emailWhitelistService)
        {
            _emailWhitelistService = emailWhitelistService;
        }

        [HttpGet]
        public async Task<IActionResult> GetWhitelist()
        {
            var result = await _emailWhitelistService.GetWhitelistAsync();
            return Ok(result);
        }

        [HttpPut("setting")]
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveEmail(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Id must be greater than 0.");
            }
            await _emailWhitelistService.RemoveEmailAsync(id);
            return NoContent();
        }
    }
}