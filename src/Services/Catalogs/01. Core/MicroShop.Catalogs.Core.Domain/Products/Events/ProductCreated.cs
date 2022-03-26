namespace MicroShop.Catalogs.Core.Domain.Products.Events;

public class ProductCreated : IDomainEvent
{
    #region Properties

    public Guid Id { get; set; }
    public string Name { get; set; }

    #endregion

    #region Ctor

    public ProductCreated(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    #endregion
}