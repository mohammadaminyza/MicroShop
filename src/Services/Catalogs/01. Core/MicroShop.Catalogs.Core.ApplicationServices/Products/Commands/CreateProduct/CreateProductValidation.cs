using FluentValidation;

namespace MicroShop.Catalogs.Core.ApplicationServices.Products.Commands.CreateProduct;

public class CreateProductValidation : AbstractValidator<CreateProductCommand>
{
    public CreateProductValidation()
    {
        RuleFor(c => c.Name)
            .NotEmpty();
    }
}