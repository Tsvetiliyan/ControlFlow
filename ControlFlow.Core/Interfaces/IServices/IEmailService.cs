namespace ControlFlow.Core.Interfaces.IServices;

public interface IEmailService
{
    Task<bool> SendEmailAsync(string recipientEmail, string subject, string htmlBody, string textBody);
}
