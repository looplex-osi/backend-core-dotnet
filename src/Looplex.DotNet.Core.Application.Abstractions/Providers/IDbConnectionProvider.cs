using System.Data;
using System.Threading.Tasks;

namespace Looplex.DotNet.Core.Application.Abstractions.Providers;

public interface IDbConnectionProvider
{
    Task<IDbConnection> GetConnectionAsync(string domain, out string databaseName);
}