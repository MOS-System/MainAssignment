using Microsoft.AspNetCore.Mvc;
using MOS.Application.DTOs.Requests.Auth;
using MOS.Application.Services;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    // POST api/auth/login
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        // TODO: call _authService.LoginAsync
        // TODO: return 200 with AuthResponse
        throw new NotImplementedException();
    }

    // POST api/auth/register
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        // TODO: call _authService.RegisterAsync
        // TODO: return 201 with AuthResponse
        throw new NotImplementedException();
    }

    // POST api/auth/verify-mfa (bonus)
    //[HttpPost("verify-mfa")]
    //public async Task<IActionResult> VerifyMfa([FromBody] VerifyMfaRequest request)
    //{
    //    // TODO: call _mfaService.VerifyCodeAsync
    //    // TODO: return 200 with token if valid
    //    throw new NotImplementedException();
    //}
}