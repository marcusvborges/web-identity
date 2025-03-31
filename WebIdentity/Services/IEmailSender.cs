namespace WebIdentity.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
        Task SendEmailBodyAsync(string email, string resetLink);
    }
}
