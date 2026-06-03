using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MOS.Domain.Entities;
using System.Security.Claims;
using AutoMapper;
using MOS.Application.Services.Interfaces;


namespace MOS.Application.Services
{
    public abstract class BaseService<T> where T : class
    {
        protected ILogger<T> _logger;
        protected IMapper _mapper;
        //protected ITokenService _tokenService;
        protected IHttpContextAccessor _httpContextAccessor;
        protected IConfiguration _configuration;


        public BaseService(ILogger<T> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _logger = logger;
            _mapper = mapper;
            //_tokenService = tokenService;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        protected int GetUserIdFromJWT()
        {
            var id = _httpContextAccessor.HttpContext?.User?
                .FindFirst(ClaimTypes.NameIdentifier)?
                .Value;

            if (!int.TryParse(id, out var userId))
                throw new UnauthorizedAccessException();

            return userId;
        }


        protected string GetUserNameFromJWT()
        {
            var name = _httpContextAccessor.HttpContext?.User
                .FindFirstValue(ClaimTypes.Name);

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new UnauthorizedAccessException();
            }

            return name;
        }

        protected string GetUserEmailFromJWT()
        {
            var email = _httpContextAccessor.HttpContext?.User
                .FindFirstValue("email");

            if (string.IsNullOrWhiteSpace(email))
            {
                throw new UnauthorizedAccessException();
            }

            return email;
        }

    }
}
