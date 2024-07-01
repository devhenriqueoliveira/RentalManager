namespace RentalManager.Infrastructure.CrossCutting.Commons.Settings
{
    public class DatabaseSettings(string connectionString)
    {
        public string ConnectionString { get; private set; } = connectionString;
    }
}
