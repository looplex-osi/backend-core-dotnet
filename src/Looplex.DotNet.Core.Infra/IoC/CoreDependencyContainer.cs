using Looplex.DotNet.Core.Application.Abstractions.DataAccess;
using Looplex.DotNet.Core.Infra.Data;
using Microsoft.Extensions.DependencyInjection;

namespace Looplex.DotNet.Core.Infra.IoC
{
    public class CoreDependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<IDatabaseContext, DatabaseContext>();
        }
    }
}