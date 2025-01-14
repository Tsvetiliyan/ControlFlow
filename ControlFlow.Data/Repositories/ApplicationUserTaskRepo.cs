using ControlFlow.Core.Entities;
using ControlFlow.Core.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace ControlFlow.Data.Repositories;

public class ApplicationUserTaskRepo : Repository<ApplicationUser>, IApplicationUserRepo
{
    public ApplicationUserTaskRepo(IdentityAppContext context) : base(context)
    {
    }

    public async Task<ApplicationUser?> GetUserByEmailAsync(string email)
    {
        return await _dbContext.Set<ApplicationUser>().FirstOrDefaultAsync(x => x.Email == email);
    }
}
