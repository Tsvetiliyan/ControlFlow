// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControlFlow.Core.Entities;
using ControlFlow.Core.Enums.EmailEnums;
using ControlFlow.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Org.BouncyCastle.Pqc.Asn1;

namespace ControlFlow.Areas.Identity.Pages.Account
{
    public class ConfirmEmailModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailConfirmationService _emailConfirmationService;

        public ConfirmEmailModel(UserManager<ApplicationUser> userManager, IEmailConfirmationService emailConfirmationService)
        {
            _userManager = userManager;
            _emailConfirmationService = emailConfirmationService;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }
        public async Task<IActionResult> OnGetAsync(string userId, string token)
        {
            if (userId == null || token == null)
            {
                StatusMessage = "Error: Invalid confirmation link or user is not found.";
                return RedirectToPage("/Index");
            }

            var user = await _userManager.FindByIdAsync(userId);

            EmailConfirmationResult confirmationResult = await _emailConfirmationService.ValidateConfirmationEmailAsync(userId, token);

            switch (confirmationResult)
            {
                case EmailConfirmationResult.Success:
                    StatusMessage = "Thank you! Your email has been confirmed.";
                    return RedirectToPage("/Index");

                case EmailConfirmationResult.AlreadyConfirmed:
                    StatusMessage = "Your email was already confirmed. You can proceed to login.";
                    return RedirectToPage("/Account/Login");

                default:
                    StatusMessage = "Error: Invalid confirmation link or other error occured during the process.";
                    return RedirectToPage("/Index");
            }
        }
    }
}
