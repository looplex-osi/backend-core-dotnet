using Looplex.DotNet.Core.Common.Middlewares;
using Looplex.DotNet.Core.Middlewares;
using Looplex.DotNet.Core.WebAPI.Middlewares;
using Looplex.DotNet.Core.WebAPI.Routes;
using Looplex.OpenForExtension.Context;
using Looplex.OpenForExtension.Plugins;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.OpenApi.Models;

namespace Looplex.DotNet.Core.WebAPI.Routes
{
    public static partial class RouteBuilder
    {
        public static RouteHandlerBuilder MapGet(
            this IEndpointRouteBuilder app,
            string routeName,
            IList<IPlugin> plugins,
            MiddlewareDelegate[] middlewares,
            int[]? producesStatusCodes = null)
        {
            var builder = app.MapGet(routeName, async (HttpContext httpContext, CancellationToken cancellationToken) =>
            {
                var _middlewares = DefaultComposedMiddlewares.RequiredEndpointMiddlewares.ToList();
                _middlewares.AddRange(middlewares);

                var context = DefaultContext.Create(plugins, app.ServiceProvider);

                SetStateValues(context.State, httpContext);
                await MiddlewareComposer.Compose([.. _middlewares])(context);
            });

            if (producesStatusCodes != null)
            {
                foreach (int statusCode in producesStatusCodes)
                {
                    builder.Produces(statusCode);
                }
            }

            if (middlewares.Any(m => m == CoreMiddlewares.PaginationMiddleware))
            {
                builder.WithOpenApi(operation =>
                {
                    operation.Parameters.Add(new OpenApiParameter
                    {
                        Name = "page",
                        In = ParameterLocation.Query,
                        Required = true,
                        Schema = new OpenApiSchema
                        {
                            Type = "integer"
                        }
                    });
                    operation.Parameters.Add(new OpenApiParameter
                    {
                        Name = "per_page",
                        In = ParameterLocation.Query,
                        Required = true,
                        Schema = new OpenApiSchema
                        {
                            Type = "integer"
                        }
                    });
                    return operation;
                });
            }

            return builder;
        }

        private static void SetStateValues(dynamic state, HttpContext httpContext)
        {
            state.HttpContext = httpContext;
        }

        public static RouteHandlerBuilder MapPost(
            this IEndpointRouteBuilder app,
            string routeName,
            IList<IPlugin> plugins,
            MiddlewareDelegate[] middlewares,
            int[]? producesStatusCodes = null)
        {
            var builder = app.MapPost(routeName, async (HttpContext httpContext, CancellationToken cancellationToken) =>
            {
                var _middlewares = DefaultComposedMiddlewares.RequiredEndpointMiddlewares.ToList();
                _middlewares.AddRange(middlewares);

                var context = DefaultContext.Create(plugins, app.ServiceProvider);

                SetStateValues(context.State, httpContext);
                await MiddlewareComposer.Compose([.. _middlewares])(context);
            });

            if (producesStatusCodes != null)
            {
                foreach (int statusCode in producesStatusCodes)
                {
                    builder.Produces(statusCode);
                }
            }

            return builder;
        }

        public static RouteHandlerBuilder MapDelete(
            this IEndpointRouteBuilder app,
            string routeName,
            IList<IPlugin> plugins,
            MiddlewareDelegate[] middlewares,
            int[]? producesStatusCodes = null)
        {
            var builder = app.MapDelete(routeName, async (HttpContext httpContext, CancellationToken cancellationToken) =>
            {
                var _middlewares = DefaultComposedMiddlewares.RequiredEndpointMiddlewares.ToList();
                _middlewares.AddRange(middlewares);

                var context = DefaultContext.Create(plugins, app.ServiceProvider);

                SetStateValues(context.State, httpContext);
                await MiddlewareComposer.Compose([.. _middlewares])(context);
            });

            if (producesStatusCodes != null)
            {
                foreach (int statusCode in producesStatusCodes)
                {
                    builder.Produces(statusCode);
                }
            }

            return builder;
        }
    }
}
