using MOS.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MOS.Application.Services
{
    // manage whitelist toggle + email list
    public class EmailWhitelistService
    {
        private readonly IEmailWhitelistRepository _whitelistRepository;

        public EmailWhitelistService(IEmailWhitelistRepository whitelistRepository)
        {
            _whitelistRepository = whitelistRepository;
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
