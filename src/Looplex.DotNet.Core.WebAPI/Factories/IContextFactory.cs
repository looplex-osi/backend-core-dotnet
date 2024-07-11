using Looplex.OpenForExtension.Context;

namespace Looplex.DotNet.Core.WebAPI.Factories;

public interface IContextFactory
{
    IDefaultContext Create(IEnumerable<string> services);
}