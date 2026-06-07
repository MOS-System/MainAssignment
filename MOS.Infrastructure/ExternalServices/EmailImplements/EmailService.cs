using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Microsoft.Extensions.Options;
using MOS.Application.Services.Interfaces;
using MOS.Domain.Entities;
using MOS.Domain.Enums;
using MOS.Infrastructure.Interfaces;
using System.Text;

namespace MOS.Infrastructure.ExternalServices.Email
{
    public class EmailService : IEmailService
    {
        private readonly GmailApiSetting _setting;
        private readonly IAuditRepository _auditRepository;

        public EmailService(IOptions<GmailApiSetting> options, IAuditRepository auditRepository)
        {
            _setting = options.Value;
            _auditRepository = auditRepository;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var credential = new UserCredential(
                new GoogleAuthorizationCodeFlow(
                    new GoogleAuthorizationCodeFlow.Initializer
                    {
                        ClientSecrets = new ClientSecrets
                        {
                            ClientId = _setting.ClientId,
                            ClientSecret = _setting.ClientSecret
                        },
                        Scopes = new[] { GmailService.Scope.GmailSend }
                    }),
                "user",
                new Google.Apis.Auth.OAuth2.Responses.TokenResponse
                {
                    RefreshToken = _setting.RefreshToken
                });

            var service = new GmailService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = _setting.ApplicationName
            });

            var message = new Message
            {
                Raw = CreateRawMessage(_setting.SenderEmail, to, subject, body)
            };

            await service.Users.Messages.Send(message, "me").ExecuteAsync();
        }

        private static string CreateRawMessage(
            string from,
            string to,
            string subject,
            string body)
        {
            var raw =
                $"From: {from}\r\n" +
                $"To: {to}\r\n" +
                $"Subject: {subject}\r\n" +
                "Content-Type: text/plain; charset=utf-8\r\n\r\n" +
                body;

            return Convert.ToBase64String(Encoding.UTF8.GetBytes(raw))
                .Replace("+", "-")
                .Replace("/", "_")
                .Replace("=", "");
        }
    }
}