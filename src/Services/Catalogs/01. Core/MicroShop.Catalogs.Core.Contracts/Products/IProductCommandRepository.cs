using MicroShop.Catalogs.Core.Domain.Common.ValueObjects;
using MicroShop.Catalogs.Core.Domain.Products.Entities;

namespace MicroShop.Catalogs.Core.Contracts.Products;

public interface IProductCommandRepository : ICommandRepository<Product>
{
}