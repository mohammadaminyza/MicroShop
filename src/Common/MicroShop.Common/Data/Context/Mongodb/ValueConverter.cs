using System.Linq.Expressions;

namespace MicroShop.Common.Data.Context.Mongodb;

public abstract class ValueConverter<TModel, TProvider>
{
    private static Dictionary<Type, Dictionary<Func<TModel, TProvider>, Func<TProvider, TModel>>> _conversions = new();

    private readonly Func<TModel, TProvider> _convertToProvider;
    private readonly Func<TProvider, TModel> _convertFromProvider;
    protected ValueConverter(
           Expression<Func<TModel, TProvider>> convertToProviderExpression,
           Expression<Func<TProvider, TModel>> convertFromProviderExpression)
    {
        _convertToProvider = convertToProviderExpression.Compile();
        _convertFromProvider = convertFromProviderExpression.Compile();

        AddConvertion(_convertToProvider, _convertFromProvider);
    }

    public static TProvider ConvertToProvider(TModel model)
    {
        var convertorKey = _conversions.FirstOrDefault(c => c.Key == typeof(TModel));
        var convertor = convertorKey.Value.FirstOrDefault().Key;
        var conversion = convertor.Invoke(model);

        return conversion;
    }


    private void AddConvertion(Func<TModel, TProvider> modelConversion, Func<TProvider, TModel> providerConversion)
    {
        if (!_conversions.Any(c => c.Key == typeof(TModel)))
        {
            var valueConversion = new Dictionary<Func<TModel, TProvider>, Func<TProvider, TModel>>();
            valueConversion.Add(modelConversion, providerConversion);

            _conversions.Add(typeof(TModel), valueConversion);
        }
        else
        {
            var keyConversion = _conversions.First(c => c.Key == typeof(TModel));
            keyConversion.Value.Add(modelConversion, providerConversion);
        }
    }
}