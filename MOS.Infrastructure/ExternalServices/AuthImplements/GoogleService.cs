using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using MOS.Application.DTOs.Responses.Auth;
using MOS.Application.DTOs.Responses.Products;
using MOS.Application.Exceptions;
using MOS.Application.ExternalServices.AuthInterfaces;
using MOS.Application.ExternalServices.SecurityInterfaces;
using MOS.Application.Services.Interfaces;
using MOS.Domain.Entities;
using MOS.Domain.Enums;
using MOS.Infrastructure.Implements;
using MOS.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace MOS.Infrastructure.ExternalServices.AuthImplements
{
    public class GoogleService : IGoogleService
    {
        private readonly IConfiguration _configuration;
        private readonly ITokenService _tokenService;
        private readonly IUserRepository _userRepository;
        private readonly IPermissionRepository _permissionRepository;
        private readonly IAuditRepository _auditRepository;
        private readonly IEmailWhitelistRepository _emailWhitelistRepository;
        private readonly IMapper _mapper;


        public GoogleService(
            IConfiguration configuration, 
            ITokenService tokenService, 
            IUserRepository userRepository, 
            IPermissionRepository permissionRepository, 
            IAuditRepository auditRepository, 
            IEmailWhitelistRepository emailWhitelistRepository,
            IMapper mapper)
        {
            _configuration = configuration;
            _tokenService = tokenService;
            _userRepository = userRepository;
            _permissionRepository = permissionRepository;
            _auditRepository = auditRepository;
            _emailWhitelistRepository = emailWhitelistRepository;
            _mapper = mapper;
        }

        public async Task<AuthResponse> HandleGoogleCompleteAsync(AuthenticateResult result)
        {
            if (!result.Succeeded)
                throw new ConflictException("Google", "Login Google Failed With Error Result");

            var claims = result.Principal!.Claims;
            var email = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var name = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            var givenName = claims.FirstOrDefault(c => c.Type == ClaimTypes.GivenName)?.Value;
            var surName = claims.FirstOrDefault(c => c.Type == ClaimTypes.Surname)?.Value;

            var emailVerified = result.Principal!.FindFirst("email_verified")?.Value;

            var isVerified = emailVerified == null || emailVerified == "true";


            if (!isVerified)
                throw new ForbiddenException("Google Email", "Not Veryfied");

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(name))
                throw new ConflictException("Google Claims", "Cannot Receiver");

            var authResponse = new AuthResponse
            {
                UserName = name,
                Name = givenName + surName,
                Email = email,
                SigninMethod = SigninMethod.google
            };

            var existUser = await _userRepository.GetUserByEmailAsync(email);
            if(existUser == null)
            {
                var isWhiteListEnable = await _emailWhitelistRepository.GetSettingAsync();
                if (isWhiteListEnable != null && isWhiteListEnable.IsEnabled)
                {
                    var emailWhiteList = await _emailWhitelistRepository.GetEmailsAsync();
                    if (!emailWhiteList.Any(e => string.Equals(e.Email, authResponse.Email, StringComparison.OrdinalIgnoreCase)))
                        throw new ForbiddenException("User Email are not allow to use MOS system", authResponse.Email);
                }

                authResponse.RequiresRegistration = true;
                return authResponse;
            }

            authResponse = _mapper.Map<AuthResponse>(existUser);
            authResponse.Token = _tokenService.GenerateToken(authResponse);

            var products = await _permissionRepository.GetProductsByUserIdAsync(existUser.Id);
            authResponse.Products = products.Select(p => _mapper.Map<ProductResponse>(p)).ToList();

            await LogAudit(authResponse);

            return authResponse;
        }


        private async Task LogAudit(AuthResponse authReponse)
        {
            await _auditRepository.AddAsync(
           new AuditLog(
           authReponse.Id,
           authReponse.Name,
           authReponse.UserName,
           CategoryLogType.Account.ToString(),
           authReponse.Email,
           AuditAction.SignIn,
           $"User {authReponse.Email} login via Google")
          );
        }
    }
}
