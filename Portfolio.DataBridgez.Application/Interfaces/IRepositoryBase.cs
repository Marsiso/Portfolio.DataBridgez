using System.Linq.Expressions;
using Portfolio.DataBridgez.Domain.Entities;

namespace Portfolio.DataBridgez.Application.Interfaces;

public interface IRepositoryBase<T> : IDisposable where T : Entity
{
    IQueryable<T> FindAll(bool trackChanges);
    IQueryable<T> FindAll(bool trackChanges, CancellationToken cancellationToken);
    IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges);

    IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges,
        CancellationToken cancellationToken);

    void Create(T resource);
    Task CreateAsync(T resource, CancellationToken cancellationToken);
    void Update(T resource);
    void Update(T resource, CancellationToken cancellationToken);
    void Delete(T resource);
    void Delete(T resource, CancellationToken cancellationToken);
    void SaveChanges();
    Task SaveChangesAsync();
    Task SaveChangesAsync(CancellationToken cancellationToken);
}