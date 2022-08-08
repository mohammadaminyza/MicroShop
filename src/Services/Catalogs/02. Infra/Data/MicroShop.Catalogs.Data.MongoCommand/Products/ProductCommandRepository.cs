using MicroShop.Catalogs.Core.Contracts.Products;
using MicroShop.Catalogs.Core.Domain.Common.ValueObjects;
using MicroShop.Catalogs.Core.Domain.Products.Entities;
using MicroShop.Catalogs.Data.MongoCommand.Common;
using MicroShop.Common.Data.Context.Mongodb;

namespace MicroShop.Catalogs.Data.MongoCommand.Products;

public class ProductCommandRepository : MongodbBaseCommandRepository<Product, CatalogCommandDbContext>, IProductCommandRepository
{
    public ProductCommandRepository(CatalogCommandDbContext dbContext) : base(dbContext)
    {
    }
}