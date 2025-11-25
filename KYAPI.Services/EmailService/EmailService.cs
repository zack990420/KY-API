using KYAPI.Repositories;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace KYAPI.Services;

public class EmailService : IEmailService
{
    private readonly IEmailConfigRepository _emailConfigRepository;
    private readonly ILogger<EmailService> _logger;

    public EmailService(IEmailConfigRepository emailConfigRepository, ILogger<EmailService> logger)
    {
        _emailConfigRepository = emailConfigRepository;
        _logger = logger;
    }

    public async Task SendEmailAsync(string to, string subject, string body, bool isHtml = true)
    {
        var config = await _emailConfigRepository.GetActiveConfigAsync();

        if (config == null)
        {
            throw new InvalidOperationException("No active email configuration found in database.");
        }

        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(config.FromName, config.FromEmail));
        message.To.Add(MailboxAddress.Parse(to));
        message.Subject = subject;

        var bodyBuilder = new BodyBuilder();
        if (isHtml)
        {
            bodyBuilder.HtmlBody = body;
        }
        else
        {
            bodyBuilder.TextBody = body;
        }
        message.Body = bodyBuilder.ToMessageBody();

        using var client = new SmtpClient();
        try
        {
            await client.ConnectAsync(
                config.SmtpHost,
                config.SmtpPort,
                config.EnableSsl ? SecureSocketOptions.StartTls : SecureSocketOptions.None
            );
            await client.AuthenticateAsync(config.SmtpUsername, config.SmtpPassword);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);

            _logger.LogInformation("Email sent successfully to {To}", to);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send email to {To}", to);
            throw;
        }
    }
}
