namespace MicroShop.Common.Data.Context.Mongodb;

public class MongodbTracker
{
    public Func<Task>? MongodbActions { get; set; }

    public MongodbTracker()
    {
    }
}