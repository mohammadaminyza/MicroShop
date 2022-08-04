using MicroShop.Common.Data.Context.Mongodb;
using MicroShop.Common.Data.UnitOfWork;
using MicroShop.Common.Entities;
using MicroShop.Common.ValueObjects;
using System.Linq.Expressions;

namespace MicroShop.Common.Data.Repository;

public class MongodbBaseCommandRepository<TEntity, TDbContext> : MongodbUnitOfWork<TDbContext>, ICommandRepository<TEntity>
    where TEntity : AggregateRoot
    where TDbContext : BaseMongoCommandDbContext
{
    private readonly TDbContext _dbContext;

    public MongodbBaseCommandRepository(TDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public void Insert(TEntity entity)
    {
        _dbContext.Set<TEntity>().Add(entity);
    }

    public async Task InsertAsync(TEntity entity)
    {
        await _dbContext.Set<TEntity>().AddAsync(entity);
    }

    public void InsertRange(IEnumerable<TEntity> entities)
    {
        _dbContext.Set<TEntity>().AddRange(entities);
    }

    public async Task InsertRangeAsync(IEnumerable<TEntity> entities)
    {
        await _dbContext.Set<TEntity>().AddRangeAsync(entities);
    }

    public void Delete(Id id)
    {
        _dbContext.Set<TEntity>().Remove(e => e.Id.Equals(id));
    }

    public void Delete(TEntity entity)
    {
        _dbContext.Set<TEntity>().Remove(e => e.Id.Equals(entity.Id));
    }

    public void DeleteGraph(Id id)
    {
        Delete(id);
    }

    public void DeleteRange(IEnumerable<TEntity> entities)
    {
        _dbContext.Set<TEntity>().RemoveRange(e => entities.Any(a => a.Id.Equals(e.Id)));
    }

    public TEntity GetGraph(Id id)
    {
        throw new NotImplementedException();
    }

    public Task<TEntity> GetGraphAsync(Id id)
    {
        throw new NotImplementedException();
    }

    public bool Exists(Expression<Func<TEntity, bool>> expression)
    {
        return _dbContext.Set<TEntity>().Any(expression);
    }

    public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression)
    {
        return await _dbContext.Set<TEntity>().AnyAsync(expression);
    }
}