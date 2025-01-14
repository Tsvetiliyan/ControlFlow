using ControlFlow.Core.Enums.EmailEnums;
using ControlFlow.Core.Results;

namespace ControlFlow.Core.Interfaces.IServices;

public interface IEmailConfirmationService
{
    Task<Result> SendConfirmationEmailAsync(string userId, string link);
    Task<EmailConfirmationResult> ValidateConfirmationEmailAsync(string userId, string token);
}
