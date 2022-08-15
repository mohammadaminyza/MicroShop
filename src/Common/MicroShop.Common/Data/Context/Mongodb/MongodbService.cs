using Microsoft.Extensions.Hosting;

namespace MicroShop.Common.Data.Context.Mongodb;

/// <summary>
/// Mongodb Host Service
/// On Launching Application This Service Will Register All Your Database Configurations
/// </summary>
/// <typeparam name="TDbContext"></typeparam>
public class MongodbService<TDbContext> : IHostedService where TDbContext : MongodbContext
{
    private readonly TDbContext _dbContext;
    private readonly MongodbBaseService _mongodbConfigurationManagment;

    public MongodbService(TDbContext dbContext, MongodbBaseService mongodbConfigurationManagment)
    {
        _dbContext = dbContext;
        _mongodbConfigurationManagment = mongodbConfigurationManagment;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _mongodbConfigurationManagment.ExcuteEntityConfigurations(_dbContext);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}