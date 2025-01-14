using ControlFlow.Core.Entities;

namespace ControlFlow.Core.Interfaces.IRepositories;

public interface IApplicationUserRepo : IRepository<ApplicationUser>
{
    Task<ApplicationUser?> GetUserByEmailAsync(string email);
}
