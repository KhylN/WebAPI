using System.Data.SqlClient;

namespace SafeVaultSecure.Services
{
    public class DatabaseService
    {
        private readonly string _connectionString;

        public DatabaseService(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        public object? GetUser(string username)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();

            string query = "SELECT Username, Role FROM Users WHERE Username = @username";
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@username", username);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new { Username = reader["Username"], Role = reader["Role"] };
            }
            return null;
        }
    }
}
