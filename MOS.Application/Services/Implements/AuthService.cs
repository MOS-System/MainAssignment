using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Server.IIS;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MOS.Application.DTOs.Requests.Auth;
using MOS.Application.DTOs.Responses.Auth;
using MOS.Application.DTOs.Responses.Products;
using MOS.Application.DTOs.Responses.Users;
using MOS.Application.Exceptions;
using MOS.Application.Services.Interfaces;
using MOS.Domain.Entities;
using MOS.Domain.Enums;
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
        private readonly IProductRepository _productRepository;

        public AuthService(
            IUserRepository userRepository,
            ITenantRepository tenantRepository,
            ITokenService tokenService,
            IPasswordService passwordService,
            IAuditRepository auditRepository,
            IProductRepository productRepository,
            ILogger<AuthService> logger,
            IMapper mapper, IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration) : base(logger, mapper, httpContextAccessor, configuration)
        {
            _userRepository = userRepository;
            _tenantRepository = tenantRepository;
            _productRepository = productRepository;
            //_tokenService = tokenService;
            _passwordService = passwordService;
            _auditRepository = auditRepository;
        }

        public async Task<AuthResponse> RegisterUserWithProducts(RegisterRequest registerRequest)
        {
            // check email taken
            if (await _userRepository.EmailExistsAsync(registerRequest.Email)) throw new ConflictException("User", "email");

            var passwordHash = _passwordService.HashPassword(registerRequest.Password);

            // create new user
            var user = new User
            (
                registerRequest.Name,
                registerRequest.Email,
                passwordHash,
                registerRequest.Phone,
                registerRequest.UserId,
                null,
                RoleType.Administrator
            );
            await _userRepository.AddUserAsync(user);

            // log audit
            await _auditRepository.AddAsync(
                new AuditLog(
                    user.Id,
                    user.Name,
                    user.Email,
                    AuditAction.UserAdded,
                    $"User {user.Email} created")
                );

            var products = await _productRepository.GetAllAsync();
            var productResponses = products.Select(p => _mapper.Map<ProductResponse>(p)).ToList();
            var authResponse = _mapper.Map<AuthResponse>(user);
            authResponse.Products = productResponses;

            return authResponse;
        }

        public async Task<AuthResponse> AuthenticateUserWithProducts(LoginRequest loginRequest)
        {
            var (user, products) = await _userRepository.AuthenticateUserWithProducts(loginRequest);

            if (user == null) throw new NotFoundException("User", loginRequest);

            var authResponse = _mapper.Map<AuthResponse>(user);
            if (products != null) authResponse.Products = products.Select(p => _mapper.Map<ProductResponse>(p)).ToList();

            return authResponse;
        }
    }
}
