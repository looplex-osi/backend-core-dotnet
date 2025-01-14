using System.Threading;
using Looplex.OpenForExtension.Abstractions.Contexts;

namespace Looplex.DotNet.Core.Application.Abstractions.Services;

/// <summary>
/// Provides role-based access control (RBAC) functionality for authorization checks.
/// </summary>
public interface IRbacService
{
    /// <summary>
    /// Checks if the user in the context has permission to perform an action on
    /// a resource within a domain (also from context).
    /// </summary>
    /// <param name="context"></param>
    /// <param name="resource"></param>
    /// <param name="action"></param>
    /// <param name="cancellationToken"></param>
    void ThrowIfUnauthorized(IContext context, string resource, string action, CancellationToken cancellationToken);
}