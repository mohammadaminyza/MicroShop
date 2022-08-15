using MicroShop.Common.Data.Context.Mongodb;
using Microsoft.Extensions.DependencyInjection;

namespace MicroShop.Common.Extensions;

/// <summary>
/// Configure mongodb context dependencies
/// </summary>
public static class MongodbExtensions
{
    public static IServiceCollection AddMongodbContext<TDbContext>(this IServiceCollection services, Action<MongodbContextOption> options) where
        TDbContext : MongodbContext
    {
        var mongodbOptions = new MongodbContextOption();
        options(mongodbOptions);

        services.AddTransient<IMongoClient>(c => new MongoClient(mongodbOptions.ConnectionString));
        services.AddSingleton<MongodbContextOption>(p => mongodbOptions);
        services.AddTransient<TDbContext>();
        services.AddSingleton<MongodbBaseService>();
        services.AddHostedService<MongodbService<TDbContext>>();

        return services;
    }
}