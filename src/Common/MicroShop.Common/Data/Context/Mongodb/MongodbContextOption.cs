using MongoDB.Driver;
namespace MicroShop.Common.Data.Context.Mongodb;

/// <summary>
/// Stuff That Context Needs For Launching Like ConnectionString etc.
/// </summary>
public class MongodbContextOption
{
    public string ConnectionString { get; set; }
    public string? DatabaseName => MongoUrl.Create(ConnectionString)?.DatabaseName;

    internal MongodbContextOption()
    {
        ConnectionString = string.Empty;
    }

    public MongodbContextOption(string connectionString)
    {
        ConnectionString = connectionString;
    }
}