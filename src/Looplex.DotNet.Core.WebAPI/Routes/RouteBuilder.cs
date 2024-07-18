using Looplex.DotNet.Core.Middlewares;
using Looplex.DotNet.Core.WebAPI.Factories;
using Looplex.DotNet.Core.WebAPI.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Looplex.DotNet.Core.WebAPI.Routes
{
    public static class RouteBuilder
    {
        public static RouteHandlerBuilder MapGet(
            this IEndpointRouteBuilder app,
            string routeName,
            RouteBuilderOptions options)
        {
            var builder = app.MapGet(routeName, async (HttpContext httpContext, CancellationToken cancellationToken) =>
            {
                var contextFactory = httpContext.RequestServices.GetRequiredService<IContextFactory>();
                var middlewares = DefaultComposedMiddlewares.RequiredEndpointMiddlewares.ToList();
                middlewares.AddRange(options.Middlewares);

                var context = contextFactory.Create(options.Services);

                SetStateValues(context.State, httpContext);
                await MiddlewareComposer.Compose([.. middlewares])(context);
            });

            AddStatusCodesToRoute(options, builder);

            if (options.Middlewares.Any(m => m == CoreMiddlewares.PaginationMiddleware))
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

        private static void AddStatusCodesToRoute(RouteBuilderOptions options, RouteHandlerBuilder builder)
        {
            if (options.ProducesStatusCodes.Length == 0)
            {
                foreach (var statusCode in options.ProducesStatusCodes)
                {
                    builder.Produces(statusCode);
                }
            }
        }

        private static void SetStateValues(dynamic state, HttpContext httpContext)
        {
            state.HttpContext = httpContext;
        }

        public static RouteHandlerBuilder MapPost(
            this IEndpointRouteBuilder app,
            string routeName,
            RouteBuilderOptions options)
        {
            var builder = app.MapPost(routeName, async (HttpContext httpContext, CancellationToken cancellationToken) =>
            {
                var contextFactory = httpContext.RequestServices.GetRequiredService<IContextFactory>();
                var middlewares = DefaultComposedMiddlewares.RequiredEndpointMiddlewares.ToList();
                middlewares.AddRange(options.Middlewares);

                var context = contextFactory.Create(options.Services);

                SetStateValues(context.State, httpContext);
                await MiddlewareComposer.Compose([.. middlewares])(context);
            });

            AddStatusCodesToRoute(options, builder);

            return builder;
        }

        public static RouteHandlerBuilder MapDelete(
            this IEndpointRouteBuilder app,
            string routeName,
            RouteBuilderOptions options)
        {
            var builder = app.MapDelete(routeName, async (HttpContext httpContext, CancellationToken cancellationToken) =>
            {
                var contextFactory = httpContext.RequestServices.GetRequiredService<IContextFactory>();
                var middlewares = DefaultComposedMiddlewares.RequiredEndpointMiddlewares.ToList();
                middlewares.AddRange(options.Middlewares);

                var context = contextFactory.Create(options.Services);

                SetStateValues(context.State, httpContext);
                await MiddlewareComposer.Compose([.. middlewares])(context);
            });

            AddStatusCodesToRoute(options, builder);

            return builder;
        }
    }
}
