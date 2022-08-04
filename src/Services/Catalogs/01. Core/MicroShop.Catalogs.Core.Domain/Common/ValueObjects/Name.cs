using Ardalis.GuardClauses;
using MicroShop.Common.Exceptions;
using MicroShop.Common.GuardClauses;

namespace MicroShop.Catalogs.Core.Domain.Common.ValueObjects;

public class Name : BaseValueObject<Name>
{
    public static Name FromString(string name) => new(name);

    public string Value { get; set; }

    public override bool ObjectIsEqual(Name otherObject) => Value == otherObject.Value;
    public override int ObjectGetHashCode() => Value.GetHashCode();

    public static explicit operator string(Name name) => name.Value;
    public static implicit operator Name(string name) => new(name);

    public Name(string value)
    {
        Guard.Against.NullOrEmpty(value,
            nameof(Value), ResourceKeys.InvalidNullOrEmpty, new InvalidValueObjectStateException(ResourceKeys.InvalidNullOrEmpty));

        Value = value;
    }
}