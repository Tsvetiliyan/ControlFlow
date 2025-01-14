
using ControlFlow.Core.Interfaces.IRepositories;

namespace ControlFlow.Core.Interfaces.IUnitOfWork;

public interface IUnitOfWork : IDisposable
{
    IApplicationUserRepo ApplicationUserRepo { get; set; }
    IUserTaskRepo UserTaskRepo { get; set; }

    int SaveChanges();
}
