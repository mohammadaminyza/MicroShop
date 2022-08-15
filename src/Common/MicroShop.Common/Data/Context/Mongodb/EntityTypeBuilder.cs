using System.Linq.Expressions;

namespace MicroShop.Common.Data.Context.Mongodb;

/// <summary>
/// FluentApi property selection
/// </summary>
/// <typeparam name="TEntity"></typeparam>
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

/// <summary>
/// FluentApi property methods
/// </summary>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="TPropertyType"></typeparam>
public class PropertyTypeBuilder<TEntity, TPropertyType>
{
    private readonly string _propertyName;

    public PropertyTypeBuilder(string propertyName)
    {
        _propertyName = propertyName;
    }

    /// <summary>
    /// Conversion type to other type
    /// It's mostly useful in value objects
    /// </summary>
    /// <typeparam name="TOutput"></typeparam>
    /// <param name="provider"></param>
    /// <param name="model"></param>
    /// <returns></returns>
    public PropertyTypeBuilder<TEntity, TPropertyType> HasConversion<TOutput>(
        Expression<Func<TPropertyType, TOutput>> provider,
        Expression<Func<TOutput, TPropertyType>> model)
    {
        new ValueConverter<TOutput, TPropertyType>(typeof(TEntity), _propertyName, model, provider);

        return this;
    }
}
