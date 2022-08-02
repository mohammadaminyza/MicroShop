using MicroShop.Catalogs.Core.Domain.Products.Entities;
using MicroShop.Catalogs.Data.MongoCommand.Common.Convertors;
using MicroShop.Common.Data.Context.Mongodb;
using MongoDB.Driver;

namespace MicroShop.Catalogs.Data.MongoCommand.Common;

public class CatalogCommandDbContext : MongodbContext
{
    public CatalogCommandDbContext(IMongoClient client, MongodbContextOption option) : base(client, option)
    {
        new NameConversion();
    }

    public IMongoCollection<Product> Products => Set<Product>();
}