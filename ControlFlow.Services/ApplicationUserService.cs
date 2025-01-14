using ControlFlow.Core;
using ControlFlow.Core.Entities;
using ControlFlow.Core.Enums;
using Microsoft.AspNetCore.Identity;

namespace ControlFlow.Services;

public class ApplicationUserService : IApplicationUserService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public ApplicationUserService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<ApplicationUser?> GetUserByIdAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        return user;
    }

    public async Task<ApplicationUser?> GetUserByEmailAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        return user;
    }

    public async Task<bool> UserExistsAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        return user != null;
    }

    public async Task<bool> UpdateUserAsync(ApplicationUser user)
    {
        var result = await _userManager.UpdateAsync(user);
        return result.Succeeded;
    }

    public async Task<bool> DeleteUserAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return false;

        var result = await _userManager.DeleteAsync(user);
        return result.Succeeded;
    }

    public List<ApplicationUser> GetAllUsers()
    {
        var user = _userManager.Users.ToList();
        return user;
    }
    public List<ApplicationUser> GetUsersBySubscriptionPlan(SubscriptionType subscriptionType)
    {
        var users = _userManager.Users.Where(u => u.SubscriptionPlan == subscriptionType).ToList();
        return users;
    }
}
