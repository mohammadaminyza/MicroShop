using MicroShop.Common.ValueObjects;
using System.Linq.Expressions;
using MicroShop.Common.Data.Context;
using MicroShop.Common.Data.Context.Mongodb;
using MicroShop.Common.Entities;

namespace MicroShop.Common.Data.Repository;

public class MongodbBaseCommandRepository<TEntity, TDbContext> : ICommandRepository<TEntity> where TEntity : AggregateRoot where TDbContext : MongodbContext
{
    private readonly TDbContext _dbContext;

    public MongodbBaseCommandRepository(TDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void BeginTransaction()
    {
        throw new NotSupportedException();
    }

    public int Commit()
    {
        throw new NotSupportedException();
    }

    public Task<int> CommitAsync()
    {
        throw new NotSupportedException();
    }

    public void CommitTransaction()
    {
        throw new NotSupportedException();
    }

    public void Delete(Id id)
    {
        throw new NotSupportedException();
    }

    public void Delete(TEntity entity)
    {
        throw new NotSupportedException();
    }

    public void DeleteGraph(long id)
    {
        throw new NotSupportedException();
    }

    public void DeleteRange(IEnumerable<TEntity> entities)
    {
        throw new NotSupportedException();
    }

    public bool Exists(Expression<Func<TEntity, bool>> expression)
    {
        throw new NotSupportedException();
    }

    public Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression)
    {
        throw new NotSupportedException();
    }

    public TEntity GetGraph(Id id)
    {
        throw new NotSupportedException();
    }

    public Task<TEntity> GetGraphAsync(Id businessId)
    {
        throw new NotSupportedException();
    }

    public void Insert(TEntity entity)
    {
        throw new NotSupportedException();
    }

    public async Task InsertAsync(TEntity entity)
    {
        await _dbContext.Set<TEntity>().InsertOneAsync(entity);
    }

    public void InsertRange(IEnumerable<TEntity> entities)
    {
        throw new NotSupportedException();
    }

    public Task InsertRangeAsync(IEnumerable<TEntity> entities)
    {
        throw new NotSupportedException();
    }

    public void RollbackTransaction()
    {
        throw new NotSupportedException();
    }
}