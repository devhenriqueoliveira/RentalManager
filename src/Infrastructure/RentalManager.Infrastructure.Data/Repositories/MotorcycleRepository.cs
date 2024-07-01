using Dapper;
using RentalManager.Domain.Entities;
using RentalManager.Infrastructure.Data.Contexts;
using RentalManager.Infrastructure.Data.Interfaces;
using static Dapper.SqlMapper;

namespace RentalManager.Infrastructure.Data.Repositories
{
    public class MotorcycleRepository(
        IDapperDatabaseContext context) : IMotorcycleRepository
    {
        #region Fields

        private readonly IDapperDatabaseContext _context = context;

        #endregion

        #region Commands
        public async Task<bool> CreateAsync(Motorcycle entity)
        {
            var query = "INSERT INTO Motorcycle (Id, Year, Plate, Model) VALUES (@Id, @Year, @Plate, @Model)";

            var recordsInsert = await _context.Connection.ExecuteAsync(query, entity, _context.Transaction);

            return recordsInsert > 0;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var query = "DELETE FROM Motorcycle WHERE Id = @Id";

            var recordsInsert = await _context.Connection.ExecuteAsync(query, new { Id = id }, _context.Transaction);

            return recordsInsert > 0;
        }

        public Task<bool> UpdateAsync(Motorcycle entity)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Queries
        public async Task<bool> ExistsByPlateAsync(string plate)
        {
            string sql = "SELECT COUNT(1) FROM Motorcycle WHERE Plate = @Plate";
            var count = await _context.Connection.QuerySingleOrDefaultAsync<int>(sql, new { Plate = plate }, _context.Transaction);
            return count > 0;
        }

        public async Task<IReadOnlyCollection<Motorcycle>> GetAllAsync()
        {
            string sql = "SELECT * FROM Motorcycle";
            var result = await _context.Connection.QueryAsync<Motorcycle>(sql, _context.Transaction);
            return result.ToList();
        }

        public async Task<Motorcycle> GetByIdAsync(Guid id)
        {
            string sql = "SELECT Id, Year, Plate, Model FROM Motorcycle Where Id = @Id";
            var result = await _context.Connection.QueryFirstOrDefaultAsync<Motorcycle>(sql, new { Id = id }, _context.Transaction);
            return result!;
        }

        #endregion
    }
}
