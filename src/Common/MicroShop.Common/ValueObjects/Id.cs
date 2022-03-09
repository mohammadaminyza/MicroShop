using MicroShop.Common.Exceptions;

namespace MicroShop.Common.ValueObjects;

public class Id : BaseValueObject<Id>
{
    public Guid Value { get; set; }

    public override bool ObjectIsEqual(Id otherObject) => Value == otherObject.Value;
    public override int ObjectGetHashCode() => Value.GetHashCode();



    public static explicit operator string(Id title) => title.Value.ToString();
    public static implicit operator Id(string value) => new(value);


    public static explicit operator Guid(Id title) => title.Value;
    public static implicit operator Id(Guid value) => new() { Value = value };

    public override string ToString() => Value.ToString();

    private Id()
    {
    }

    public Id(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidValueObjectStateException("ValidationErrorIsRequire", nameof(Id));
        }
        if (Guid.TryParse(value, out Guid tempValue))
        {
            Value = tempValue;
        }
        else
        {
            throw new InvalidValueObjectStateException("ValidationErrorInvalidValue", nameof(Id));
        }
    }
}