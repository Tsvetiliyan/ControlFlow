using ControlFlow.Core.Enums;

namespace ControlFlow.Core.Dtos;

public class UserDto
{
    public UserDto(int id, string username, string email, string firstName, string lastName, SubscriptionType subscriptionPlan)
    {
        Id = id;
        Username = username;
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        SubscriptionPlan = subscriptionPlan;
    }

    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public SubscriptionType SubscriptionPlan { get; set; }
}
