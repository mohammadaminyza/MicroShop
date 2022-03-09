using MicroShop.Common.ValueObjects;

namespace MicroShop.Catalogs.Core.Domain.Common.ValueObjects;

public class Name : BaseValueObject<Name>
{
    public string Value { get; set; }

    public override bool ObjectIsEqual(Name otherObject) => Value == otherObject.Value;
    public override int ObjectGetHashCode() => Value.GetHashCode();

    public Name(string value)
    {
        Value = value;
    }
}