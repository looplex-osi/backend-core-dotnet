﻿using Looplex.OpenForExtension.Abstractions.Contexts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Looplex.DotNet.Core.Middlewares
{
    public delegate Task MiddlewareDelegate(IContext context, Func<Task> next);

    public static class MiddlewareComposer
    {
        public static Func<IContext, Task> Compose(MiddlewareDelegate?[] middlewares)
        {
            if (middlewares == null)
                throw new ArgumentNullException(nameof(middlewares), "Middlewares stack MUST be provided.");

            if (middlewares.Any(middleware => middleware == null))
            {
                throw new ArgumentException("Middlewares MUST be composed of functions.", nameof(middlewares));
            }

            return async (context) =>
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
