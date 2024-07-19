using Looplex.DotNet.Core.Infra.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Looplex.DotNet.Core.WebAPI.ExtensionMethods;

public static class ServicesExtensionMethods
{
    public static void AddCoreServices(this IServiceCollection services)
    {
        CoreDependencyContainer.RegisterServices(services);
    }
}