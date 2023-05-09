using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Portfolio.DataBridgez.Application.Interfaces;

namespace Portfolio.DataBridgez.Infrastructure.Persistence.Repositories;

public class RepositoryBase<T> : IRepositoryBase<T> where T : Entity
{
    private bool _disposed;
    private readonly AppDbContext _dbContext;

    protected RepositoryBase(AppDbContext dbContext)
    {
        ThrowIfDisposed();
        _dbContext = dbContext;
    }

    ~RepositoryBase() => Dispose(false);

    public IQueryable<T> FindAll(bool trackChanges)
    {
        ThrowIfDisposed();
        return trackChanges
            ? _dbContext.Set<T>().AsTracking()
            : _dbContext.Set<T>().AsNoTracking();
    }

    public IQueryable<T> FindAll(bool trackChanges, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();
        return trackChanges
            ? _dbContext.Set<T>().AsTracking()
            : _dbContext.Set<T>().AsNoTracking();
    }

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges)
    {
        ThrowIfDisposed();
        ArgumentNullException.ThrowIfNull(expression);
        var queryResult = _dbContext.Set<T>().Where(expression);
        return trackChanges
            ? queryResult.AsTracking()
            : queryResult.AsNoTracking();
    }

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();
        ArgumentNullException.ThrowIfNull(expression);
        var queryResult = _dbContext.Set<T>().Where(expression);
        return trackChanges
            ? queryResult.AsTracking()
            : queryResult.AsNoTracking();
    }

    public void Create(T resource)
    {
        ThrowIfDisposed();
        ArgumentNullException.ThrowIfNull(resource);
        _dbContext.Set<T>().Add(resource);
    }

    public async Task CreateAsync(T resource, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();
        ArgumentNullException.ThrowIfNull(resource);
        await _dbContext.Set<T>().AddAsync(resource, cancellationToken);
    }

    public void Update(T resource)
    {
        ThrowIfDisposed();
        ArgumentNullException.ThrowIfNull(resource);
        _dbContext.Set<T>().Update(resource);
    }

    public void Update(T resource, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();
        ArgumentNullException.ThrowIfNull(resource);
        _dbContext.Set<T>().Update(resource);
    }

    public void Delete(T resource)
    {
        ThrowIfDisposed();
        ArgumentNullException.ThrowIfNull(resource);
        _dbContext.Set<T>().Remove(resource);
    }

    public void Delete(T resource, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();
        ArgumentNullException.ThrowIfNull(resource);
        _dbContext.Set<T>().Remove(resource);
    }

    public void SaveChanges()
    {
        ThrowIfDisposed();
        _dbContext.SaveChanges();
    }

    public async Task SaveChangesAsync()
    {
        ThrowIfDisposed();
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }

            _disposed = true;
        }
    }

    protected void ThrowIfDisposed()
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(GetType().Name);
        }
    }
}