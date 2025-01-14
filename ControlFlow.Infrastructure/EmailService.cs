using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;
using ControlFlow.Core.Interfaces.IServices;
using ControlFlow.Core.Entities.Emails;

namespace ControlFlow.Infrastructure;

public class EmailService : IEmailService
{
    private readonly EmailSettings _emailSettings;
    private readonly ILogger<EmailService> _logger;

    public EmailService(IOptions<EmailSettings> emailSettings, ILogger<EmailService> logger)
    {
        _emailSettings = emailSettings.Value;
        _logger = logger;
    }

    public async Task<bool> SendEmailAsync(string recipientEmail, string subject, string htmlBody, string textBody)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("ControlFlow", _emailSettings.SenderEmail));
        message.To.Add(new MailboxAddress("Recipient", recipientEmail));
        message.Subject = subject;

        var bodyBuilder = new BodyBuilder { HtmlBody = htmlBody, TextBody = textBody };
        message.Body = bodyBuilder.ToMessageBody();

        using (var client = new SmtpClient())
        {
            try
            {
                await client.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.Port, SecureSocketOptions.SslOnConnect);
                await client.AuthenticateAsync(_emailSettings.SenderEmail, _emailSettings.Password);
                await client.SendAsync(message);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to send email: {ErrorMessage}", ex.Message);
                return false;
            }
            finally
            {
                // Disconnect from the server
                await client.DisconnectAsync(true);
            }
        }
    }
}
