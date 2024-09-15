using DnsClient;
using System.Net.Mail;

namespace Cmos.IDP.Services
{
    public class EmailService
    {
        private readonly SmtpClient _smtpClient;
        private readonly ILogger<EmailService> _logger;
        private readonly ILookupClient _lookupClient;
        public static bool isUserRegisterd { get; set; } = false;
        public EmailService(SmtpClient smtpClient, ILookupClient lookupClient , ILogger<EmailService> logger)
        {
            _smtpClient = smtpClient;
            _lookupClient = lookupClient;
            _logger = logger;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress("admin@timetotimetechnical.com"),
                Subject = subject,
                Body = message,
            };

            mailMessage.To.Add(email);
            try
            { 
                if(IsValidEmail(email)) {
                await _smtpClient.SendMailAsync(mailMessage);
                    isUserRegisterd = true;
                _logger.LogInformation("Email sent to {Email}", email);
                } else {
                    isUserRegisterd = false;
                    _logger.LogError("Email validation failed for {Email}", email);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send email to {Email}", email);
                throw; // Re-throw the exception after logging
            }
        }
            public bool IsValidEmail(string email)
            {
                try
                {
                    var domain = email.Split('@')[1];
                    var result = _lookupClient.Query(domain, QueryType.MX);
                 _logger.LogInformation("Email validation result for {Email}: {Result}", email, result.Answers.Any());
                return result.Answers.Any();

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to validate email {Email}", email);
                    return false;
                }

        }
    }
}
