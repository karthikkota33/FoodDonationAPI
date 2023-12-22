using System.Data.SqlClient;

namespace FoodDonationAPI.Services
{
    public interface IDatabaseConnectionService : IDisposable
    {
        Task<SqlConnection> CreateConnectionAsync();
        SqlConnection CreateConnection();
    }
}
