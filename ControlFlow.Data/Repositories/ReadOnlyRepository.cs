using ControlFlow.Core.Interfaces.IRepositories;

namespace ControlFlow.Data.Repositories;

public class ReadOnlyRepository<T> : BaseRepository<T>, IReadOnlyRepository<T> where T : class
{
    public ReadOnlyRepository(IdentityAppContext context) : base(context)
    {

    }
}
