using System.Linq.Expressions;

namespace MicroShop.Common.Data.Context.Mongodb;

public class DbSet<TEntity>
{
    private readonly IMongoCollection<TEntity> _collection;
    private readonly MongodbContext _context;
    protected IMongoCollection<TEntity> Collection => _collection;

    public DbSet(MongodbContext context, IMongoCollection<TEntity> collection)
    {
        _context = context;
        _collection = collection;
    }

    #region Add

    /// <summary>
    /// add entity to changeTracker
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public void Add(TEntity entity)
    {
        _context.Add(entity, Collection);
    }

    /// <summary>
    /// add entity to changeTracker async
    /// </summary>
    /// <param name="entity"></param>
    public async Task AddAsync(TEntity entity)
    {
        await _context.AddAsync(entity, Collection);
    }

    /// <summary>
    /// add range entities to changeTracker
    /// </summary>
    /// <param name="entities"></param>
    /// <param name="collection">mongodb collection</param>
    public void AddRange(IEnumerable<TEntity> entities)
    {
        _context.AddRange(entities, Collection);
    }

    /// <summary>
    /// add range entities to changeTracker async
    /// </summary>
    /// <param name="entities"></param>
    /// <param name="collection">mongodb collection</param>
    public async Task AddRangeAsync(IEnumerable<TEntity> entities)
    {
        await _context.AddRangeAsync(entities, Collection);
    }

    #endregion

    #region Update

    /// <summary>
    /// replace entity with other entity that you want
    /// </summary>
    /// <param name="entity">replace entity value</param>
    /// <param name="expression">filter witch entity do you want to replace</param>
    public void Update(TEntity entity, Expression<Func<TEntity, bool>> expression)
    {
        _context.Update(entity, expression, Collection);
    }

    /// <summary>
    /// replace entity with other entity that you want async
    /// </summary>
    /// <param name="entity">replace entity value</param>
    /// <param name="expression">filter witch entity do you want to replace</param>
    public async Task UpdateAsync(TEntity entity, Expression<Func<TEntity, bool>> expression)
    {
        await _context.UpdateAsync(entity, expression, Collection);
    }

    #endregion

    #region Remove

    /// <summary>
    /// remove entity from changeTracker
    /// </summary>
    /// <param name="expression">filter expression of entity you wanna delete</param>
    public void Remove(Expression<Func<TEntity, bool>> expression)
    {
        _context.Remove(expression, Collection);
    }

    /// <summary>
    /// remove entity from changeTracker async
    /// </summary>
    /// <param name="expression">filter of entity you wanna delete</param>
    public async Task RemoveAsync(Expression<Func<TEntity, bool>> expression)
    {
        await _context.RemoveAsync(expression, Collection);
    }

    /// <summary>
    /// remove range entities from changeTracker
    /// </summary>
    /// <param name="expression">filter expression of entity you wanna delete</param>
    public void RemoveRange(Expression<Func<TEntity, bool>> expression)
    {
        _context.RemoveRange(expression, Collection);
    }

    /// <summary>
    /// remove range entities from changeTracker async
    /// </summary>
    /// <param name="expression">filter expression of entity you wanna delete</param>
    public async Task RemoveRangeAsync(Expression<Func<TEntity, bool>> expression)
    {
        await _context.RemoveRangeAsync(expression, Collection);
    }

    #endregion

    #region Any

    /// <summary>
    /// check exists of object with expression filter
    /// </summary>
    /// <param name="expression">expression that will check database for it</param>
    /// <returns>exist or not</returns>
    public bool Any(Expression<Func<TEntity, bool>> expression)
    {
        return _context.Any(expression, Collection);
    }

    /// <summary>
    /// Check exists of object with expression filter async
    /// </summary>
    /// <param name="expression">expression that will check database for it</param>
    /// <returns>exist or not</returns>
    internal async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression)
    {
        return await _context.AnyAsync(expression, Collection);
    }

    #endregion

}