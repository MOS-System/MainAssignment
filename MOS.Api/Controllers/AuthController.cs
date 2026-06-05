using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MOS.Api.Controllers;
using MOS.Api.EndPoints;
using MOS.Application.DTOs.Requests.Auth;
using MOS.Application.DTOs.Responses.Auth;
using MOS.Application.ExternalServices.AuthInterfaces;
using MOS.Application.ExternalServices.SecurityInterfaces;
using MOS.Application.Services.Interfaces;
using System.Text.Json;


[ApiController]
public class AuthController : BaseController<AuthController>
{
    private readonly IAuthService _authService;
    private readonly ITokenService _tokenService;
    private readonly IAuditService _auditService;
    private readonly IMfaService _mfaService;
    private readonly IMicrosoftService _microsoftService;
    private readonly IGoogleService _googleService;
    public AuthController(
        IAuthService authService,
        ITokenService tokenService,
        ILogger<AuthController> logger,
        IConfiguration configuration,
        IAuditService auditService,
        IMfaService mfaService,
        IGoogleService googleService,
        IMicrosoftService microsoftService) : base(configuration, logger)
    {
        _authService = authService;
        _tokenService = tokenService;
        _auditService = auditService;
        _mfaService = mfaService;
        _microsoftService = microsoftService;
        _googleService = googleService;
    }

    // POST api/v1/auth/verify-mfa
    [HttpPost(Endpoints.AuthEnpoints.VerifyMfaCode)]
    public async Task<IActionResult> VerifyMfaCode([FromBody] VerifyRequest request)
    {
        var authResponse = await _mfaService.VerifyMfaCodeAndAuthUserWithProduct(request);
        return Ok(authResponse);

    }


    // POST api/v1/auth/login
    [HttpPost(Endpoints.AuthEnpoints.Login)]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var code = await _mfaService.GetMfaCode(request);

        return Ok(code);
    }



    // POST api/v1/auth/register
    [HttpPost(Endpoints.AuthEnpoints.Register)]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var authResponse = await _authService.RegisterUserWithProducts(request);
        return Ok(authResponse);
    }


    // POST api/v1/auth/logout
    [HttpPost(Endpoints.AuthEnpoints.Logout)]
    public async Task<IActionResult> Logout()
    {
        await _auditService.LogLogoutAsync();
        return Ok();
    }


    [HttpGet(Endpoints.AuthEnpoints.GoogleLogin)]
    [AllowAnonymous]
    public IActionResult GoogleLogin()
    {
        var redirectUrl = _configuration["GoogleOAuth:UrlComplete"];
        var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
        return Challenge(properties, GoogleDefaults.AuthenticationScheme);

    }


    [HttpGet(Endpoints.AuthEnpoints.GoogleComplete)]
    [AllowAnonymous]
    public async Task<IActionResult> GoogleComplete()
    {
        var result = await HttpContext.AuthenticateAsync(
         CookieAuthenticationDefaults.AuthenticationScheme);

        var response = await _googleService.HandleGoogleCompleteAsync(result);
        var feUrl = _configuration["FrontendRedirect:Url"];

        var json = JsonSerializer.Serialize(response);
        var encoded = Uri.EscapeDataString(json);
        return Redirect($"{feUrl}/?authResponse={encoded}");

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

        var feUrl = _configuration["FrontendRedirect:Url"];

        if (result.RequiresRegistration)
        {
            // user not found → send to register page with pre-filled data
            var registerUrl = $"{feUrl}/register" +
                $"?email={Uri.EscapeDataString(result.Email)}" +
                $"&name={Uri.EscapeDataString(result.Name)}" +
                $"&userName={Uri.EscapeDataString(result.UserName)}" +
                $"&signinMethod={Uri.EscapeDataString(result.SigninMethod.ToString())}";

            return Redirect(registerUrl);
        }

        // user found → send to FE with JWT
        var json = JsonSerializer.Serialize(result);
        var encoded = Uri.EscapeDataString(json);
        return Redirect($"{feUrl}/?authResponse={encoded}");
    }
}