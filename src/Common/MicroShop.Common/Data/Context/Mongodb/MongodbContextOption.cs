using MongoDB.Driver;
namespace MicroShop.Common.Data.Context.Mongodb;

/// <summary>
/// Stuff That Context Needs For Launching Like ConnectionString etc.
/// </summary>
public class MongodbContextOption
{
    private readonly MongoUrl? _mongoUrl;

    public string? ConnectionString { get; set; }
    public string? DatabaseName => _mongoUrl?.DatabaseName;

    internal MongodbContextOption()
    {
    }

    public MongodbContextOption(string connectionString, MongoUrl mongoUrl)
    {
        ConnectionString = connectionString;
        _mongoUrl = mongoUrl;
    }
}