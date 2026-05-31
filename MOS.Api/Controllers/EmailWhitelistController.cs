using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MOS.Api.Controllers;
using MOS.Application.DTOs.Requests.EmailWhitelist;
using MOS.Application.Services.Interfaces;
using MOS.Domain.Constants;


[Authorize(Policy = Permissions.AdminPolicy)]
public class EmailWhitelistController : BaseController<EmailWhitelistController>
{
    private readonly IEmailWhiteListService _whitelistService;

    public EmailWhitelistController(IEmailWhiteListService emailWhiteListService, ILogger<EmailWhitelistController> logger) : base(logger)
    {
        _whitelistService = emailWhiteListService;
    }



    // GET api/emailwhitelist
    [HttpGet]
    public async Task<IActionResult> GetSetting()
    {
        // TODO: call _whitelistService.GetSettingAsync
        // TODO: return 200 with WhitelistSettingResponse
        throw new NotImplementedException();
    }

    // PUT api/emailwhitelist/setting
    [HttpPut("setting")]
    public async Task<IActionResult> UpdateSetting(
        [FromBody] UpdateWhitelistSettingRequest request)
    {
        // TODO: call _whitelistService.UpdateSettingAsync
        // TODO: return 204
        throw new NotImplementedException();
    }

    // POST api/emailwhitelist/emails
    [HttpPost("emails")]
    public async Task<IActionResult> AddEmail(
        [FromBody] AddEmailToWhitelistRequest request)
    {
        // TODO: call _whitelistService.AddEmailAsync
        // TODO: return 201
        throw new NotImplementedException();
    }

    // DELETE api/emailwhitelist/emails
    [HttpDelete("emails")]
    public async Task<IActionResult> RemoveEmail(
        [FromBody] DeleteEmailFromWhitelistRequest request)
    {
        // TODO: call _whitelistService.RemoveEmailAsync
        // TODO: return 204
        throw new NotImplementedException();
    }
}