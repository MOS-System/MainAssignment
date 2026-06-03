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
    public AuthController( IAuthService authService, ITokenService tokenService, ILogger<AuthController> logger) : base(logger)
    {
        _authService = authService;
        _tokenService = tokenService;
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

    // POST api/auth/verify-mfa (bonus)
    //[HttpPost("verify-mfa")]
    //public async Task<IActionResult> VerifyMfa([FromBody] VerifyMfaRequest request)
    //{
    //    // TODO: call _mfaService.VerifyCodeAsync
    //    // TODO: return 200 with token if valid
    //    throw new NotImplementedException();
    //}

    private void SetToken(AuthResponse authResponse)
    {
        var token = _tokenService.GenerateToken(authResponse);
        authResponse.Token = token;
    }
}