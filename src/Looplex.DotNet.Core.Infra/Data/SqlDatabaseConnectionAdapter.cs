using Dapper;
using Looplex.DotNet.Core.Application.Abstractions.DataAccess;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.Common;

namespace Looplex.DotNet.Core.Infra.Data
{
    public class SqlDatabaseConnectionAdapter : IDatabaseConnection
    {
        private readonly SqlConnection _connection;

        public SqlDatabaseConnectionAdapter(SqlConnection connection)
        {
            _connection = connection;
        }

        public void Open()
        {
            _connection.Open();
        }

        public void Dispose()
        {
            _connection.Dispose();
        }

        public Task<int> ExecuteAsync(string sql, object? parameters = null, IDbTransaction? transaction = null)
        {
            return _connection.ExecuteAsync(sql, parameters, transaction);
        }

        public Task<IEnumerable<T>> QueryAsync<T>(string sql, object? parameters = null, IDbTransaction? transaction = null)
        {
            return _connection.QueryAsync<T>(sql, parameters, transaction);
        }

        public Task<IEnumerable<TR>> QueryAsync<TF, TS, TR>(string sql, Func<TF, TS, TR> map, object? parameters = null, IDbTransaction? transaction = null, string splitOn = "Id")
        {
            return _connection.QueryAsync(sql, map, param: parameters, transaction, splitOn: splitOn);
        }

        public Task<T?> QueryFirstOrDefaultAsync<T>(string sql, object? parameters = null, IDbTransaction? transaction = null)
        {
            return _connection.QueryFirstOrDefaultAsync<T>(sql, parameters, transaction);
        }

        public DbTransaction BeginTransaction()
        {
            return _connection.BeginTransaction();
        }
    }
}
