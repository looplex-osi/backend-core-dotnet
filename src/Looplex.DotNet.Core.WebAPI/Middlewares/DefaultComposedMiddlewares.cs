using Looplex.DotNet.Core.Middlewares;

namespace Looplex.DotNet.Core.WebAPI.Middlewares
{
    public static class DefaultComposedMiddlewares
    {
        internal static readonly MiddlewareDelegate[] RequiredEndpointMiddlewares = {
            CoreMiddlewares.ResponseWritterMiddleware,
            CoreMiddlewares.ExceptionMiddleware,
        };
    }
}
