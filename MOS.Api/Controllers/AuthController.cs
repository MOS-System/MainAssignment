using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MOS.Api.Controllers;
using MOS.Api.EndPoints;
using MOS.Application.DTOs.Requests.Audit;
using MOS.Application.DTOs.Requests.Auth;
using MOS.Application.DTOs.Responses.Auth;
using MOS.Application.ExternalServices.AuthInterfaces;
using MOS.Application.ExternalServices.SecurityInterfaces;
using MOS.Application.Services.Interfaces;


[ApiController]
public class AuthController : BaseController<AuthController>
{
    private readonly IAuthService _authService;
    private readonly ITokenService _tokenService;
    private readonly IAuditService _auditService;
    private readonly IMfaService _mfaService;
    private readonly IMicrosoftService _microsoftService;
    public AuthController(
        IAuthService authService, 
        ITokenService tokenService, 
        ILogger<AuthController> logger, 
        IAuditService auditService, 
        IMfaService mfaService, 
        IMicrosoftService microsoftService) : base(logger)
    {
        _authService = authService;
        _tokenService = tokenService;
        _auditService = auditService;
        _mfaService = mfaService;
        _microsoftService = microsoftService;
    }

    // POST api/v1/auth/login
    [HttpPost(Endpoints.AuthEnpoints.Login)]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var authResponse = await _authService.AuthenticateUserWithProducts(request);
        SetToken(authResponse);
        return Ok(authResponse);

    }



    // POST api/v1/auth/register
    [HttpPost(Endpoints.AuthEnpoints.Register)]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var authResponse = await _authService.RegisterUserWithProducts(request);
        SetToken(authResponse);
        return Ok(authResponse);
    }


    // POST api/v1/auth/logout
    [HttpPost(Endpoints.AuthEnpoints.Logout)]
    public async Task<IActionResult> Logout([FromBody] AuditAddRequest request)
    {
        var result = await _auditService.AddAuditLog(request);
        return Ok(result);

    }


    [HttpGet(Endpoints.AuthEnpoints.MicrosoftLogin)]
    [AllowAnonymous]
    public async Task<IActionResult> MicrosoftLogin()
    {
        // 1. Generate random state to prevent CSRF
        var state = Guid.NewGuid().ToString("N");

        // 2. Store state temporarily (in session or cache) to verify on callback
        HttpContext.Session.SetString("oauth_state", state);

        // 3. Build Microsoft authorization URL
        var authUrl = _microsoftService.BuildMicrosoftAuthUrl(state);

        // 4. Redirect browser to Microsoft login page
        return Redirect(authUrl);
    }

    [HttpGet(Endpoints.AuthEnpoints.MicrosoftCallBack)]
    [AllowAnonymous]
    public async Task<IActionResult> MicrosoftCallback([FromQuery] string code, [FromQuery] string state)
    {
        // 1. Validate state to prevent CSRF
        var savedState = HttpContext.Session.GetString("oauth_state");
        if (string.IsNullOrEmpty(savedState) || savedState != state)
            return BadRequest("Invalid state parameter");

        // 2. Clear state from session immediately after use
        HttpContext.Session.Remove("oauth_state");

        // 3. Exchange code for tokens + process user
        var result = await _microsoftService.HandleMicrosoftCallbackAsync(code);
        if (result == null)
            return Unauthorized("Authentication failed");

        // 4. Return your JWT to the browser
        return Ok(result);
    }
    private void SetToken(AuthResponse authResponse)
    {
        var token = _tokenService.GenerateToken(authResponse);
        authResponse.Token = token;
    }
}