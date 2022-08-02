using MicroShop.Catalogs.Core.Contracts.Products;
using MicroShop.Catalogs.Core.Domain.Products.Entities;
using MicroShop.Common.ApplicationServices.Commands;

namespace MicroShop.Catalogs.Core.ApplicationServices.Products.Commands.CreateProduct;

public class CreateProductCommandHandler : CommandHandler<CreateProductCommand>
{
    private readonly IProductCommandRepository _productCommandRepository;

    public CreateProductCommandHandler(IProductCommandRepository productCommandRepository)
    {
        _productCommandRepository = productCommandRepository;
    }

    public override async Task<CommandResult> Handle(CreateProductCommand request)
    {
        await _productCommandRepository.InsertAsync(new Product(Guid.NewGuid(), request.Name));

        return await OkAsync();
    }
}