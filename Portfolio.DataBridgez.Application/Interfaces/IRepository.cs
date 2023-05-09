using Portfolio.DataBridgez.Domain.Entities;

namespace Portfolio.DataBridgez.Application.Interfaces;

public interface IRepository<T> : IRepositoryBase<T> where T : Entity
{
    Task<T?> GetByIdAsync(long id, bool trackChanges);
    Task<T?> GetByIdAsync(long id, bool trackChanges, CancellationToken cancellationToken);
    Task<IEnumerable<T>> GetAllAsync(bool trackChanges);
    Task<IEnumerable<T>> GetAllAsync(bool trackChanges, CancellationToken cancellationToken);
}