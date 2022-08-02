using MicroShop.Common.Data.Context;
using MicroShop.Common.Data.Context.Mongodb;
using Microsoft.Extensions.DependencyInjection;

namespace MicroShop.Common.Extensions;

public static class MongodbExtensions
{
    public static IServiceCollection AddMongodbContext<TDbContext>(this IServiceCollection services, Action<MongodbContextOption> options) where
        TDbContext : MongodbContext
    {
        var mongodbOptions = new MongodbContextOption("");
        options(mongodbOptions);

        services.AddTransient<IMongoClient>(c => new MongoClient(mongodbOptions.ConnectionString));
        services.AddSingleton<MongodbContextOption>(p => mongodbOptions);
        services.AddTransient<TDbContext>();

        return services;
    }
}