using ControlFlow.Core.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ControlFlow.Data.Repositories;

public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
{
    protected readonly IdentityAppContext _dbContext;
    protected readonly DbSet<T> _dbSet;

    protected BaseRepository(IdentityAppContext context)
    {
        _dbContext = context;
        _dbSet = _dbContext.Set<T>();
    }

    public T? Get(Expression<Func<T, bool>> filter, string? includeProperties = null)
    {
        IQueryable<T> query = _dbSet;
        query = query.Where(filter);
        if (!string.IsNullOrWhiteSpace(includeProperties))
        {
            foreach (string property in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(property);
            }
        }
        return query.FirstOrDefault(filter);

    }

    public IEnumerable<T> GetRange(Expression<Func<T, bool>> filter, string? includeProperties = null)
    {
        IQueryable<T> query = _dbSet;
        query = query.Where(filter);
        if (!string.IsNullOrWhiteSpace(includeProperties))
        {
            foreach (string property in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(property);
            }
        }
        return query.ToList();
    }

    public IEnumerable<T> GetAll(string? includeProperties = null)
    {
        IQueryable<T> query = _dbSet;
        if (!string.IsNullOrWhiteSpace(includeProperties))
        {
            foreach (string property in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(property);
            }
        }
        return _dbSet.ToList();
    }

    public async Task<T?> GetAsync(Expression<Func<T, bool>> filter, string? includeProperties = null)
    {
        IQueryable<T> query = _dbSet;
        query = query.Where(filter);
        if (!string.IsNullOrWhiteSpace(includeProperties))
        {
            foreach (string property in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(property);
            }
        }
        return await query.FirstOrDefaultAsync(filter);
    }

    public async Task<IEnumerable<T>> GetRangeAsync(Expression<Func<T, bool>> filter, string? includeProperties = null)
    {
        IQueryable<T> query = _dbSet;
        query = query.Where(filter);
        if (!string.IsNullOrWhiteSpace(includeProperties))
        {
            foreach (string property in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(property);
            }
        }
        return await query.ToListAsync();
    }

    public async Task<IEnumerable<T>> GetAllAsync(string? includeProperties = null)
    {
        IQueryable<T> query = _dbSet;
        if (!string.IsNullOrWhiteSpace(includeProperties))
        {
            foreach (string property in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(property);
            }
        }
        return await query.ToListAsync();
    }

    public bool Any(Expression<Func<T, bool>> filter)
    {
        return _dbSet.Any(filter);
    }

    public async Task<bool> AnyAsync(Expression<Func<T, bool>> filter)
    {
        return await _dbSet.AnyAsync(filter);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        _dbContext.Dispose();
    }
}

