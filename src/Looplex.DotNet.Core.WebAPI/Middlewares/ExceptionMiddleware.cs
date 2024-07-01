using Looplex.DotNet.Core.Common.Exceptions;
using Looplex.DotNet.Core.Common.Utils;
using Looplex.DotNet.Core.Middlewares;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Dynamic;
using System.Net;

namespace Looplex.DotNet.Core.Common.Middlewares
{
    public static partial class CoreMiddlewares
    { 
        public readonly static MiddlewareDelegate ExceptionMiddleware = new(async (context, next) =>
        {            
            try
            {
                await next();
            }
            catch (EntityNotFoundException ex)
            {
                HttpContext httpContext = context.State.HttpContext;
                IWebHostEnvironment env = httpContext.RequestServices.GetRequiredService<IWebHostEnvironment>();

                await httpContext.Response
                    .WriteAsJsonAsync(GetErrorObject(ex, env), HttpStatusCode.NotFound);
            }
            catch (HttpRequestException ex)
            {
                HttpContext httpContext = context.State.HttpContext;
                IWebHostEnvironment env = httpContext.RequestServices.GetRequiredService<IWebHostEnvironment>();

                HttpStatusCode statusCode = ex.StatusCode ?? HttpStatusCode.InternalServerError;
                await httpContext.Response
                    .WriteAsJsonAsync(GetErrorObject(ex, env), statusCode);
            }
            catch (Exception ex)
            {
                HttpContext httpContext = context.State.HttpContext;
                IWebHostEnvironment env = httpContext.RequestServices.GetRequiredService<IWebHostEnvironment>();

                await httpContext.Response
                    .WriteAsJsonAsync(GetErrorObject(ex, env), HttpStatusCode.InternalServerError);
            }
        });

        private static object GetErrorObject(Exception ex, IWebHostEnvironment env)
        {
            dynamic error = new ExpandoObject();

            error.Message = ex.Message;
            if (env.IsDevelopment())
            {
                error.Stack = ex.StackTrace;
                error.Source = ex.Source;
                error.InnerException = ex.InnerException?.Message;
            }

            return error;
        }
    }
}
