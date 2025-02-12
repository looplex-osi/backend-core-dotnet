﻿using Looplex.DotNet.Core.Application.Abstractions.Factories;
using Looplex.DotNet.Core.Middlewares;
using Looplex.DotNet.Core.WebAPI.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
// ReSharper disable SuspiciousTypeConversion.Global

namespace Looplex.DotNet.Core.WebAPI.Routes
{
    public static class RouteBuilder
    {
        public static RouteHandlerBuilder MapGet(
            this IEndpointRouteBuilder app,
            string routeName,
            RouteBuilderOptions options)
        {
            return app.MapGet(routeName, async (HttpContext httpContext, CancellationToken cancellationToken) =>
            {
                var contextFactory = httpContext.RequestServices.GetRequiredService<IContextFactory>();
                var middlewares = DefaultComposedMiddlewares.RequiredEndpointMiddlewares.ToList();
                middlewares.AddRange(options.Middlewares);

                var context = contextFactory.Create(options.Services);

                context.State.HttpContext = httpContext;
                context.State.CancellationToken = cancellationToken;
                await MiddlewareComposer.Compose([.. middlewares])(context);
                
                if (context is IDisposable disposableContext)
                    disposableContext.Dispose();
            });
        }

        public static RouteHandlerBuilder MapPost(
            this IEndpointRouteBuilder app,
            string routeName,
            RouteBuilderOptions options)
        {
            return app.MapPost(routeName, async (HttpContext httpContext, CancellationToken cancellationToken) =>
            {
                var contextFactory = httpContext.RequestServices.GetRequiredService<IContextFactory>();
                var middlewares = DefaultComposedMiddlewares.RequiredEndpointMiddlewares.ToList();
                middlewares.AddRange(options.Middlewares);

                var context = contextFactory.Create(options.Services);

                context.State.HttpContext = httpContext;
                context.State.CancellationToken = cancellationToken;
                await MiddlewareComposer.Compose([.. middlewares])(context);
                
                if (context is IDisposable disposableContext)
                    disposableContext.Dispose();
            });
        }

        public static RouteHandlerBuilder MapPut(
            this IEndpointRouteBuilder app,
            string routeName,
            RouteBuilderOptions options)
        {
            return app.MapPut(routeName, async (HttpContext httpContext, CancellationToken cancellationToken) =>
            {
                var contextFactory = httpContext.RequestServices.GetRequiredService<IContextFactory>();
                var middlewares = DefaultComposedMiddlewares.RequiredEndpointMiddlewares.ToList();
                middlewares.AddRange(options.Middlewares);

                var context = contextFactory.Create(options.Services);

                context.State.HttpContext = httpContext;
                context.State.CancellationToken = cancellationToken;
                await MiddlewareComposer.Compose([.. middlewares])(context);
                
                if (context is IDisposable disposableContext)
                    disposableContext.Dispose();
            });
        }

        public static RouteHandlerBuilder MapPatch(
            this IEndpointRouteBuilder app,
            string routeName,
            RouteBuilderOptions options)
        {
            return app.MapPatch(routeName, async (HttpContext httpContext, CancellationToken cancellationToken) =>
            {
                var contextFactory = httpContext.RequestServices.GetRequiredService<IContextFactory>();
                var middlewares = DefaultComposedMiddlewares.RequiredEndpointMiddlewares.ToList();
                middlewares.AddRange(options.Middlewares);

                var context = contextFactory.Create(options.Services);

                context.State.HttpContext = httpContext;
                context.State.CancellationToken = cancellationToken;
                await MiddlewareComposer.Compose([.. middlewares])(context);
                
                if (context is IDisposable disposableContext)
                    disposableContext.Dispose();
            });
        }
        
        public static RouteHandlerBuilder MapDelete(
            this IEndpointRouteBuilder app,
            string routeName,
            RouteBuilderOptions options)
        {
            return app.MapDelete(routeName, async (HttpContext httpContext, CancellationToken cancellationToken) =>
            {
                var contextFactory = httpContext.RequestServices.GetRequiredService<IContextFactory>();
                var middlewares = DefaultComposedMiddlewares.RequiredEndpointMiddlewares.ToList();
                middlewares.AddRange(options.Middlewares);

                var context = contextFactory.Create(options.Services);

                context.State.HttpContext = httpContext;
                context.State.CancellationToken = cancellationToken;
                await MiddlewareComposer.Compose([.. middlewares])(context);
                
                if (context is IDisposable disposableContext)
                    disposableContext.Dispose();
            });
        }
    }
}
