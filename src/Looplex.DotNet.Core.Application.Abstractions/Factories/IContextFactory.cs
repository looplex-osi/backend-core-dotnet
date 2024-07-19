using System.Collections.Generic;
using Looplex.OpenForExtension.Context;

namespace Looplex.DotNet.Core.Application.Abstractions.Factories;

public interface IContextFactory
{
    IDefaultContext Create(IEnumerable<string> services);
}