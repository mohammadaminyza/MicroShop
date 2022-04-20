using MicroShop.Common.Data.Context.Mongodb;
using MicroShop.Common.Data.UnitOfWork;
using MicroShop.Common.Entities;
using MicroShop.Common.ValueObjects;
using System.Linq.Expressions;
using JetBrains.Annotations;

namespace MicroShop.Common.Data.Repository;

public class MongodbBaseCommandRepository<TEntity, TDbContext> : MongodbUnitOfWork<TDbContext>, ICommandRepository<TEntity> where TEntity : AggregateRoot where TDbContext : MongodbContext
{
    private readonly TDbContext _dbContext;

    public MongodbBaseCommandRepository(TDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public void Insert(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public async Task InsertAsync(TEntity entity)
    {
        await _dbContext.Set<TEntity>().InsertOneAsync(entity);
    }

    public void InsertRange(IEnumerable<TEntity> entities)
    {
        throw new NotImplementedException();
    }

    public async Task InsertRangeAsync(IEnumerable<TEntity> entities)
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

    public void DeleteGraph(long id)
    {
        throw new NotImplementedException();
    }

    public void DeleteRange(IEnumerable<TEntity> entities)
    {
        throw new NotImplementedException();
    }

    public TEntity GetGraph(Id id)
    {
        throw new NotImplementedException();
    }

    public async Task<TEntity> GetGraphAsync(Id businessId)
    {
        throw new NotImplementedException();
    }

    public bool Exists(Expression<Func<TEntity, bool>> expression)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression)
    {
        throw new NotImplementedException();
    }
}