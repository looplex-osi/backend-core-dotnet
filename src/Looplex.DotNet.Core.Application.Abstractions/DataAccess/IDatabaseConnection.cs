using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace Looplex.DotNet.Core.Application.Abstractions.DataAccess
{
    public interface IDatabaseConnection : IDisposable
    {
        Task<int> ExecuteAsync(string sql, object? parameters = null, IDbTransaction? transaction = null);

        Task<IEnumerable<T>> QueryAsync<T>(string sql, object? parameters = null, IDbTransaction? transaction = null);

        Task<IEnumerable<TR>> QueryAsync<TF, TS, TR>(string sql, Func<TF, TS, TR> map, object? parameters = null, IDbTransaction? transaction = null, string splitOn = "Id");

        Task<T?> QueryFirstOrDefaultAsync<T>(string sql, object? parameters = null, IDbTransaction? transaction = null);

        DbTransaction BeginTransaction();

        void Open();
    }
}
