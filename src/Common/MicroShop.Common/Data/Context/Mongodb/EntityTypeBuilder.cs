using System.Linq.Expressions;

namespace MicroShop.Common.Data.Context.Mongodb;

public class EntityTypeBuilder<TEntity>
{
    public PropertyTypeBuilder<TEntity, TPropertyType> Property<TPropertyType>(Expression<Func<TEntity, TPropertyType>> property)
    {
        var propertyName = GetPropertyNameByExperssion(property);

        return new PropertyTypeBuilder<TEntity, TPropertyType>(propertyName);
    }

    private string GetPropertyNameByExperssion<TPropertyType>(
        Expression<Func<TEntity, TPropertyType>> property)
    {
        var propertyInfo = (MemberExpression)property.Body;
        var propertyName = propertyInfo.Member.Name;
        return propertyName;
    }
}

public class PropertyTypeBuilder<TEntity, TPropertyType>
{
    private readonly string _propertyName;

    public PropertyTypeBuilder(string propertyName)
    {
        _propertyName = propertyName;
    }

    public PropertyTypeBuilder<TEntity, TPropertyType> HasConversion<TOutput>(
        Expression<Func<TPropertyType, TOutput>> provider,
        Expression<Func<TOutput, TPropertyType>> model)
    {
        new ValueConverter<TOutput, TPropertyType>(typeof(TEntity), _propertyName, model, provider);

        return this;
    }

    public PropertyTypeBuilder<TEntity, TPropertyType> IsRequired()
    {
        return this;
    }
}
