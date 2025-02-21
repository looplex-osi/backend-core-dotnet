using System.Data;
using System.Threading.Tasks;

namespace Looplex.DotNet.Core.Application.Abstractions.Providers;

public interface IDbConnectionProvider
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="tenant">The tenant to get the database name and connection for</param>
    /// <returns>Returns (connection, databaseName) for the tenant</returns>
    Task<(IDbConnection, string)> GetConnectionAsync(string tenant);
}