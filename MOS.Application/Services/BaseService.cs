using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MOS.Domain.Entities;
using System.Security.Claims;
using AutoMapper;


namespace MOS.Application.Services
{
    public abstract class BaseService<T> where T : class
    {
        protected ILogger<T> _logger;
        protected IMapper _mapper;
        protected IHttpContextAccessor _httpContextAccessor;
        protected IConfiguration _configuration;
        public BaseService(ILogger<T> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _logger = logger;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }
        protected string GetRoleFromJwt()
        {
            string role = _httpContextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.Role)!;
            return role;
        }
        protected Guid GetUserIdFromJwt()
        {
            //return Guid.Parse(_httpContextAccessor?.HttpContext?.User?.FindFirstValue("userId")!);
            throw new NotImplementedException("This method should be implemented in the derived service class where the UnitOfWork is available.");
        }
        protected async Task<User> GetUserFromJwt()
        {
                  throw new NotImplementedException("This method should be implemented in the derived service class where the UnitOfWork is available.");
        }

     
        protected bool IsAuthorized()
        {
            var httpContext = _httpContextAccessor.HttpContext;


            if (!httpContext!.User.Identity!.IsAuthenticated)
            {
                return false;
            }
            return true;
        }
    }
}
