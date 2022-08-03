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
        _dbContext.Set<TEntity>().InsertOne(entity);
    }

    public async Task InsertAsync(TEntity entity)
    {
        await _dbContext.Set<TEntity>().InsertOneAsync(entity);
    }

    public void InsertRange(IEnumerable<TEntity> entities)
    {
        _dbContext.Set<TEntity>().InsertMany(entities);
    }

    public async Task InsertRangeAsync(IEnumerable<TEntity> entities)
    {
        await _dbContext.Set<TEntity>().InsertManyAsync(entities);
    }

    public void Delete(Id id)
    {
        _dbContext.Set<TEntity>().DeleteOne(e => e.Id.Equals(id));
    }

    public void Delete(TEntity entity)
    {
        _dbContext.Set<TEntity>().DeleteOne(e => e.Id.Equals(entity.Id));
    }

    public void DeleteGraph(long id)
    {
        throw new NotSupportedException();
    }

    public void DeleteRange(IEnumerable<TEntity> entities)
    {
        _dbContext.Set<TEntity>().DeleteMany(e => entities.Any(a => a.Id.Equals(e.Id)));
    }

    public TEntity GetGraph(Id id)
    {
        return _dbContext.Set<TEntity>().FindSync(e => e.Id.Equals(id)).FirstOrDefault();
    }

    public async Task<TEntity> GetGraphAsync(Id id)
    {
        return await (await _dbContext.Set<TEntity>().FindAsync(e => e.Id.Equals(id))).FirstOrDefaultAsync();
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