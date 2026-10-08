namespace PortfolioApp.Services.Interfaces
{
    /// <summary>Sends transactional emails (contact form notifications) via SMTP.</summary>
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string htmlBody);
    }
}
