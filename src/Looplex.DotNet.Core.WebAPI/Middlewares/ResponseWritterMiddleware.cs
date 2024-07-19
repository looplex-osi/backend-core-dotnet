using System.Text;
using Looplex.DotNet.Core.Middlewares;
using Microsoft.AspNetCore.Http;

namespace Looplex.DotNet.Core.WebAPI.Middlewares
{
    public static partial class CoreMiddlewares
    {
        public static readonly MiddlewareDelegate ResponseWritterMiddleware = new(async (context, cancellationToken, next) =>
        {
            cancellationToken.ThrowIfCancellationRequested();
            
            HttpContext httpContext = context.State.HttpContext;

            var originalBodyStream = httpContext.Response.Body;
            var responseBodyStream = new MemoryStream();
            httpContext.Response.Body = responseBodyStream;

            await next();

            if (responseBodyStream.Length > 0)
            {
                responseBodyStream.Seek(0, SeekOrigin.Begin);
                var responseBody = await new StreamReader(responseBodyStream).ReadToEndAsync();
                await using var responseWriter = new StreamWriter(originalBodyStream, Encoding.UTF8, leaveOpen: true);
                await responseWriter.WriteAsync(responseBody);
            }
            httpContext.Response.Body = originalBodyStream;
        });
    }
}
