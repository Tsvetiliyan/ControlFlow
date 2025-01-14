using System.Linq.Expressions;

namespace ControlFlow.Core.Interfaces.IRepositories;

public interface IBaseRepository<T>:IDisposable where T : class
{
    //Sync Get methods

    /// <summary>
    /// Gets a single entity based on a filter expression.
    /// </summary>
    /// <param name="filter">The filter expression to apply to the entity.</param>
    /// <returns>The first matching entity, or null if no match is found.</returns>
    T? Get(Expression<Func<T, bool>> filter, string? includeProperties = null);

    /// <summary>
    /// Gets a collection of entities based on a filter expression.
    /// </summary>
    /// <param name="filter">The filter expression to apply to the entities.</param>
    /// <returns>An IEnumerable of matching entities.</returns>
    IEnumerable<T> GetRange(Expression<Func<T, bool>> filter, string? includeProperties = null);

    /// <summary>
    /// Gets all entities of type T.
    /// </summary>
    /// <returns>An IEnumerable of all entities of type T.</returns>
    IEnumerable<T> GetAll(string? includeProperties = null);

    //Async Get methods

    /// <summary>
    /// Asynchronously gets a single entity based on a filter expression.
    /// </summary>
    /// <param name="filter">The filter expression to apply to the entity.</param>
    /// <returns>A Task representing the asynchronous operation, with the first matching entity or null if no match is found.</returns>
    Task<T?> GetAsync(Expression<Func<T, bool>> filter, string? includeProperties=null);

    /// <summary>
    /// Asynchronously gets a collection of entities based on a filter expression.
    /// </summary>
    /// <param name="filter">The filter expression to apply to the entities.</param>
    /// <returns>A Task representing the asynchronous operation, with an IEnumerable of matching entities.</returns>
    Task<IEnumerable<T>> GetRangeAsync(Expression<Func<T, bool>> filter, string? includeProperties = null);

    /// <summary>
    /// Asynchronously gets all entities of type T.
    /// </summary>
    /// <returns>A Task representing the asynchronous operation, with an IEnumerable of all entities of type T.</returns>
    Task<IEnumerable<T>> GetAllAsync(string? includeProperties = null);

    //Any Methods

    /// <summary>
    /// Checks if any entities match a given filter.
    /// </summary>
    /// <param name="filter">The filter expression to apply to the entities.</param>
    /// <returns>True if any entities match the filter, otherwise false.</returns>
    bool Any(Expression<Func<T, bool>> filter);

    /// <summary>
    /// Asynchronously checks if any entities match a given filter.
    /// </summary>
    /// <param name="filter">The filter expression to apply to the entities.</param>
    /// <returns>A Task representing the asynchronous operation, with a boolean indicating if any entities match the filter.</returns>
    Task<bool> AnyAsync(Expression<Func<T, bool>> filter);
}


