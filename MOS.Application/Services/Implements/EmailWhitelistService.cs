using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MOS.Application.Services.Interfaces;

namespace MOS.Application.Services.Implements
{
    // manage whitelist toggle + email list
    public class EmailWhitelistService : BaseService<EmailWhitelistService>, IEmailWhiteListService
    {
        private readonly IEmailWhitelistRepository _whitelistRepository;

        public EmailWhitelistService(
            IEmailWhitelistRepository emailWhitelistRepository, 
            ILogger<EmailWhitelistService> logger, IMapper mapper, 
            IHttpContextAccessor httpContextAccessor, 
            IConfiguration configuration) : base(logger, mapper, httpContextAccessor, configuration)
        {
            _whitelistRepository = emailWhitelistRepository;
        }



        // TODO: GetSettingAsync - returns WhitelistSettingResponse
        // returns current toggle state and full email list

        // TODO: UpdateSettingAsync - takes UpdateWhitelistSettingRequest
        // enable or disable the whitelist toggle

        // TODO: AddEmailAsync - takes AddEmailToWhitelistRequest
        // check not duplicate, add email

        // TODO: RemoveEmailAsync - takes RemoveEmailFromWhitelistRequest
        // check exists, remove email

        // TODO: IsAllowedAsync - takes email string, returns bool
        // if whitelist disabled return true, else check if email is in list
    }
}
