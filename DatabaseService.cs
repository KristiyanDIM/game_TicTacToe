using Dapper;
using Microsoft.Data.SqlClient;

public class DatabaseService
{
    private readonly string _connectionString = "Server=DESKTOP-IGVKO3E\\SQLEXPRESS;Database=game;Trusted_Connection=True;TrustServerCertificate=True;";

    // Метод за създаване на връзка към MS SQL
    public SqlConnection CreateConnection() => new SqlConnection(_connectionString);

    // Метод за асинхронно зареждане на данни от базата
    public async Task<IEnumerable<game.LoginPage.User>> GetLeaderboardAsync()
    {
        try
        {
            using (var connection = CreateConnection())
            {
                await connection.OpenAsync();
                var leaderboard = await connection.QueryAsync<game.LoginPage.User>(
                    "SELECT * FROM Users ORDER BY Score DESC");
                return leaderboard;
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Error fetching leaderboard data", ex);
        }
    }
}
