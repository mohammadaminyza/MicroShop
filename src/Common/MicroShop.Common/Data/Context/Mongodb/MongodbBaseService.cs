namespace MicroShop.Common.Data.Context.Mongodb;

/// <summary>
/// Base services like excuting db configuration etc.
/// </summary>
public class MongodbBaseService
{
    /// <summary>
    /// Excuting dbSet configurations
    /// </summary>
    /// <param name="dbContext"></param>
    public void ExcuteEntityConfigurations(MongodbContext dbContext)
    {
        //Todo Excute Config Method
        var properties = dbContext.GetType()
           .GetProperties()
           .ToList();

        foreach (var property in properties)
        {
            var genericType = property.PropertyType?.GenericTypeArguments.FirstOrDefault();

            if (genericType == null)
                continue;

            var genericEntityBuilder = typeof(EntityTypeBuilder<>).MakeGenericType(genericType);

            var entityBuilder = Activator.CreateInstance(genericEntityBuilder);

            if (entityBuilder == null)
                continue;

            var entityConfigurationType = typeof(IEntityTypeConfiguration<>);

            var configs = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => t.GetInterface(entityConfigurationType.Name) != null && t.IsClass && !t.IsAbstract)
                .ToList();

            if (configs == null)
                continue;

            foreach (var configType in configs)
            {
                var config = Activator.CreateInstance(configType);
                var configMethod = config?.GetType()?.GetMethod(entityConfigurationType.GetMethods()[0].Name);
                configMethod?.Invoke(config, new object[] { entityBuilder });
            }
        }

    }
}