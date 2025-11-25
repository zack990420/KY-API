namespace KYAPI.Services;

public interface IBackgroundJobService
{
    void SendWelcomeEmail(string email, string username);
    void ProcessDataCleanup();
    void GenerateMonthlyReport();
}

public class BackgroundJobService : IBackgroundJobService
{
    private readonly ILogger<BackgroundJobService> _logger;
    private readonly IEmailService _emailService;

    public BackgroundJobService(ILogger<BackgroundJobService> logger, IEmailService emailService)
    {
        _logger = logger;
        _emailService = emailService;
    }

    public void SendWelcomeEmail(string email, string username)
    {
        _logger.LogInformation("Sending welcome email to {Email}", email);
        
        try
        {
            var subject = "Welcome to MyWebApi!";
            var body = $"<h1>Welcome {username}!</h1><p>Thank you for registering.</p>";
            
            _emailService.SendEmailAsync(email, subject, body, isHtml: true).Wait();
            
            _logger.LogInformation("Welcome email sent successfully to {Email}", email);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send welcome email to {Email}", email);
            throw;
        }
    }

    public void ProcessDataCleanup()
    {
        _logger.LogInformation("Starting data cleanup job");
        
        try
        {
            // Example: Clean up old logs, expired tokens, etc.
            _logger.LogInformation("Data cleanup completed successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Data cleanup failed");
            throw;
        }
    }

    public void GenerateMonthlyReport()
    {
        _logger.LogInformation("Starting monthly report generation");
        
        try
        {
            // Example: Generate and email monthly reports
            _logger.LogInformation("Monthly report generated successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Monthly report generation failed");
            throw;
        }
    }
}
