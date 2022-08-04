using MicroShop.Catalogs.Core.Domain.Products.Entities;
using MicroShop.Catalogs.Data.MongoCommand.Common.Convertors;
using MicroShop.Common.Data.Context.Mongodb;

namespace MicroShop.Catalogs.Data.MongoCommand.Common;

public class CatalogCommandDbContext : BaseMongoCommandDbContext
{
    public CatalogCommandDbContext(MongodbContextOption option) : base(option)
    {
        new NameConversion();
    }

    public DbSet<Product> Products => Set<Product>();
}