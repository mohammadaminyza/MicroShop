using MicroShop.Common.Data.Context.Mongodb;

namespace MicroShop.Common.Data.UnitOfWork;

public class MongodbUnitOfWork<TContext> : IUnitOfWork where TContext : MongodbContext
{
    private readonly TContext _dbContext;

    public MongodbUnitOfWork(TContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void BeginTransaction()
    {
        throw new NotImplementedException();
    }

    public void CommitTransaction()
    {
        throw new NotImplementedException();
    }

    public void RollbackTransaction()
    {
        throw new NotImplementedException();
    }

    public int Commit()
    {
        throw new NotImplementedException();
    }

    public async Task<int> CommitAsync()
    {
        throw new NotImplementedException();
    }
}