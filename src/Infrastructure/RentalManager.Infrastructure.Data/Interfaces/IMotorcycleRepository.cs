using RentalManager.Domain.Entities;
using RentalManager.Domain.Interfaces;

namespace RentalManager.Infrastructure.Data.Interfaces
{
    public interface IMotorcycleRepository : IRepository<Motorcycle>
    {
        Task<bool> ExistsByPlateAsync(string plate);
    }
}
