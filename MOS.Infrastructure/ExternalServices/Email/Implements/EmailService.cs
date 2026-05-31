using MOS.Application.Services.Interfaces;

namespace MOS.Infrastructure.ExternalServices.Email.Implements
{
    public class EmailService : IEmailService
    {
        // TODO: inject email configuration (SMTP settings from appsettings.json)

        public async Task SendAsync(string toEmail, string subject, string body)
        {
            // TODO: check whitelist before sending
            // TODO: implement SMTP email sending
        }
    }
}
