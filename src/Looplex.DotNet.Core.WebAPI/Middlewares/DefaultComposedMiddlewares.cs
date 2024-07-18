using Looplex.DotNet.Core.Middlewares;

namespace Looplex.DotNet.Core.WebAPI.Middlewares
{
    public static class DefaultComposedMiddlewares
    {
        internal readonly static MiddlewareDelegate[] RequiredEndpointMiddlewares = {
            CoreMiddlewares.ResponseWritterMiddleware,
            CoreMiddlewares.ExceptionMiddleware,
        };
    }
}
