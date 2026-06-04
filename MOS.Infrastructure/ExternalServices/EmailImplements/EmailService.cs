using MOS.Application.Services.Interfaces;

namespace MOS.Infrastructure.ExternalServices.EmailImplements
{
    public class EmailService : IEmailService
    {
        // TODO: inject email configuration (SMTP settings from appsettings.json)

        public async Task SendAsync(string toEmail, string subject, string body)
        {
            // TODO: check whitelist before sending
            // TODO: implement SMTP email sending
        }

        public Task SendEmailAsync(string to, string subject, string body)
        {
            throw new NotImplementedException();
        }
    }
}
