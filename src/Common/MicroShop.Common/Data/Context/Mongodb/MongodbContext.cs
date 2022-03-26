using System.Reflection;

namespace MicroShop.Common.Data.Context.Mongodb;

public abstract class MongodbContext
{
    private readonly IMongoDatabase _mongodbDatabase;
    private readonly MongodbContextOption _contextOption;

    protected MongodbContext(MongodbContextOption option)
    {
        var mongodbClient = new MongoClient(option.ConnectionString);
        var databaseName = MongoUrl.Create(option.ConnectionString).DatabaseName;

        _mongodbDatabase = mongodbClient.GetDatabase(databaseName);
        _contextOption = option;

        AddPropertiesMongodbCollection();
    }

    protected MongodbContext(IMongoClient client, MongodbContextOption option)
    {
        var databaseName = MongoUrl.Create(option.ConnectionString).DatabaseName;

        _mongodbDatabase = client.GetDatabase(databaseName);
        _contextOption = option;

        AddPropertiesMongodbCollection();
    }

    public IMongoCollection<TEntity> Set<TEntity>()
    {
        var collectionName = GetCollectionName<TEntity>();

        return _mongodbDatabase.GetCollection<TEntity>(collectionName);
    }

    private void AddPropertiesMongodbCollection()
    {
        var properties = this.GetType().GetProperties().ToList();

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
        return this.GetType().GetProperties()?.FirstOrDefault(p => p.PropertyType == typeof(IMongoCollection<TEntity>))?.Name;
    }
    private string? GetCollectionName(Type type)
    {
        return this.GetType().GetProperties()?.FirstOrDefault(p => p.PropertyType == typeof(IMongoCollection<>).MakeGenericType(type))?.Name;
    }

    private object? GetMongodbCollection(Type type)
    {
        return this.GetType().GetMethod("GetMongodbCollection")?.MakeGenericMethod(type).Invoke(this, null);
    }
}