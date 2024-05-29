using IraoGpi.Domain.Abstractions.Entity;
using IraoGpi.Domain.Abstractions.UnitOfWork;

namespace IraoGpi.Domain.Abstractions.Repositories;

public interface IRepository<TEntity> where TEntity : IEntity
{
    IUnitOfWork UnitOfWork { get; }

    IQueryable<TEntity> GetAll();

    Task<TEntity> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    void Update(TEntity entity);
}

