using ControlFlow.Core.Enums;
using Microsoft.AspNetCore.Identity;

namespace ControlFlow.Core.Entities;

public class ApplicationUser : IdentityUser
{
    public SubscriptionType SubscriptionPlan { get; set; }
}
