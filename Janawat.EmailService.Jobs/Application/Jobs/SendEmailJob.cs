using Janawat.EmailService.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using Quartz;

namespace Janawat.EmailService.Application.Jobs
{
    public class SendEmailJob : IJob
    {
        private readonly IEmailService _emailService;
        private readonly ILogger<SendEmailJob> _logger;

        public SendEmailJob(IEmailService emailService, ILogger<SendEmailJob> logger)
        {
            _emailService = emailService;
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("SendEmailJob is starting...");

            try
            {
                // Attempt to send an email using the IEmailService
                await _emailService.SendEmailAsync("nuchit@outlook.com", "Scheduled Email", "Hello! This is a scheduled email.");
                _logger.LogInformation("Email sent successfully.");
            }
            catch (Exception ex)
            {
                // Log an error if the email sending fails
                _logger.LogError(ex, "Failed to send email.");
            }

            _logger.LogInformation("SendEmailJob completed.");
        }
    }
}
