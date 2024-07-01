using Looplex.OpenForExtension.Context;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Looplex.DotNet.Core.Middlewares
{
    public delegate Task MiddlewareDelegate(IDefaultContext context, Func<Task> next);

    public class MiddlewareComposer
    {
        public static Func<IDefaultContext, Task> Compose(MiddlewareDelegate[] middlewares)
        {
            if (middlewares == null)
                throw new ArgumentNullException(nameof(middlewares), "Middlewares stack MUST be provided.");

            foreach (var middleware in middlewares)
            {
                if (middleware == null)
                    throw new ArgumentException("Middlewares MUST be composed of functions.", nameof(middlewares));
            }

            return async (context) =>
            {
                int index = -1;
                await Dispatch(0);

                async Task Dispatch(int i)
                {
                    if (i <= index)
                        throw new InvalidOperationException("next() called multiple times");
                    index = i;
                    var middleware = i == middlewares.Length ? null : middlewares[i];
                    if (middleware == null) return;
                    try
                    {
                        await middleware(context, () => Dispatch(i + 1));
                    }
                    catch (Exception ex)
                    {
                        context.Services.GetService<ILogger>()
                            ?.LogError(ex, "An error occurred when running the middleware");
                        throw;
                    }
                }
            };
        }
    }
}
