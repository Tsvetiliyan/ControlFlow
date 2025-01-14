using ControlFlow.Core;
using ControlFlow.Core.Entities;
using ControlFlow.Core.Enums.EmailEnums;
using ControlFlow.Core.Interfaces.IServices;
using ControlFlow.Core.Results;
using Microsoft.AspNetCore.Identity;
using System.Buffers.Text;

namespace ControlFlow.Services;

public class EmailConfirmationService : IEmailConfirmationService
{
    private readonly IEmailService _emailService;
    private readonly IApplicationUserService _applicationUserService;
    private readonly UserManager<ApplicationUser> _userManager;

    public EmailConfirmationService(IEmailService emailService, UserManager<ApplicationUser> userManager, IApplicationUserService applicationUserService)
    {
        _emailService = emailService;
        _userManager = userManager;
        _applicationUserService = applicationUserService;
    }

    public async Task<Result> SendConfirmationEmailAsync(string userId, string baseUrl)
    {
        ApplicationUser? user = await _applicationUserService.GetUserByIdAsync(userId);
        if (user == null)
        {
            return new Result
            {
                IsSuccess = false,
                ErrorMessage = "Failed to send email."
            };
        }

        string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

        string confirmationLink = $"{baseUrl}/Identity/Account/ConfirmEmail?userId={userId}&token={Uri.EscapeDataString(token)}";


        string subject = "Confirm Your Email";

        string htmlBody = $"<p>Please confirm your email by clicking the link: <a href='{confirmationLink}'>Confirm Email</a></p>";
        string textBody = $"Please confirm your email by clicking the link below:\n{confirmationLink}\n\nIf you did not request this, please ignore this email.";

        // Send email
        bool emailSent = await _emailService.SendEmailAsync(user.Email!, subject, htmlBody, textBody);
        if (!emailSent)
        {
            return new Result
            {
                IsSuccess = false,
                ErrorMessage = "Failed to send email."
            };
        }

        return new Result
        {
            IsSuccess = true
        };
    }

    public async Task<EmailConfirmationResult> ValidateConfirmationEmailAsync(string userId, string token)
    {
        ApplicationUser? user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return EmailConfirmationResult.InvalidToken;
        }

        if (user.EmailConfirmed)
        {
            return EmailConfirmationResult.AlreadyConfirmed;
        }

        var result = await _userManager.ConfirmEmailAsync(user, Uri.UnescapeDataString(token));
        if (result.Succeeded)
        {
            user.EmailConfirmed = true;
            if (await _applicationUserService.UpdateUserAsync(user))
            {
                return EmailConfirmationResult.Success;
            }
        }

        return EmailConfirmationResult.InvalidToken;
    }
}
