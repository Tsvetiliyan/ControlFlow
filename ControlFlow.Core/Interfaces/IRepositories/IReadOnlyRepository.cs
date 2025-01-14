namespace ControlFlow.Core.Interfaces.IRepositories;

public interface IReadOnlyRepository<T> : IBaseRepository<T> where T : class
{
    // Inherits methods from IBaseRepository. 
    // No additional methods are needed because this interface is for read-only operations.
}
