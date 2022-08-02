using System.ComponentModel;

namespace MicroShop.Common.Data.Context.Mongodb;

public class BaseMongoCommandDbContext : MongodbContext
{
    #region Fields

    private readonly MongodbTracker _mongodbTracker;

    #endregion

    #region Ctor

    public BaseMongoCommandDbContext(MongodbContextOption option) : base(option)
    {
        _mongodbTracker = new MongodbTracker();
    }

    public BaseMongoCommandDbContext(IMongoClient client, MongodbContextOption option) : base(client, option)
    {
        _mongodbTracker = new MongodbTracker();
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