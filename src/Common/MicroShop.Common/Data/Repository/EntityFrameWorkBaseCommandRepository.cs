using MicroShop.Common.Data.UnitOfWork;
using MicroShop.Common.Entities;
using MicroShop.Common.ValueObjects;
using System.Linq.Expressions;

namespace MicroShop.Common.Data.Repository;

public class EntityFrameWorkBaseCommandRepository<TEntity, TDbContext> : ICommandRepository<TEntity>,
    IUnitOfWork where TEntity : AggregateRoot
{
    public readonly TDbContext _dbContext;

    public EntityFrameWorkBaseCommandRepository(TDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void BeginTransaction()
    {
        throw new NotImplementedException();
    }

    public int Commit()
    {
        throw new NotImplementedException();
    }

    public Task<int> CommitAsync()
    {
        throw new NotImplementedException();
    }

    public void CommitTransaction()
    {
        throw new NotImplementedException();
    }

    public void Delete(Id id)
    {
        throw new NotImplementedException();
    }

    public void Delete(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public void DeleteGraph(Id id)
    {
        throw new NotImplementedException();
    }

    public void DeleteRange(IEnumerable<TEntity> entities)
    {
        throw new NotImplementedException();
    }

    public bool Exists(Expression<Func<TEntity, bool>> expression)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression)
    {
        throw new NotImplementedException();
    }

    public TEntity GetGraph(Id id)
    {
        throw new NotImplementedException();
    }

    public Task<TEntity> GetGraphAsync(Id businessId)
    {
        throw new NotImplementedException();
    }

    public void Insert(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public Task InsertAsync(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public void InsertRange(IEnumerable<TEntity> entities)
    {
        throw new NotImplementedException();
    }

    public Task InsertRangeAsync(IEnumerable<TEntity> entities)
    {
        throw new NotImplementedException();
    }

    public void RollbackTransaction()
    {
        throw new NotImplementedException();
    }
}