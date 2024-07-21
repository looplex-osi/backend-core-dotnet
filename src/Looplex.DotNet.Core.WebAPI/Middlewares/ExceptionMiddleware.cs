using System.Dynamic;
using System.Net;
using Looplex.DotNet.Core.Common.Exceptions;
using Looplex.DotNet.Core.Common.Utils;
using Looplex.DotNet.Core.Middlewares;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace Looplex.DotNet.Core.WebAPI.Middlewares
{
    public static partial class CoreMiddlewares
    { 
        public static readonly MiddlewareDelegate ExceptionMiddleware = new(async (context, cancellationToken, next) =>
        {            
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                await next();
            }
            catch (EntityNotFoundException ex)
            {
                HttpContext httpContext = context.State.HttpContext;
                var env = httpContext.RequestServices.GetRequiredService<IWebHostEnvironment>();

                await httpContext.Response
                    .WriteAsJsonAsync(GetErrorObject(ex, env), HttpStatusCode.NotFound);
            }
            catch (EntityInvalidException ex)
            {
                HttpContext httpContext = context.State.HttpContext;
                var env = httpContext.RequestServices.GetRequiredService<IWebHostEnvironment>();
                
                await httpContext.Response
                    .WriteAsJsonAsync(GetErrorObject(ex, env), HttpStatusCode.NotFound);
            }
            catch (HttpRequestException ex)
            {
                HttpContext httpContext = context.State.HttpContext;
                var env = httpContext.RequestServices.GetRequiredService<IWebHostEnvironment>();

                var statusCode = ex.StatusCode ?? HttpStatusCode.InternalServerError;
                await httpContext.Response
                    .WriteAsJsonAsync(GetErrorObject(ex, env), statusCode);
            }
            catch (Exception ex)
            {
                HttpContext httpContext = context.State.HttpContext;
                var env = httpContext.RequestServices.GetRequiredService<IWebHostEnvironment>();

                await httpContext.Response
                    .WriteAsJsonAsync(GetErrorObject(ex, env), HttpStatusCode.InternalServerError);
            }
        });

        private static string GetErrorObject(Exception ex, IWebHostEnvironment env)
        {
            dynamic error = new ExpandoObject();

            error.Message = ex.Message;
            if (env.IsDevelopment())
            {
                error.Stack = ex.StackTrace;
                error.Source = ex.Source;
                error.InnerException = ex.InnerException?.Message;
            }

            if (ex is EntityInvalidException entityInvalidException)
            {
                error.ErrorMessages = entityInvalidException.ErrorMessages;
            }

            return JsonConvert.SerializeObject(error);
        }
    }
}
