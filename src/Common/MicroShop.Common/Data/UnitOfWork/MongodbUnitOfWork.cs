using MicroShop.Common.Data.Context.Mongodb;

namespace MicroShop.Common.Data.UnitOfWork;

public class MongodbUnitOfWork<TContext> : IUnitOfWork where TContext : BaseMongoCommandDbContext
{
    private readonly TContext _dbContext;

    public MongodbUnitOfWork(TContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void BeginTransaction()
    {
        _dbContext.StartTransaction();
    }

    public void CommitTransaction()
    {
        _dbContext.CommitTransaction();
    }

    public void RollbackTransaction()
    {
        _dbContext.AbortTransaction();
    }

    public int Commit()
    {
        _dbContext.CommitTransaction();

        return 0;
    }

    public async Task<int> CommitAsync()
    {
        await _dbContext.CommitTransactionAsync();
        return 0;
    }
}