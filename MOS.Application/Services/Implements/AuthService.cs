using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Server.IIS;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MOS.Application.DTOs.Requests.Auth;
using MOS.Application.Services.Interfaces;
using MOS.Domain.Entities;
using MOS.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MOS.Application.Services.Implements
{
    // login, register, JWT issue
    public class AuthService : BaseService<AuthService>, IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITenantRepository _tenantRepository;
        private readonly IPasswordService _passwordService;
        private readonly IAuditRepository _auditRepository;

        public AuthService(
            IUserRepository userRepository,
            ITenantRepository tenantRepository,
            ITokenService tokenService,
            IPasswordService passwordService,
            IAuditRepository auditRepository, 
            ILogger<AuthService> logger,
            IMapper mapper, IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration) : base(logger, mapper, httpContextAccessor, configuration)
        {
            _userRepository = userRepository;
            _tenantRepository = tenantRepository;
            //_tokenService = tokenService;
            _passwordService = passwordService;
            _auditRepository = auditRepository;
        }

        public async Task<Tenant> GetTenantByLoginRequest(LoginRequest loginRequest)
        {
            var tenant = _tenantRepository.GetTenantByNameAndPasswordAsync(loginRequest);
            if (tenant == null)
            {
                return null;
            }
            return await tenant;
        }
    }
}
