using System.Collections.Generic;
using Looplex.OpenForExtension.Abstractions.Contexts;

namespace Looplex.DotNet.Core.Application.Abstractions.Factories;

public interface IContextFactory
{
    IContext Create(IEnumerable<string> services);
}