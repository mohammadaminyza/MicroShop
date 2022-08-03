namespace MicroShop.Common.Data.Context.Mongodb;

public class BaseMongoCommandDbContext : MongodbContext
{
    #region Fields

    private readonly MongodbChangeTracker _mongodbTracker;

    #endregion

    #region Ctor

    public BaseMongoCommandDbContext(MongodbContextOption option) : base(option)
    {
        _mongodbTracker = new();
    }

    public BaseMongoCommandDbContext(IMongoClient client, MongodbContextOption option) : base(client, option)
    {
        _mongodbTracker = new();
    }

    #endregion

    #region Add

    /// <summary>
    /// Add Entity To ChangeTracker
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="entity"></param>
    /// <returns></returns>
    public void Add<TEntity>(TEntity entity)
    {
        var collection = Set<TEntity>();
        //Insert Action
        var action = () => { collection.InsertOne(entity); };
        var command = () => new Task(action);
        _mongodbTracker.AddCommand(command);
    }

    /// <summary>
    /// Add Entity To ChangeTracker Async
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="entity"></param>
    /// <returns></returns>
    public Task AddAsync<TEntity>(TEntity entity)
    {
        var collection = Set<TEntity>();
        var command = async () => { await collection.InsertOneAsync(entity); };
        _mongodbTracker.AddCommand(command);

        return Task.CompletedTask;
    }

    #endregion

    #region Update

    /// <summary>
    /// Replace Entity With Other Entity That You Want
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="entity">replace entity value</param>
    /// <param name="filter">filter witch entity do you want to replace</param>
    public void Update<TEntity>(TEntity entity, FilterDefinition<TEntity> filter)
    {
        var collection = Set<TEntity>();
        var action = () => { collection.ReplaceOne(filter, entity); };
        var command = () => new Task(action);
        _mongodbTracker.AddCommand(command);
    }

    /// <summary>
    /// Replace Entity With Other Entity That You Want Async
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="entity">replace entity value</param>
    /// <param name="filter">filter witch entity do you want to replace</param>
    public Task UpdateAsync<TEntity>(TEntity entity, FilterDefinition<TEntity> filter)
    {
        var collection = Set<TEntity>();
        var command = async () => { await collection.ReplaceOneAsync(filter, entity); };
        _mongodbTracker.AddCommand(command);

        return Task.CompletedTask;
    }

    #endregion

    #region Remove

    /// <summary>
    /// Remove Entity From ChangeTracker
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="filter">filter of entity you wanna delete</param>
    public void Remove<TEntity>(FilterDefinition<TEntity> filter)
    {
        var collection = Set<TEntity>();
        var action = () => { collection.DeleteOne(filter); };
        var command = () => new Task(action);
        _mongodbTracker.AddCommand(command);
    }

    /// <summary>
    /// Remove Entity From ChangeTracker Async
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="filter">filter of entity you wanna delete</param>
    public Task RemoveAsync<TEntity>(FilterDefinition<TEntity> filter)
    {
        var collection = Set<TEntity>();
        var command = async () => { await collection.DeleteOneAsync(filter); };
        _mongodbTracker.AddCommand(command);

        return Task.CompletedTask;
    }

    #endregion

    #region Savechange

    /// <summary>
    /// Apply Changes In Change Tracker
    /// </summary>
    /// <returns>Count Of Operations</returns>
    public int SaveChanges()
    {
        using (StartTransaction())
        {
            Task.WhenAll(_mongodbTracker.ExcuteCommands());

            CommitTransaction();
        }

        return _mongodbTracker.MongodbCommands.Count();
    }

    /// <summary>
    /// Apply Changes In Change Tracker Async
    /// </summary>
    /// <returns>Count Of Operations</returns>
    public async Task<int> SaveChangesAsync()
    {
        using (StartTransaction())
        {
            await Task.WhenAll(_mongodbTracker.ExcuteCommands());
            await CommitTransactionAsync();
        }

        return _mongodbTracker.MongodbCommands.Count();
    }

    #endregion
}