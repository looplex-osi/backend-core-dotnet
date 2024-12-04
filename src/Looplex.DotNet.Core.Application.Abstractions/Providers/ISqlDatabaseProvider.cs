using System.Threading.Tasks;
using Looplex.DotNet.Core.Application.Abstractions.Services;

namespace Looplex.DotNet.Core.Application.Abstractions.Providers;

public interface ISqlDatabaseProvider
{
    Task<ISqlDatabaseService> GetDatabaseAsync(string domain);
}