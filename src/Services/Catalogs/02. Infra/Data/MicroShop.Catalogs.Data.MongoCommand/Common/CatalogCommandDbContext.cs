using MicroShop.Catalogs.Core.Domain.Products.Entities;
using MicroShop.Catalogs.Data.MongoCommand.Products.Configs;
using MicroShop.Common.Data.Context.Mongodb;

namespace MicroShop.Catalogs.Data.MongoCommand.Common;

public class CatalogCommandDbContext : BaseMongoCommandDbContext
{
    public CatalogCommandDbContext(MongodbContextOption option) : base(option)
    {
        //TODO Note: باید متد کانفیگ ها را پر کند را در لایف تایم موقعه لانچ برنامه یک بار انجام بشه
        var config = new ProductConfig();
        config.Configure(new());
    }

    public DbSet<Product> Products => Set<Product>();
}