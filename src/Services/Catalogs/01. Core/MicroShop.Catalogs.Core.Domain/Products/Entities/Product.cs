using MicroShop.Catalogs.Core.Domain.Common.ValueObjects;
using MicroShop.Catalogs.Core.Domain.Products.Events;
using MicroShop.Common.Entities;
using MicroShop.Common.ValueObjects;

namespace MicroShop.Catalogs.Core.Domain.Products.Entities;

public class Product : AggregateRoot
{
    #region Properties

    public Name? Name { get; set; }

    #endregion

    #region Ctor

    private Product()
    {
    }

    public Product(Id id, Name name)
    {
        Id = id;
        Name = name;

        AddEvent(new ProductCreated(Id.Value, Name.Value));
    }

    #endregion

    #region Methods



    #endregion
}