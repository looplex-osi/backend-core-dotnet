using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace Looplex.DotNet.Core.Application.Abstractions.Services;

public interface ISqlDatabaseService : IDisposable
{
    Task<int> ExecuteAsync(string sql, object? parameters = null, IDbTransaction? transaction = null, CommandType? commandType = null);

    Task<IEnumerable<T>> QueryAsync<T>(string sql, object? parameters = null, IDbTransaction? transaction = null, CommandType? commandType = null);

    Task<IEnumerable<TR>> QueryAsync<TF, TS, TR>(string sql, Func<TF, TS, TR> map, object? parameters = null, IDbTransaction? transaction = null, string splitOn = "Id", CommandType? commandType = null);

    Task<T?> QueryFirstOrDefaultAsync<T>(string sql, object? parameters = null, IDbTransaction? transaction = null, CommandType? commandType = null);

    Task<(IEnumerable<T1>, IEnumerable<T2>)> QueryMultipleAsync<T1, T2>(string sql, object? param = null, IDbTransaction? transaction = null, CommandType? commandType = null);
    
    DbTransaction BeginTransaction();

    Task OpenConnectionAsync();
}