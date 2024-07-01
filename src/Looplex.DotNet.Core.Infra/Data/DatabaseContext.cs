using Looplex.DotNet.Core.Application.Abstractions.DataAccess;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Looplex.DotNet.Core.Infra.Data
{
    public class DatabaseContext : IDatabaseContext
    {
        private readonly IConfiguration _configuration;

        public DatabaseContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDatabaseConnection CreateConnection()
        {
            var server = _configuration["DbServer"];
            var port = _configuration["DbPort"];
            var user = _configuration["DbUser"];
            var password = _configuration["Password"];
            var database = _configuration["Database"];
            var connectionString = $"Server={server}, {port};Database={database};User ID={user};Password={password};TrustServerCertificate=true";

            var connection = new SqlConnection(connectionString);

            return new SqlDatabaseConnectionAdapter(connection);
        }
    }
}
