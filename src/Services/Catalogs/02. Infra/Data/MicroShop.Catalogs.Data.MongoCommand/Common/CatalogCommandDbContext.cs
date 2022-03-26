using MicroShop.Catalogs.Core.Domain.Products.Entities;
using MicroShop.Common.Data.Context;
using MicroShop.Common.Data.Context.Mongodb;
using MongoDB.Driver;

namespace MicroShop.Catalogs.Data.MongoCommand.Common;

public class CatalogCommandDbContext : MongodbContext
{
    public CatalogCommandDbContext(IMongoClient client, MongodbContextOption option) : base(client, option)
    {
    }

    public IMongoCollection<Product> Products => Set<Product>();
}