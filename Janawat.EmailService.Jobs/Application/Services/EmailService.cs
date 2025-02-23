using Janawat.EmailService.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;
 

namespace Janawat.EmailService.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string to, string subject, string body, CancellationToken cancellationToken = default)
        {
            // Retrieve SMTP settings from configuration
            var smtpSettings = _configuration.GetSection("SmtpSettings");

            // Create a new email message
            var email = new MimeMessage();

            // Set the sender, recipient, subject, and body of the email
            email.From.Add(MailboxAddress.Parse(smtpSettings["Username"]));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = body };

            using var smtp = new SmtpClient();
            try
            {
                // Connect to the SMTP server
                await smtp.ConnectAsync(smtpSettings["Host"], int.Parse(smtpSettings["Port"]), MailKit.Security.SecureSocketOptions.StartTls, cancellationToken);

                // Authenticate with the SMTP server
                await smtp.AuthenticateAsync(smtpSettings["Username"], smtpSettings["Password"], cancellationToken);

                // Send the email
                await smtp.SendAsync(email, cancellationToken);
                Console.WriteLine($"Email sent successfully to {to}");
            }
            catch (Exception ex)
            {
                // Log any exceptions that occur during the email sending process
                Console.WriteLine($"Failed to send email: {ex.Message}");
            }
            finally
            {
                // Disconnect from the SMTP server
                await smtp.DisconnectAsync(true, cancellationToken);
            }
        }
    }
}
