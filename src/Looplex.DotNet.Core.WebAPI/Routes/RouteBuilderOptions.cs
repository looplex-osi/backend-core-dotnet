using Looplex.DotNet.Core.Middlewares;
// ReSharper disable CollectionNeverUpdated.Global
// ReSharper disable ClassNeverInstantiated.Global

namespace Looplex.DotNet.Core.WebAPI.Routes;

public record RouteBuilderOptions
{
    public IList<string> Services { get; } = [];
    public IList<MiddlewareDelegate> Middlewares { get; } = [];
}