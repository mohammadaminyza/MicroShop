namespace MicroShop.Common.Data.Context.Mongodb;

public class BaseMongoCommandDbContext : MongodbContext
{
    public BaseMongoCommandDbContext(MongodbContextOption option) : base(option)
    {
    }

    public BaseMongoCommandDbContext(IMongoClient client, MongodbContextOption option) : base(client, option)
    {
    }
}