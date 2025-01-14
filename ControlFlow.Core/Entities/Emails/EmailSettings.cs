namespace ControlFlow.Core.Entities.Emails;

public class EmailSettings
{
    public required string SmtpServer { get; set; }
    public required string SenderEmail { get; set; }
    public required string Password { get; set; }
    public int Port { get; set; }
}
