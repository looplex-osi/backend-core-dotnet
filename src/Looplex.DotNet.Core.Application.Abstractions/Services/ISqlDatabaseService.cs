using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace Looplex.DotNet.Core.Application.Abstractions.Services;

public interface ISqlDatabaseService : IDisposable
{
    Task<int> ExecuteAsync(string sql, object? parameters = null, IDbTransaction? transaction = null,
        CommandType? commandType = null);

    Task<IEnumerable<T>> QueryAsync<T>(string sql, object? parameters = null, IDbTransaction? transaction = null,
        CommandType? commandType = null);

    Task<IEnumerable<TR>> QueryAsync<TF, TS, TR>(string sql, Func<TF, TS, TR> map, object? parameters = null,
        IDbTransaction? transaction = null, string splitOn = "Id", CommandType? commandType = null);

    Task<T?> QueryFirstOrDefaultAsync<T>(string sql, object? parameters = null, IDbTransaction? transaction = null,
        CommandType? commandType = null);

    Task<IEnumerable<T1>> QueryMultipleAsync<T1>(string sql, object? param = null, IDbTransaction? transaction = null,
        CommandType? commandType = null);

    Task<(IEnumerable<T1>, IEnumerable<T2>)> QueryMultipleAsync<T1, T2>(string sql, object? param = null,
        IDbTransaction? transaction = null, CommandType? commandType = null);

    Task<(IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>)> QueryMultipleAsync<T1, T2, T3>(string sql,
        object? param = null, IDbTransaction? transaction = null, CommandType? commandType = null);

    Task<(IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>)> QueryMultipleAsync<T1, T2, T3, T4>(
        string sql, object? param = null, IDbTransaction? transaction = null, CommandType? commandType = null);

    Task<(IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>)>
        QueryMultipleAsync<T1, T2, T3, T4, T5>(string sql, object? param = null, IDbTransaction? transaction = null,
            CommandType? commandType = null);

    Task<(IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>)>
        QueryMultipleAsync<T1, T2, T3, T4, T5, T6>(string sql, object? param = null, IDbTransaction? transaction = null,
            CommandType? commandType = null);

    Task<(IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>,
        IEnumerable<T7>)> QueryMultipleAsync<T1, T2, T3, T4, T5, T6, T7>(string sql, object? param = null,
        IDbTransaction? transaction = null, CommandType? commandType = null);

    Task<(IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>,
        IEnumerable<T7>, IEnumerable<T8>)> QueryMultipleAsync<T1, T2, T3, T4, T5, T6, T7, T8>(string sql,
        object? param = null, IDbTransaction? transaction = null, CommandType? commandType = null);

    Task<(IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>,
        IEnumerable<T7>, IEnumerable<T8>, IEnumerable<T9>)> QueryMultipleAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9>(
        string sql, object? param = null, IDbTransaction? transaction = null, CommandType? commandType = null);

    DbTransaction BeginTransaction();

    Task OpenConnectionAsync();
}