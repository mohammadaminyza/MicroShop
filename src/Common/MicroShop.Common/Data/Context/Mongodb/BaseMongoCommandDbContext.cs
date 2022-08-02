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

    public void Add()
    {
        //TODO Complate
    }

    #endregion

    #region Savechange

    public int SaveChanges()
    {
        using (StartTransaction())
        {
            Task.WhenAll(_mongodbTracker.ExcuteActions());

            CommitTransaction();
        }

        return _mongodbTracker.MongodbActions.Count();
    }


    public async Task<int> SaveChangesAsync()
    {
        using (StartTransaction())
        {
            await Task.WhenAll(_mongodbTracker.ExcuteActions());
            await CommitTransactionAsync();
        }

        return _mongodbTracker.MongodbActions.Count();
    }

    #endregion
}