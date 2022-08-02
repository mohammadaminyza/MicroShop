using MicroShop.Catalogs.Core.Domain.Common.ValueObjects;
using MicroShop.Common.Data.Context.Mongodb;

namespace MicroShop.Catalogs.Data.MongoCommand.Common.Convertors;

public class NameConversion : ValueConverter<Name, string>
{
    public NameConversion() : base(n => n.Value, n => Name.FromString(n))
    {
    }
}