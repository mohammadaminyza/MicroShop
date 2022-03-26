using System.Reflection;
using MicroShop.Common.Data.Repository;
using MicroShop.Common.Data.UnitOfWork;
using MicroShop.Common.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace MicroShop.Common.Extensions;

public static class CommonExtension
{
    public static IServiceCollection AddCommon(this IServiceCollection services)
    {
        return services;
    }

    //public static IServiceCollection AddInterfaceDependenciesLifeTime(this IServiceCollection services, IEnumerable<Assembly> assemblies)
    //{
    //    return services.AddWithTransientLifetime(assemblies, typeof(ITransientLifetime))
    //        .AddWithScopedLifetime(assemblies, typeof(IScopeLifetime))
    //        .AddWithSingletonLifetime(assemblies, typeof(ISingletonLifetime));
    //}

    //public static IServiceCollection AddRepositories(this IServiceCollection services,
    //    IEnumerable<Assembly> assembliesForSearch) =>
    //    services.AddWithTransientLifetime(assembliesForSearch, typeof(ICommandRepository<>), typeof(IQueryRepository));

    //public static IServiceCollection AddUnitOfWorks(this IServiceCollection services,
    //    IEnumerable<Assembly> assembliesForSearch) =>
    //    services.AddWithTransientLifetime(assembliesForSearch, typeof(IUnitOfWork));

    //public static IServiceCollection AddWithTransientLifetime(this IServiceCollection services,
    //    IEnumerable<Assembly> assembliesForSearch,
    //    params Type[] assignableTo)
    //{
    //    services.Scan(s => s.FromAssemblies(assembliesForSearch)
    //        .AddClasses(c => c.AssignableToAny(assignableTo))
    //        .AsImplementedInterfaces()
    //        .WithTransientLifetime());
    //    return services;
    //}

    //public static IServiceCollection AddWithScopedLifetime(this IServiceCollection services,
    //    IEnumerable<Assembly> assembliesForSearch,
    //    params Type[] assignableTo)
    //{
    //    services.Scan(s => s.FromAssemblies(assembliesForSearch)
    //        .AddClasses(c => c.AssignableToAny(assignableTo))
    //        .AsImplementedInterfaces()
    //        .WithScopedLifetime());
    //    return services;
    //}

    //public static IServiceCollection AddWithSingletonLifetime(this IServiceCollection services,
    //    IEnumerable<Assembly> assembliesForSearch,
    //    params Type[] assignableTo)
    //{
    //    services.Scan(s => s.FromAssemblies(assembliesForSearch)
    //        .AddClasses(c => c.AssignableToAny(assignableTo))
    //        .AsImplementedInterfaces()
    //        .WithSingletonLifetime());
    //    return services;
    //}
}