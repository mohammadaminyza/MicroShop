using MongoDB.Bson;

namespace MicroShop.Common.Data.Context.Mongodb;

public interface IBsonDocumentService
{
    BsonDocument EntityToBsonWithEntityConfiguration<TEntity>(TEntity entity);
}