using AutoMapper;
using DocumentFormat.OpenXml.Spreadsheet;
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
    // generate code, validate, expire
    public class MfaService : BaseService<MfaService>, IMfaService
    {
        private readonly IMfaRepository _mfaRepository;
        private readonly IUserRepository _userRepository;
        private readonly IAuditRepository _auditRepository;
        private readonly IEmailService _emailService;
        private readonly ITokenService _tokenService;


        public MfaService(
            IMfaRepository mfaRepository,
            IUserRepository userRepository,
            IAuditRepository auditRepository,
            IEmailService emailService,
            ITokenService tokenService,
            ILogger<MfaService> logger,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration) : base(logger, mapper, httpContextAccessor, configuration)
        {
            _mfaRepository = mfaRepository;
            _userRepository = userRepository;
            _auditRepository = auditRepository;
            _emailService = emailService;
            _tokenService = tokenService;
        }

        public async Task<string> GetMfaCode(LoginRequest loginRequest)
        {
            var userExisted = await _userRepository.GetUserByEmailAsync(loginRequest.Email);
            if (userExisted == null) throw new NotFoundException("User", "Email Not Found");

            var code = await _mfaRepository.GenerateMfaCode(userExisted.Id);

            await _emailService.SendEmailAsync(
            userExisted.Email,
            "FROM MOS SYSTEM",
            $"Hello {userExisted.Name},\n\n" +
            "Here your OPT code for MOS account created. The code will experied in 60s\n\n" +
            $"Code: {code}\n\n" +
            "Do not share the code for anyone\n" +
            "For any further information please contact in MOS");

            return code;
        }

        public async Task<AuthResponse> VerifyMfaCodeAndAuthUserWithProduct(VerifyRequest verifyRequest)
        {
            var (user, products) = await _userRepository.AuthenticateUserWithProducts(verifyRequest);
        
            if (user == null) throw new NotFoundException("User", "Incorrect Email or Password");

            if (!await _mfaRepository.VerifyMfaCode(user.Id, verifyRequest.MfaCode)) throw new ConflictException("Mfa Code", "Incorrect");

            var authResponse = _mapper.Map<AuthResponse>(user);
             authResponse.Token = _tokenService.GenerateToken(authResponse);
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


            await _mfaRepository.UpdateMfaCodeStatus(user.Id, verifyRequest.MfaCode);

            return authResponse;
        }
    }
}
