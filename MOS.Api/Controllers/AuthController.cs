using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MOS.Api.Controllers;
using MOS.Api.EndPoints;
using MOS.Application.DTOs.Requests.Auth;
using MOS.Application.DTOs.Responses.Auth;
using MOS.Application.Services.Interfaces;
using MOS.Domain.Entities;
using MOS.Domain.Enums;
using System.Data;

[ApiController]
public class AuthController : BaseController<AuthController>
{
    private readonly IAuthService _authService;
    private readonly ITokenService _tokenService;
    public AuthController(IAuthService authService,ITokenService tokenService , ILogger<AuthController> logger) : base(logger)
    {
        _authService = authService;
        _tokenService = tokenService;
    }
    


    // POST api/auth/login
    
    [HttpPost(Endpoints.AuthEnpoints.Login)]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var tenant = await _authService.GetTenantByLoginRequest(request);
        var token = _tokenService.GenerateToken( tenant, RoleType.Administrator.ToString());

        // 5. Return response
        var response = new AuthResponse
        {
            Token = token,
            Name = tenant.Name,
            Email = tenant.Name,
            Role = RoleType.Administrator

        };

        return Ok(response);
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