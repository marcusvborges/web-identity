
using System.Net;
using System.Net.Mail;

namespace WebIdentity.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var smtpClient = new SmtpClient(_configuration["Email:SmtpServer"])
            {
                Port = int.Parse(_configuration["Email:Port"]),
                Credentials = new NetworkCredential(
                    _configuration["Email:Username"],
                    _configuration["Email:Password"]
                ),
                EnableSsl = true
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_configuration["Email:From"]),
                Subject = subject,
                Body = message,
                IsBodyHtml = true
            };

            mailMessage.To.Add(email);

            await smtpClient.SendMailAsync(mailMessage);
        }

        public async Task SendEmailBodyAsync(string email, string resetLink)
        {
            string emailBody = $@"
                <div style='font-family: Arial, sans-serif; text-align: center; padding: 20px;'>

                    <h2 style='color: #333;'>Hello!</h2>

                    <p style='color: #555; font-size: 16px;'>Forgot your password? Let's make another one!</p>

                    <a href='{resetLink}' 
                        style='display: inline-block; padding: 12px 24px; margin: 20px 0; background-color: #24A0ED; 
                               color: white; text-decoration: none; font-weight: bold; border-radius: 5px;'>
                        RESET PASSWORD
                    </a>

                    <p style='color: #777; font-size: 14px;'>If you do not wish to change your password,
                        just ignore this email and it will not be changed.</p>
                </div>";

            await SendEmailAsync(email, "Password reset", emailBody);
        }
    }
}
