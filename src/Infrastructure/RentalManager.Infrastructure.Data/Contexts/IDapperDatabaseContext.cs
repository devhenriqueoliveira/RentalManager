using System.Data;

namespace RentalManager.Infrastructure.Data.Contexts
{
    public interface IDapperDatabaseContext : IDisposable
    {
        IDbTransaction Transaction { get; }
        IDbConnection Connection { get; }
    }
}
