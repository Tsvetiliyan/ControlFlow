using ControlFlow.Core.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace ControlFlow.Data.Repositories;

public class Repository<T> : BaseRepository<T>, IRepository<T> where T : class
{
    public Repository(IdentityAppContext context) : base(context)
    {

    }

    public void Add(T entity)
    {
        _dbSet.Add(entity);
    }
    public void AddRange(IEnumerable<T> entities)
    {
        _dbSet.AddRange(entities);
    }
    public void Remove(T entity)
    {
        _dbSet.Remove(entity);
    }
    public void RemoveRange(IEnumerable<T> entities)
    {
        _dbSet.RemoveRange(entities);
    }
    public void Update(T entity)
    {
        _dbSet.Update(entity);
    }
}
