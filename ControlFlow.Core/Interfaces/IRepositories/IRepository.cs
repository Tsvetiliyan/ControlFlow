namespace ControlFlow.Core.Interfaces.IRepositories;

public interface IRepository<T> : IBaseRepository<T> where T : class
{
    /// <summary>
    /// Adds a new entity to the repository.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    void Add(T entity);

    /// <summary>
    /// Adds a range of entities to the repository.
    /// </summary>
    /// <param name="entities">The collection of entities to add.</param>
    void AddRange(IEnumerable<T> entities);

    /// <summary>
    /// Removes a specified entity from the repository.
    /// </summary>
    /// <param name="entity">The entity to remove.</param>
    void Remove(T entity);

    /// <summary>
    /// Removes a range of entities from the repository.
    /// </summary>
    /// <param name="entities">The collection of entities to remove.</param>
    void RemoveRange(IEnumerable<T> entities);

    /// <summary>
    /// Updates an existing entity in the repository.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    void Update(T entity);
}
