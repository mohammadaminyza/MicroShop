using MicroShop.Common.ApplicationServices.Commands;

namespace MicroShop.Catalogs.Core.ApplicationServices.Products.Commands.CreateProduct;

public class CreateProductCommand : ICommand
{
    public string Name { get; set; } = null!;
}