using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using PortfolioApp.Services.Interfaces;

namespace PortfolioApp.Services.Implementations
{
    /// <summary>MailKit based SMTP email sender, configured from the "SmtpSettings" section of appsettings.json.</summary>
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IConfiguration config, ILogger<EmailService> logger)
        {
            _config = config;
            _logger = logger;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string htmlBody)
        {
            try
            {
                var smtp = _config.GetSection("SmtpSettings");

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(smtp["SenderName"], smtp["SenderEmail"]));
                message.To.Add(MailboxAddress.Parse(toEmail));
                message.Subject = subject;
                message.Body = new BodyBuilder { HtmlBody = htmlBody }.ToMessageBody();

                using var client = new SmtpClient();
                await client.ConnectAsync(smtp["Host"], int.Parse(smtp["Port"] ?? "587"), SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(smtp["Username"], smtp["Password"]);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                // Never let a mail failure break the contact form submission itself.
                _logger.LogError(ex, "Failed to send email to {ToEmail}", toEmail);
            }
        }
    }
}
