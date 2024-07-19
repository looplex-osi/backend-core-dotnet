using Looplex.DotNet.Core.Middlewares;

namespace Looplex.DotNet.Core.WebAPI.Routes;

public record RouteBuilderOptions
{
    public string[] Services { get; init; } = [];
    public required MiddlewareDelegate[] Middlewares { get; init; }
}