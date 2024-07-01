using Npgsql;
using System.Data;

namespace RentalManager.Infrastructure.Data.Contexts
{
    public class DapperDatabaseContext : IDapperDatabaseContext
    {
        private readonly IDbConnection _connection;
        private readonly IDbTransaction _transaction;

        public DapperDatabaseContext(string connectionString)
        {
            _connection = new NpgsqlConnection(connectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        public IDbConnection Connection => _connection;
        public IDbTransaction Transaction => _transaction;

        public void Dispose()
        {
            _transaction?.Dispose();
            _connection?.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}
