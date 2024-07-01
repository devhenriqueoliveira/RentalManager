using RentalManager.Infrastructure.Data.Contexts;
using RentalManager.Infrastructure.Data.Interfaces;
using System.Data;

namespace RentalManager.Infrastructure.Data.UnitOfWork
{
    public class UnitOfWork(
        IDapperDatabaseContext context,
        IMotorcycleRepository motorcycleRepository) : IUnitOfWork
    {
        public IMotorcycleRepository Motorcycles { get; } = motorcycleRepository;
        private readonly IDapperDatabaseContext _context = context;
        private bool _disposed;

        public async Task<bool> CommitAsync()
        {
            try
            {
                _context.Transaction.Commit();
                return await Task.FromResult(true);
            }
            catch
            {
                await RollBackAsync();
                return await Task.FromResult(false);
            }
        }

        public async Task<bool> RollBackAsync()
        {
            try
            {
                _context.Transaction.Rollback();
                return await Task.FromResult(true);
            }
            catch
            {
                return await Task.FromResult(true);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
