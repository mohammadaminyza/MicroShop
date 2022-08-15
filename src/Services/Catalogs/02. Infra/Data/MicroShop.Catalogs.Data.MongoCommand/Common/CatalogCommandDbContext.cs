using MicroShop.Catalogs.Core.Domain.Products.Entities;
using MicroShop.Common.Data.Context.Mongodb;

namespace MicroShop.Catalogs.Data.MongoCommand.Common;

public class CatalogCommandDbContext : BaseMongoCommandDbContext
{
    public CatalogCommandDbContext(MongodbContextOption option) : base(option)
    {
    }

    public DbSet<Product> Products => Set<Product>();
}