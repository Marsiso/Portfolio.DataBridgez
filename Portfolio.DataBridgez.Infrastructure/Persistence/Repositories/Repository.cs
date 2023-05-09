using Microsoft.EntityFrameworkCore;
using Portfolio.DataBridgez.Application.Interfaces;

namespace Portfolio.DataBridgez.Infrastructure.Persistence.Repositories;

public sealed class Repository<T> : RepositoryBase<T>, IRepository<T> where T : Entity
{
    private bool _disposed;

    public Repository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<T?> GetByIdAsync(long id, bool trackChanges)
    {
        ThrowIfDisposed();
        return await FindByCondition(u => u.Id == id, trackChanges).SingleOrDefaultAsync();
    }

    public async Task<T?> GetByIdAsync(long id, bool trackChanges, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();
        return await FindByCondition(u => u.Id == id, trackChanges, cancellationToken)
            .SingleOrDefaultAsync(cancellationToken: cancellationToken);
    }

    public async Task<IEnumerable<T>> GetAllAsync(bool trackChanges)
    {
        ThrowIfDisposed();
        return await FindAll(trackChanges).ToListAsync();
    }

    public async Task<IEnumerable<T>> GetAllAsync(bool trackChanges, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();
        return await FindAll(trackChanges, cancellationToken).ToListAsync(cancellationToken: cancellationToken);
    }

    protected override void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
            }

            _disposed = true;
        }

        base.Dispose(disposing);
    }
    
    private new void ThrowIfDisposed()
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(GetType().Name);
        }
    }
    
    ~Repository() => this.Dispose(false);
}