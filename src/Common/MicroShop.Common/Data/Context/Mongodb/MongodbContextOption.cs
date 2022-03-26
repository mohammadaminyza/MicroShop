namespace MicroShop.Common.Data.Context.Mongodb;

public class MongodbContextOption
{
    public string ConnectionString { get; set; }

    private MongodbContextOption()
    {
        ConnectionString = string.Empty;
    }

    public MongodbContextOption(string connectionString)
    {
        ConnectionString = connectionString;
    }
}