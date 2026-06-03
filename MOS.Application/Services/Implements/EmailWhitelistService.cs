using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MOS.Application.DTOs.Requests.EmailWhitelist;
using MOS.Application.DTOs.Responses.EmailWhitelist;
using MOS.Application.Exceptions;
using MOS.Application.Services.Interfaces;
using MOS.Domain.Entities;
using MOS.Domain.Enums;
using MOS.Infrastructure.Interfaces;

namespace MOS.Application.Services.Implements
{
    public class EmailWhitelistService
        : BaseService<EmailWhitelistService>, IEmailWhitelistService
    {
        private readonly IEmailWhitelistRepository _emailWhitelistRepository;
        private readonly IAuditRepository _auditRepository;

        public EmailWhitelistService(
            IEmailWhitelistRepository emailWhitelistRepository,
            IAuditRepository auditRepository,
            ILogger<EmailWhitelistService> logger,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration)
            : base(logger, mapper, httpContextAccessor, configuration)
        {
            _emailWhitelistRepository = emailWhitelistRepository;
            _auditRepository = auditRepository;
        }

        public async Task<EmailWhitelistResponse> GetWhitelistAsync()
        {
            var setting = await GetOrCreateSettingAsync();
            var emails = await _emailWhitelistRepository.GetEmailsAsync();

            return new EmailWhitelistResponse
            {
                IsEnabled = setting.IsEnabled,
                Emails = emails.Select(e => new EmailWhitelistItemResponse
                {
                    Id = e.Id,
                    Email = e.Email,
                    AddedAt = e.AddedAt
                }).ToList()
            };
        }

        public async Task UpdateSettingAsync(UpdateEmailWhitelistSettingRequest request)
        {
            var setting = await GetOrCreateSettingAsync();

            setting.SetEnabled(request.IsEnabled);

            await _emailWhitelistRepository.UpdateSettingAsync(setting);

            await _auditRepository.AddAsync(new AuditLog(
                GetUserIdFromJWT(),
                GetUserNameFromJWT(),
                GetUserEmailFromJWT(),
                AuditAction.WhitelistSettingChanged,
                $"Email whitelist setting changed to {(request.IsEnabled ? "enabled" : "disabled")}"
            ));
        }

        public async Task AddEmailAsync(AddEmailWhitelistRequest request)
        {
            var email = request.Email.Trim().ToLower();

            if (await _emailWhitelistRepository.EmailExistsAsync(email))
            {
                throw new ConflictException("EmailWhitelist", "email already exists");
            }

            var whitelistEmail = new EmailWhitelist(email);

            await _emailWhitelistRepository.AddEmailAsync(whitelistEmail);

            await _auditRepository.AddAsync(new AuditLog(
                GetUserIdFromJWT(),
                GetUserNameFromJWT(),
                GetUserEmailFromJWT(),
                AuditAction.AddedWhitelistEmail,
                $"Email {email} added to whitelist"
            ));
        }

        public async Task RemoveEmailAsync(int id)
        {
            var email = await _emailWhitelistRepository.GetEmailByIdAsync(id)
                ?? throw new NotFoundException("EmailWhitelist", id);

            await _emailWhitelistRepository.RemoveEmailAsync(email);

            await _auditRepository.AddAsync(new AuditLog(
                GetUserIdFromJWT(),
                GetUserNameFromJWT(),
                GetUserEmailFromJWT(),
                AuditAction.RemovedWhitelistEmail,
                $"Email {email.Email} removed from whitelist"
            ));
        }

        public async Task<bool> IsAllowedAsync(string email)
        {
            var setting = await GetOrCreateSettingAsync();

            if (!setting.IsEnabled)
            {
                return true;
            }

            return await _emailWhitelistRepository.EmailExistsAsync(
                email.Trim().ToLower());
        }

        private async Task<EmailWhitelistSetting> GetOrCreateSettingAsync()
        {
            var setting = await _emailWhitelistRepository.GetSettingAsync();

            if (setting != null)
            {
                return setting;
            }

            setting = new EmailWhitelistSetting();
            await _emailWhitelistRepository.AddSettingAsync(setting);

            return setting;
        }
    }
}