using MicroShop.Common.Exceptions;
using MicroShop.Common.Utilities;
using MicroShop.Common.ValueObjects;

namespace MicroShop.Catalogs.Core.Domain.Common.ValueObjects;

public class Name : BaseValueObject<Name>
{
    public string Value { get; set; }

    public override bool ObjectIsEqual(Name otherObject) => Value == otherObject.Value;
    public override int ObjectGetHashCode() => Value.GetHashCode();

    public static explicit operator string(Name name) => name.Value;
    public static implicit operator Name(string name) => new(name);

    private Name()
    {
        Value = string.Empty;
    }

    public Name(string value)
    {
        if (value.IsNullOrEmpty())
            throw new InvalidValueObjectStateException("Value Can't Be NullOrEmpty");

        Value = value;
    }
}