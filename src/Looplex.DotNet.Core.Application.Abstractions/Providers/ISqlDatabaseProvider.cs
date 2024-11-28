using Looplex.DotNet.Core.Application.Abstractions.Services;

namespace Looplex.DotNet.Core.Application.Abstractions.Providers;

public interface ISqlDatabaseProvider
{
    ISqlDatabaseService GetDatabase(string domain);
}