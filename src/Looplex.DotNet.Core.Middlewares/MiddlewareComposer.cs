using Looplex.OpenForExtension.Context;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Looplex.DotNet.Core.Middlewares
{
    public delegate Task MiddlewareDelegate(IDefaultContext context, CancellationToken cancellationToken, Func<Task> next);

    public static class MiddlewareComposer
    {
        public static Func<IDefaultContext, CancellationToken, Task> Compose(MiddlewareDelegate?[] middlewares)
        {
            if (middlewares == null)
                throw new ArgumentNullException(nameof(middlewares), "Middlewares stack MUST be provided.");

            if (middlewares.Any(middleware => middleware == null))
            {
                throw new ArgumentException("Middlewares MUST be composed of functions.", nameof(middlewares));
            }

            return async (context, cancellationToken) =>
            {
                var index = -1;
                await Dispatch(0);
                return;

                async Task Dispatch(int i)
                {
                    if (i <= index)
                        throw new InvalidOperationException("next() called multiple times");
                    index = i;
                    var middleware = i == middlewares.Length ? null : middlewares[i];
                    if (middleware == null) return;
                    try
                    {
                        await middleware(context, cancellationToken, () => Dispatch(i + 1));
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
