using AutoMapper;
using DocumentFormat.OpenXml.Office2016.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MOS.Application.DTOs.Requests.Auth;
using MOS.Application.DTOs.Responses.Auth;
using MOS.Application.DTOs.Responses.Products;
using MOS.Application.Exceptions;
using MOS.Application.ExternalServices.SecurityInterfaces;
using MOS.Application.Services.Interfaces;
using MOS.Domain.Entities;
using MOS.Domain.Enums;
using MOS.Infrastructure.Interfaces;

namespace MOS.Application.Services.Implements
{
    // login, register, JWT issue
    public class AuthService : BaseService<AuthService>, IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordService _passwordService;
        private readonly IAuditRepository _auditRepository;
        private readonly IProductRepository _productRepository;
        private readonly IEmailWhitelistRepository _emailWhitelistRepository;
        private readonly ITenantRepository _tenantRepository;
        private readonly IEmailService _emailService;


        public AuthService(
            IUserRepository userRepository,
            ITokenService tokenService,
            IPasswordService passwordService,
            IAuditRepository auditRepository,
            IProductRepository productRepository,
            IEmailWhitelistRepository emailWhitelistRepository,
            ITenantRepository tenantRepository,
            IEmailService emailService,
            ILogger<AuthService> logger,
            IMapper mapper, IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration) : base(logger, mapper, httpContextAccessor, configuration)
        {
            _userRepository = userRepository;
            _productRepository = productRepository;
            //_tokenService = tokenService;
            _passwordService = passwordService;
            _auditRepository = auditRepository;
            _emailWhitelistRepository = emailWhitelistRepository;
            _emailService = emailService;
            _tenantRepository = tenantRepository;
        }

        public async Task<AuthResponse> RegisterUserWithProducts(RegisterRequest registerRequest)
        {
            await ValidateRegisterRequest(registerRequest);

            // create new user
            var passwordHash = _passwordService.HashPassword(registerRequest.Password);
            var user = new User
            (
                registerRequest.Name,
                registerRequest.Email,
                passwordHash,
                registerRequest.UserName,
                registerRequest.Phone,
                registerRequest.TenantId,
                RoleType.TenantAdministrator,
                registerRequest.SigninMethod
            );
            await _userRepository.AddUserAsync(user);

            // log audit
            await _auditRepository.AddAsync(
                new AuditLog(
                    user.Id,
                    user.Name,
                    user.UserName,
                    CategoryLogType.Account.ToString(),
                    user.Email,
                    AuditAction.SignUp,
                    $"User {user.Email} created")
                );

            var products = await _productRepository.GetAllProductAsync();
            var productResponses = products.Select(p => _mapper.Map<ProductResponse>(p)).ToList();

            var authResponse = _mapper.Map<AuthResponse>(user);
            authResponse.Products = productResponses;

            await _emailService.SendEmailAsync(
            user.Email,
            "Your MOS account has been created",
            $"Hello {user.Name},\n\n" +
            "Your MOS account has been created.\n\n" +
            $"Username: {user.UserName}\n" +
            "For any further information please contact in MOS"
);
            return authResponse;
        }

        private async Task ValidateRegisterRequest(RegisterRequest registerRequest)
        {
            // check email taken
            if (await _userRepository.EmailExistsAsync(registerRequest.Email)) throw new ConflictException("User", "email");

            // Check email are in whitelist or not
            var isWhiteListEnable = await _emailWhitelistRepository.GetSettingAsync();
            if (isWhiteListEnable != null && isWhiteListEnable.IsEnabled)
            {
                var emailWhiteList = await _emailWhitelistRepository.GetEmailsAsync();
                if (!emailWhiteList.Any(e => string.Equals(e.Email, registerRequest.Email, StringComparison.OrdinalIgnoreCase)))
                    throw new ForbiddenException("User", registerRequest.Email);
            }

            //Check tenant exist
            if (await _tenantRepository.GetTenantByIdAsync(registerRequest.TenantId) == null) throw new NotFoundException("Tenant", registerRequest.TenantId);
        }

        public async Task<AuthResponse> AuthenticateUserWithProducts(LoginRequest loginRequest)
        {
            var (user, products) = await _userRepository.AuthenticateUserWithProducts(loginRequest);

            if (user == null) throw new NotFoundException("User", loginRequest);

            var authResponse = _mapper.Map<AuthResponse>(user);
            if (products != null) authResponse.Products = products.Select(p => _mapper.Map<ProductResponse>(p)).ToList();

            await _auditRepository.AddAsync(
             new AuditLog(
                 user.Id,
                 user.Name,
                 user.UserName,
                 CategoryLogType.Account.ToString(),
                 user.Email,
                 AuditAction.SignIn,
                 $"User {user.Email} login via local")
             );


            return authResponse;
        }


    }
}
