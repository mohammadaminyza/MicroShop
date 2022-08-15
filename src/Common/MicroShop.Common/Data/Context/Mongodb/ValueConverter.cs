using System.Linq.Expressions;

namespace MicroShop.Common.Data.Context.Mongodb;

/// <summary>
/// Value conversion useful for value objects
/// </summary>
/// <typeparam name="TModel"></typeparam>
/// <typeparam name="TProvider"></typeparam>
internal class ValueConverter<TModel, TProvider>
{
    private static readonly Dictionary<Type, ValueConverterAction<TModel, TProvider>> _conversions = new();

    private readonly Func<TModel, TProvider> _convertToProvider;
    private readonly Func<TProvider, TModel> _convertFromProvider;
    public ValueConverter(
           Type entityType,
           string propertyName,
           Expression<Func<TModel, TProvider>> convertToProviderExpression,
           Expression<Func<TProvider, TModel>> convertFromProviderExpression)
    {
        _convertToProvider = convertToProviderExpression.Compile();
        _convertFromProvider = convertFromProviderExpression.Compile();

        AddConvertion(entityType,
            propertyName,
            _convertToProvider,
            _convertFromProvider);
    }

    public static TProvider ConvertToProvider(
        Type entityType,
        string propertyName,
        TModel model)
    {
        var convertor = _conversions.FirstOrDefault(c => c.Key == entityType &&
            c.Value.PropertyName == propertyName);

        var convert = convertor.Value.ToProvider;
        var convertedValue = convert(model);

        return convertedValue;
    }


    private void AddConvertion(
        Type entityType,
        string propertyName,
        Func<TModel, TProvider> modelConversion,
        Func<TProvider, TModel> providerConversion)
    {
        if (!_conversions.Any(c => c.Key == entityType && c.Value.PropertyName == propertyName))
        {
            ValueConverterAction<TModel, TProvider> valueConversion = new(
                modelConversion,
                providerConversion,
                propertyName);

            _conversions.Add(entityType, valueConversion);
        }
    }
}

/// <summary>
/// Standard structure for saving value conversions
/// </summary>
/// <typeparam name="TModel"></typeparam>
/// <typeparam name="TProvider"></typeparam>
internal class ValueConverterAction<TModel, TProvider>
{
    public Func<TModel, TProvider> ToProvider { get; set; }
    public Func<TProvider, TModel> FromProvider { get; set; }
    public string PropertyName { get; set; }

    public ValueConverterAction(
        Func<TModel, TProvider> toProvider,
        Func<TProvider, TModel> fromProvider,
        string propertyName)
    {
        ToProvider = toProvider;
        FromProvider = fromProvider;
        PropertyName = propertyName;
    }
}