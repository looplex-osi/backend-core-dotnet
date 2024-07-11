using Looplex.DotNet.Core.Infra.IoC;
using Looplex.DotNet.Core.Infra.Profiles;
using Microsoft.Extensions.DependencyInjection;

namespace Looplex.DotNet.Core.WebAPI.ExtensionMethods;

public static class ServicesExtensionMethods
{
    public static void AddCoreAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(CoreProfile));
    }

    public static void AddCoreServices(this IServiceCollection services)
    {
        CoreDependencyContainer.RegisterServices(services);
    }
}