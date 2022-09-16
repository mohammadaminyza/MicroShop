using System.Linq.Expressions;
using System.Reflection;

namespace MicroShop.Common.Data.Context.Mongodb;

//Todo Add More Query Methods
//Todo Command Should Save in Bson In Change Tracker
public abstract class MongodbContext
{
    #region Fields

    private readonly IMongoDatabase _mongodbDatabase;
    //TODO Make Extension
    private readonly IBsonDocumentService _bsonDocumentService;
    private readonly MongodbContextOption _contextOption;
    private readonly MongodbChangeTracker _mongodbTracker;
    private IClientSessionHandle? _session;

    #endregion

    #region Ctor

    protected MongodbContext(MongodbContextOption option)
    {
        _contextOption = option;

        var mongodbClient = new MongoClient(option.ConnectionString);

        _mongodbDatabase = mongodbClient.GetDatabase(option.DatabaseName);
        _mongodbTracker = new();
        _bsonDocumentService = new BsonDocumentService();

        AddPropertiesMongodbCollection();
    }

    protected MongodbContext(IMongoClient client, MongodbContextOption option)
    {
        _contextOption = option;
        _mongodbTracker = new();
        _bsonDocumentService = new BsonDocumentService();

        _mongodbDatabase = client.GetDatabase(option.DatabaseName);

        AddPropertiesMongodbCollection();
    }

    #endregion

    #region Collection

    public IMongoCollection<TEntity> GetCollection<TEntity>()
    {
        var collectionName = GetCollectionName<TEntity>();

        return _mongodbDatabase.GetCollection<TEntity>(collectionName);
    }


    #endregion

    #region Transactions

    public IClientSession StartTransaction()
    {
        _session = _mongodbDatabase.Client.StartSession();
        _session.StartTransaction();

        return _session;
    }

    public void AbortTransaction()
    {
        SessionHandlerValidation();
        _session!.AbortTransaction();
    }

    public void CommitTransaction()
    {
        SessionHandlerValidation();
        _session!.CommitTransaction();
    }

    public async Task CommitTransactionAsync()
    {
        SessionHandlerValidation();
        await _session!.CommitTransactionAsync();
    }

    private void SessionHandlerValidation()
    {
        if (_session == null)
            throw new Exception("Need Start Transaction First");
    }

    #endregion

    #region Set

    /// <summary>
    /// Returns DbSet For Commands
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <returns></returns>
    public DbSet<TEntity> Set<TEntity>()
    {
        //Todo fix first
        var entity = this.GetType().GetProperties().FirstOrDefault(p => p.GetType() == typeof(DbSet<TEntity>));
        var dbset = (DbSet<TEntity>?)entity?.GetValue(null);

        if (dbset != null)
        {
            return dbset;
        }
        else
        {
            var collection = GetCollection<TEntity>();
            return new DbSet<TEntity>(this, collection);
        }
    }

    #endregion

    #region Add

    /// <summary>
    /// add entity to changeTracker
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="entity"></param>
    /// <param name="collection">mongodb collection</param>
    internal void Add<TEntity>(TEntity entity, IMongoCollection<TEntity> collection)
    {
        //Insert Action
        var action = () => { collection.InsertOne(entity); };
        var command = () => new Task(action);
        _mongodbTracker.AddCommand(command);
    }

    /// <summary>
    /// add entity to changeTracker async
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="entity"></param>
    /// <param name="collection">mongodb collection</param>
    internal Task AddAsync<TEntity>(TEntity entity, IMongoCollection<TEntity> collection)
    {
        var bson = _bsonDocumentService.EntityToBsonWithEntityConfiguration(entity);

        var command = async () => { await collection.InsertOneAsync(entity); };
        _mongodbTracker.AddCommand(command);

        return Task.CompletedTask;
    }

    /// <summary>
    /// add range entity to changeTracker
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="entities"></param>
    /// <param name="collection">mongodb collection</param>
    internal void AddRange<TEntity>(IEnumerable<TEntity> entities, IMongoCollection<TEntity> collection)
    {
        //Insert Action
        var action = () => { collection.InsertMany(entities); };
        var command = () => new Task(action);
        _mongodbTracker.AddCommand(command);
    }

    /// <summary>
    /// add range entity to changeTracker async
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="entities"></param>
    /// <param name="collection">mongodb collection</param>
    internal Task AddRangeAsync<TEntity>(IEnumerable<TEntity> entities, IMongoCollection<TEntity> collection)
    {
        var command = async () => { await collection.InsertManyAsync(entities); };
        _mongodbTracker.AddCommand(command);

        return Task.CompletedTask;
    }

    #endregion

    #region Update

    /// <summary>
    /// replace entity with other entity that you want
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="entity">replace entity value</param>
    /// <param name="expression">filter witch entity do you want to replace</param>
    /// <param name="collection">mongodb collection</param>
    internal void Update<TEntity>(TEntity entity, Expression<Func<TEntity, bool>> expression, IMongoCollection<TEntity> collection)
    {
        var action = () => { collection.ReplaceOne(expression, entity); };
        var command = () => new Task(action);
        _mongodbTracker.AddCommand(command);
    }

    /// <summary>
    /// replace entity with other entity that you want async
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="entity">replace entity value</param>
    /// <param name="expression">filter expression witch entity do you want to replace</param>
    /// <param name="collection">mongodb collection</param>
    internal Task UpdateAsync<TEntity>(TEntity entity, Expression<Func<TEntity, bool>> expression, IMongoCollection<TEntity> collection)
    {
        var command = async () => { await collection.ReplaceOneAsync(expression, entity); };
        _mongodbTracker.AddCommand(command);

        return Task.CompletedTask;
    }

    #endregion

    #region Remove

    /// <summary>
    /// remove entity from changeTracker
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="expression">filter expression of entity you wanna delete</param>
    /// <param name="collection">mongodb collection</param>
    internal void Remove<TEntity>(Expression<Func<TEntity, bool>> expression, IMongoCollection<TEntity> collection)
    {
        var action = () => { collection.DeleteOne(expression); };
        var command = () => new Task(action);
        _mongodbTracker.AddCommand(command);
    }

    /// <summary>
    /// remove entity from changeTracker async
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="expression">filter expression of entity you wanna delete</param>
    /// <param name="collection">mongodb collection</param>
    internal Task RemoveAsync<TEntity>(Expression<Func<TEntity, bool>> expression, IMongoCollection<TEntity> collection)
    {
        var command = async () => { await collection.DeleteOneAsync(expression); };
        _mongodbTracker.AddCommand(command);

        return Task.CompletedTask;
    }

    /// <summary>
    /// remove range entity from changeTracker
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="expression">filter expression of entity you wanna delete</param>
    /// <param name="collection">mongodb collection</param>
    internal void RemoveRange<TEntity>(Expression<Func<TEntity, bool>> expression, IMongoCollection<TEntity> collection)
    {
        var action = () => { collection.DeleteMany(expression); };
        var command = () => new Task(action);
        _mongodbTracker.AddCommand(command);
    }

    /// <summary>
    /// remove range entity from changeTracker async
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="expression">filter expression of entity you wanna delete</param>
    /// <param name="collection">mongodb collection</param>
    internal Task RemoveRangeAsync<TEntity>(Expression<Func<TEntity, bool>> expression, IMongoCollection<TEntity> collection)
    {
        var command = async () => { await collection.DeleteManyAsync(expression); };
        _mongodbTracker.AddCommand(command);

        return Task.CompletedTask;
    }

    #endregion

    #region Any

    /// <summary>
    /// Check exists of object with expression filter
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="expression">expression that will check database for it</param>
    /// <param name="collection">mongodb collection</param>
    /// <returns>exist or not</returns>
    internal bool Any<TEntity>(Expression<Func<TEntity, bool>> expression, IMongoCollection<TEntity> collection)
    {
        return collection.FindSync(expression).Any();
    }

    /// <summary>
    /// Check exists of object with expression filter async
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="expression">expression that will check database for it</param>
    /// <param name="collection">mongodb collection</param>
    /// <returns>exist or not</returns>

    internal async Task<bool> AnyAsync<TEntity>(Expression<Func<TEntity, bool>> expression, IMongoCollection<TEntity> collection)
    {
        return await (await collection.FindAsync(expression)).AnyAsync();
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

    #region Private Methods

    /// <summary>
    /// Set DbSet value for context properties
    /// </summary>
    private void AddPropertiesMongodbCollection()
    {
        var properties = this.GetType()
            .GetProperties()
            .Where(p => p.CanWrite)
            .ToList();

        foreach (var property in properties)
        {
            var propertyEntityType = GetPropertyMongodbCollectionEntityType(property);

            if (propertyEntityType == null)
                continue;

            var mongodbCollection = GetMongodbCollection(propertyEntityType);

            property.SetValue(this, mongodbCollection);
        }
    }

    private Type? GetPropertyMongodbCollectionEntityType(PropertyInfo? property)
    {
        var propertyEntityType = property?.PropertyType.GetGenericArguments().FirstOrDefault();
        return propertyEntityType;
    }

    private string? GetCollectionName<TEntity>()
    {
        return this.GetType().GetProperties()?.FirstOrDefault(p => p.PropertyType == typeof(DbSet<TEntity>))?.Name;
    }
    private string? GetCollectionName(Type type)
    {
        return this.GetType().GetProperties()?.FirstOrDefault(p => p.PropertyType == typeof(DbSet<>).MakeGenericType(type))?.Name;
    }

    private object? GetMongodbCollection(Type type)
    {
        return this.GetType().GetMethod("GetMongodbCollection")?.MakeGenericMethod(type).Invoke(this, null);
    }

    #endregion
}