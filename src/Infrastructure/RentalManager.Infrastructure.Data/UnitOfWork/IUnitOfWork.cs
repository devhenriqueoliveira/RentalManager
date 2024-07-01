using RentalManager.Infrastructure.Data.Interfaces;

namespace RentalManager.Infrastructure.Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IMotorcycleRepository Motorcycles { get; }
        Task<bool> RollBackAsync();
        Task<bool> CommitAsync();
    }
}
