using ControlFlow.Core.Entities;
using ControlFlow.Core.Enums;
using Microsoft.AspNetCore.Identity;

namespace ControlFlow.Core;

public interface IApplicationUserService
{
    Task<ApplicationUser?> GetUserByIdAsync(string userId);

    Task<ApplicationUser?> GetUserByEmailAsync(string email);

    Task<bool> UserExistsAsync(string userId);

    Task<bool> UpdateUserAsync(ApplicationUser user);

    Task<bool> DeleteUserAsync(string userId);
    List<ApplicationUser> GetAllUsers();
    List<ApplicationUser> GetUsersBySubscriptionPlan(SubscriptionType subscriptionType);
}
