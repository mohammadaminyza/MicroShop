using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using System.Reflection;

namespace MicroShop.Common.Data.Context.Mongodb;

public class BsonDocumentService : IBsonDocumentService
{
    public BsonDocument EntityToBsonWithEntityConfiguration<TEntity>(TEntity entity)
    {
        var entityProperties = entity?.GetType()?.GetProperties()?.ToList() ?? Enumerable.Empty<PropertyInfo>().ToList();

        var bsonObject = BsonDocument.Create(entity);

        foreach (var property in entityProperties)
        {
            var bsonElement = bsonObject.FirstOrDefault(c => c.Name == property.Name);
            var modelType = property.GetType();
            ValueConverter<>
        }

        return bsonObject;
    }
}