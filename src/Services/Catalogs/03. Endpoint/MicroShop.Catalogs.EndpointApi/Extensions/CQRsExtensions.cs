using MicroShop.Common.ApplicationServices.Commands;

namespace MicroShop.Catalogs.EndpointApi.Extensions;

public static class CQRSExtensions
{
    public static ICommandDispatcher CommandDispatcher(this HttpContext httpContext) =>
        httpContext.RequestServices.GetRequiredService<ICommandDispatcher>();
}