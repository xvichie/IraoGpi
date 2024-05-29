using IraoGpi.Domain.Abstractions.Entity;
using IraoGpi.Domain.Abstractions.Repositories;
using IraoGpi.Domain.Abstractions.UnitOfWork;
using IraoGpi.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IraoGpi.Infrastructure.Repositories;

public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
{
    protected readonly ApplicationDbContext DbContext;

    public BaseRepository(ApplicationDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public IUnitOfWork UnitOfWork => DbContext;

    public IQueryable<TEntity> GetAll()
    {
        return DbContext.Set<TEntity>();
    }

    public async Task<TEntity> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await DbContext.FindAsync<TEntity>(id);
    }

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await DbContext.AddAsync(entity, cancellationToken);
    }

    public void Update(TEntity entity)
    {
        DbContext.Update(entity);
    }
}
