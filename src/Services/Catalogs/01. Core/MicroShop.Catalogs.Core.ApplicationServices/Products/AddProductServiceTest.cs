using MicroShop.Catalogs.Core.Contracts.Products;
using MicroShop.Catalogs.Core.Domain.Products.Entities;
using MicroShop.Common.ValueObjects;

namespace MicroShop.Catalogs.Core.ApplicationServices.Products;

public class AddProductServiceTest
{
    private readonly IProductCommandRepository _productCommandRepository;

    public AddProductServiceTest(IProductCommandRepository productCommandRepository)
    {
        _productCommandRepository = productCommandRepository;
    }

    public async Task Handle()
    {
        await _productCommandRepository.InsertAsync(new Product(Guid.NewGuid(), "Alireza"));
    }
}